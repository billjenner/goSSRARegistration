using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace goSSRA.Registration.Models
{
    public class VolunteerRoles
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string Role { get; set; }

        public string Desc { get; set; }
        
        [Display(Name = "Admin Role")]
        public bool AdminRole { get; set; }

        [Display(Name = "Race Role")]
        public bool RaceRole { get; set; }

        [Display(Name = "Other Role")]
        public bool OtherRole { get; set; }

        public bool Active { get; set; }
    }

    public class VolunteerEvents
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        public string Desc { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [Display(Name = "Admin Role")]
        public bool AdminRole { get; set; }

        [Display(Name = "Race Role")]
        public bool RaceRole { get; set; }

        [Display(Name = "Other Role")]
        public bool OtherRole { get; set; }

        public bool Active { get; set; }
    }

    public class VolunteerCommitments
    {
        private string _email;

        [Key]
        public int CommitmentID { get; set; }

        [Required]
        [Display(Name = "Event")]
        public int EventID { get; set; }
        public VolunteerEvents VolunteerEvent { get; set; }

        [Required]
        [Display(Name = "Preferred Role")]
        public int RoleID { get; set; }
        public VolunteerRoles VolunteerRole { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Display(Name = "Registers E-mail")]
        [RegularExpression("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$", ErrorMessage = "Invalid email address")]
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = (! String.IsNullOrEmpty(value)) ? value.ToLower() : string.Empty;
            }
        }

        [Required]
        [Display(Name = "Family Name")]
        public string FamilyName { get; set; }
    }

}