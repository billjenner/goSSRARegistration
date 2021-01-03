using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace goSSRA.Registration.Models
{
    /// <summary>
    /// DB Class used to build page of payment buttons
    /// </summary>
    public class PaymentBtns
    {
        public int PaymentBtnsID { get; set; }

        public bool buttonStyleLinks { get; set; }

        [Display(Name = "Text Description")]
        public string Desc { get; set; }

        public Int32 ?Amount { get; set; }

        [Required]
        [Display(Name = "Link to Payment Colletion Page")]
        public string hRefLink { get; set; }

        [Required]
        [Display(Name = "Button Description")]
        public string btnDesc { get; set; }
    }
}
