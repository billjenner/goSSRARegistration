using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace goSSRA.Registration.Models
{
    public class Program
    {
        public int ProgramID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(1.0, 10000.0)]
        public double Price { get; set; }
        [Range(0.0, 10000.0)]
        public double Discount { get; set; }
        public string DiscountDesc { get; set; }
        public bool Active { get; set; }
    }
}