using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace goSSRA.Registration.Models
{

    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        [Display(Name = "Enrollers Name")]
        public string EnrollersName { get; set; }

        public int AthleteID { get; set; }
        public Athlete Athlete { get; set; }

        public int ProgramID { get; set; }
        public Program Program { get; set; }

        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Payment Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$", ErrorMessage = "Invalid email address")]
        public String PaymentEmail { get; set; }

        // used in payment options screens
        public Int32? Amount { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Paid To Date")]
        public double PaidToDate { get; set; }

        [Display(Name = "Unpaid Amount")]
        public double UnpaidAmount { get; set; }

        // boolean to inform user if they paid or not
        public bool Paid { get; set; }

        [Display(Name = "I have read and accept the terms and conditions for the SSRA Code of Conduct Agreement")]
        [BooleanRequired(ErrorMessage = "You must accept the terms and conditions")]
        public bool SSRACodeofConductForm { get; set; }

        [Display(Name = "I have read and accept the terms and conditions for the Parental Code of Conduct-SSRA")]
        [BooleanRequired(ErrorMessage = "You must accept the terms and conditions")]
        public bool ParentCodeofConductSSRAForm { get; set; }

        [Display(Name = "I have read and accept the terms and conditions for the Volunteer Agreement")]
        [BooleanRequired(ErrorMessage = "You must accept the terms and conditions")]
        public bool VolunteerForm { get; set; }

        [Display(Name = "I have read and accept the terms and conditions for the Assumption and acceptance of rs Agreement")]
        [BooleanRequired(ErrorMessage = "You must accept the terms and conditions")]
        public bool AssumptionAndAcceptanceOfRsForm { get; set; }

        [Display(Name = "I have read and accept the terms and conditions for the Medical Release Agreement")]
        [BooleanRequired(ErrorMessage = "You must accept the terms and conditions")]
        public bool MedicalReleaseForm { get; set; }

        [Display(Name = "I have read and accept the terms and conditions for the Concussion Information Sheet")]
        [BooleanRequired(ErrorMessage = "You must accept the terms and conditions")]
        public bool ConcussionInfoForm { get; set; }

        [Display(Name = "Forms Complete")]
        public bool ConsentFormsOk { get; set; }

        [Display(Name = "Health Check Complete")]
        public bool HealthChkOk { get; set; }
    }
}