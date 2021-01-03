using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Net.Mail;
using MvcMembership;
using MvcMembership.Settings;
using goSSRA.Registration.Models.AccountModels;

namespace goSSRA.Registration.Controllers.AccountModels
{
    public class AccountController : Controller
    {
        private const int PageSize = 10;
        private const string ResetPasswordBody = "Your new password is: ";
        private const string ResetPasswordSubject = "Your New Password";
        private readonly IRolesService _rolesService;
        private readonly ISmtpClient _smtpClient;
        private readonly IMembershipSettings _membershipSettings;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;

        public AccountController()
            : this(new AspNetMembershipProviderWrapper(), new AspNetRoleProviderWrapper(), new SmtpClientProxy())
        {
        }

        public AccountController(AspNetMembershipProviderWrapper membership, IRolesService roles, ISmtpClient smtp)
            : this(membership.Settings, membership, membership, roles, smtp)
        {
        }

        public AccountController(
            IMembershipSettings membershipSettings,
            IUserService userService,
            IPasswordService passwordService,
            IRolesService rolesService,
            ISmtpClient smtpClient)
        {
            _membershipSettings = membershipSettings;
            _userService = userService;
            _passwordService = passwordService;
            _rolesService = rolesService;
            _smtpClient = smtpClient;
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        //
        // GET: /Account/Register

        public ActionResult ResetLogonPassword()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult ResetLogonPassword(String email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Contains("@"))
                {
                    var user = _userService.Get(email);

                    if (user != null)
                    {
                        var newPassword = _passwordService.ResetPassword(user);

                        var body = ResetPasswordBody + newPassword;
                        var msg = new MailMessage();
                        msg.To.Add(user.Email);
                        msg.Subject = ResetPasswordSubject;
                        msg.Body = body;
                        _smtpClient.Send(msg);

                        return RedirectToAction("ResetPasswordEmailConfirmed");
                    }
                }
            }

            return RedirectToAction("CannotFindLogon", new { email });
        }

        //
        // GET: /Account/ResetPasswordEmailConfirmed

        public ActionResult ResetPasswordEmailConfirmed()
        {
            return View();
        }

        public ActionResult CannotFindLogon(string email)
        {
            ViewBag.email = email;

            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }

}