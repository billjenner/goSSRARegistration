using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using goSSRA.Registration.Areas.MvcMembership;
using goSSRA.Registration.UI;

namespace goSSRA.Registration.Models
{
    // custom DataAnnotation for BooleanRequired
    public class BooleanRequired : RequiredAttribute, IClientValidatable
    {

        public BooleanRequired()
        {

        }

        public override bool IsValid(object value)
        {
            return value != null && (bool)value == true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRule() { ValidationType = "brequired", ErrorMessage = this.ErrorMessage } };
        }
    }

    public class Athlete
    {
        private string _email;
        private string _postCode;

        [Key]
        public int AthleteID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }


        [Required]
        [RegularExpression("^[MF]", ErrorMessage = "Invalid Gender")]
        public string Gender { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[bDateRange(ErrorMessage = "Birth Date must be valid")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street or P. O. Box")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [MaxLength(2, ErrorMessage = "State must be 2 characters")]
        [MinLength(2, ErrorMessage = "State must be 2 characters")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postcode/Zipcode is required.")]
        [RegularExpression("^[DFIOQUWZ0-9]{5}(-[DFIOQUWZ0-9]{4})?", ErrorMessage = "Postcode must be alphanumeric and no more than 10 characters in length")]
        public string Postcode
        {
            get
            {
                return _postCode;
            }

            set
            {
                _postCode = (! String.IsNullOrEmpty(value)) ? value.ToLower() : string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Display(Name = "E-mail")]
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

        [Required(ErrorMessage = "Parent/Guardian is required")]
        [Display(Name = "Parent/Guardian #1")]
        public string ParentName1 { get; set; }

        [Required(ErrorMessage = "Phone # is required.")]
        [Display(Name = "Phone #1")]
        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$", ErrorMessage = "Invalid phone number")]
        //[PhoneFormat(ErrorMessage = "Phone format is not valid")]
        public string Parent1Phone1 { get; set; }

        [Display(Name = null)]
        public string Parent1Phone1Type { get; set; }

        [Display(Name = "Phone #2")]
        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$", ErrorMessage = "Invalid phone number")]
        public string Parent1Phone2 { get; set; }

        [Display(Name = null)]
        public string Parent1Phone2Type { get; set; }

        [Display(Name = "Parent/Guardian #2")]
        public string ParentName2 { get; set; }


        [Display(Name = "Phone #1")]
        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$", ErrorMessage = "Invalid phone number")]
        public string Parent2Phone1 { get; set; }

        [Display(Name = null)]
        public string Parent2Phone1Type { get; set; }

        [Display(Name = "Phone #2")]
        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$", ErrorMessage = "Invalid phone number")]
        public string Parent2Phone2 { get; set; }

        [Display(Name = null)]
        public string Parent2Phone2Type { get; set; }

        [Required]
        [Display(Name = "Existing Medical Conditions")]
        public string MedicalConditions { get; set; }

        [Required]
        [Display(Name = "Present Medications")]
        public string PresentMedications { get; set; }

        [Required]
        public string Allergies { get; set; }

        public string School { get; set; }

        public string Grade { get; set; }

        [Required]
        [Display(Name = "Medical Insurance")]
        public string MedicalInsurance { get; set; }

        [Required]
        [Display(Name = "Policy #")]
        public string PolicyNum { get; set; }

        [Required]
        [Display(Name = "Insurance Phone")]
        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$", ErrorMessage = "Invalid phone number")]
        public string InsurancePhone { get; set; }

        [Required]
        [Display(Name = "Physician")]
        public string Physician { get; set; }

        [Required]
        [Display(Name = "Physician Phone")]
        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$", ErrorMessage = "Invalid phone number")]
        public string PhysicianPhone { get; set; }

        [Required]
        [Display(Name = "Emergeny Contact")]
        public string EmergenyContact { get; set; }

        [Required]
        [Display(Name = "Emergeny Contact Phone")]
        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$", ErrorMessage = "Invalid phone number")]
        public string EmergenyContactPhone { get; set; }
    }
}