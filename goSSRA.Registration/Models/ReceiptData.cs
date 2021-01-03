using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;


namespace goSSRA.Registration.Models
{
    public class ReceiptData
    {
        private string _emailRecipient1;
        private string _emailRecipient2;

        [Key]
        public int ReceiptDataID { get; set; }

        public string HeaderInfo { get; set; }

        public string Footer1Info { get; set; }

        public string Footer2Info { get; set; }

        public string Footer3Info { get; set; }

        [Display(Name = "E-mails Recipient#1")]
        [RegularExpression("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$", ErrorMessage = "Invalid email address")]
        public string EmailRecipient1
        {
            get
            {
                return _emailRecipient1;
            }

            set
            {
                _emailRecipient1 = (! String.IsNullOrEmpty(value)) ? value.ToLower() : string.Empty;
            }
        }

        [Display(Name = "E-mails Recipient#2")]
        [RegularExpression("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$", ErrorMessage = "Invalid email address")]
        public string EmailRecipient2
        {
            get
            {
                return _emailRecipient2;
            }

            set
            {
                _emailRecipient2 = (!String.IsNullOrEmpty(value)) ? value.ToLower() : string.Empty;
            }
        }
    }
}
