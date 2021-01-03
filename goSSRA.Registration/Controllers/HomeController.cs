using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goSSRA.Registration.Models;
using System.Net.Mail;
using MvcMembership;
using System.Text;

namespace goSSRA.Registration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISmtpClient _smtpClient;

        public HomeController()
            : this(new SmtpClientProxy())
        {
        }

        public HomeController(ISmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        [Authorize]
        public ActionResult Forms()
        {
            return View();
        }

        [Authorize]
        public ActionResult Form1()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult Form2()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult Form3()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult Form4()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult Form5()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult Form6()
        {
            return PartialView();
        }

        public ActionResult CheckEmail(string emailTo)
        {
            ViewBag.emailTo = emailTo;

            return View();
        }

        public ActionResult EmailTest()
        {

            return View("EmailTest");
        }

        [HttpPost]
        public ActionResult EmailTest(string emailTo)
        {
            ViewBag.emailTo = emailTo;


            StringBuilder sb = new StringBuilder();
            sb.Append("</body>");
            sb.Append("<h3>");
            sb.Append("    <p width='80%' align='center' cellpadding='100' cellspacing='100'>");
            sb.Append("        Email Test");
            sb.Append("    </p>");
            sb.Append("</h3>");
            sb.Append("</body>");

            MailMessage mm = new MailMessage()
            {
                Subject = "Email Test - goSSRA",
                IsBodyHtml = true,
                Body = sb.ToString()
            };

            // Add CC Recipients
            if (!String.IsNullOrEmpty(emailTo))
            {
                mm.To.Add(emailTo);
                mm.CC.Add("bill.jenner@gmail.com");

                _smtpClient.Send(mm);
            }

            return RedirectToAction("CheckEmail", new { emailTo });
        }
    }
}