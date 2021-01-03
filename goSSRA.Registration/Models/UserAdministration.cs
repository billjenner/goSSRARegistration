using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using PagedList;

namespace goSSRA.Registration.Models.UserAdministration
{
    public class CreateUserViewModel
    {
        [Display(Name = "User Name")]
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password (Again...)")]
        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email Address")]
        [Required, Email]
        public string Email { get; set; }

        [Display(Name = "Secret Question")]
        public string PasswordQuestion { get; set; }

        [StringLength(100)]
        [Display(Name = "Secret Answer")]
        public string PasswordAnswer { get; set; }

        [Display(Name = "Initial Roles")]
        public IDictionary<string, bool> InitialRoles { get; set; }
    }

    public class DetailsViewModel
    {
        #region StatusEnum enum

        public enum StatusEnum
        {
            Offline,
            Online,
            LockedOut,
            Unapproved
        }

        #endregion

        public string DisplayName { get; set; }
        public StatusEnum Status { get; set; }
        public MembershipUser User { get; set; }
        public bool CanResetPassword { get; set; }
        public bool RequirePasswordQuestionAnswerToResetPassword { get; set; }
        public IDictionary<string, bool> Roles { get; set; }
        public bool IsRolesEnabled { get; set; }
    }

    public class IndexViewModel
    {
        public string Search { get; set; }
        public IPagedList<MembershipUser> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsRolesEnabled { get; set; }
    }

    public class RoleViewModel
    {
        public string Role { get; set; }
        public IDictionary<string, MembershipUser> Users { get; set; }
    }
}