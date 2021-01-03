using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace goSSRA.Registration.UI
{
    public class Utils
    {
        public static class Validate
        {
            private static Regex p_USPhoneValidator = new Regex(@"^(([1-9][0-9]{2})[\-\.]([1-9][0-9]{2})[\-\.]([0-9]{4})|([1-9][0-9]{2}) ([1-9][0-9]{2}) ([0-9]{4})|(([(][1-9][0-9]{2}[)]) ([1-9][0-9]{2})-([0-9]{4})))( [x] ([1-9][0-9]{1,4}))?$");

            /// <summary>
            /// Validates US phone number
            /// </summary>
            /// <param name="phone">(string) phone number, dash [-] or dot[.] are allowed for deliminator between AC, Prefix, Suffix</param>
            /// <returns>(bool) true/false</returns>
            public static bool USPhone(string phone)
            {
                if (phone.Length > 0)
                {
                    return p_USPhoneValidator.IsMatch(phone);
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Validates Date within reasonable range
            /// </summary>
            /// <param name="dateTime"></param>
            /// <returns>(bool) true/false</returns>
            public static bool bDateRange(DateTime date)
            {
                DateTime dateBegin = new DateTime(1900, 01, 01);
                DateTime dateEnd = new DateTime(2050, 01, 01);

                if (DateTime.Compare(date, dateBegin) > 1)
                {
                    if (DateTime.Compare(date, dateEnd) < 1)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}