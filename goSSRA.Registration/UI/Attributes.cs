using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace goSSRA.Registration.UI
{
    public class PhoneFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string phone = value as string;
            if (!string.IsNullOrEmpty(phone))
            {
                return Utils.Validate.USPhone(phone);
            }

            return false;
        }
    }

    public class bDateRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var thisDate = (DateTime)value;

            //DateTime thisDate
            //if (DateTime.TryParse(value, out thisDate))
            if (thisDate != null)
            {
                return Utils.Validate.bDateRange(thisDate);
            }

            return false;
        }
    }
}