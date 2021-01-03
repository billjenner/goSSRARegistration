using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using goSSRA.Registration.Models;

namespace goSSRA.Registration.ViewModel
{
    // View - used on confirmation page, review Enrollment
    // and receipt instructions
    public class Receipt
    {
        public ReceiptData ReceiptData { get; set; }
        public Enrollment Enrollment { get; set; }
        public string ImageUrl { get; set; }
    }
}