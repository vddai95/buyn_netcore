using System;
using System.ComponentModel.DataAnnotations;

namespace byin_netcore.Attributes
{
    public class TimeZoneAttribute : ValidationAttribute
    {
        //
        // Summary:
        //     Applies formatting to a specified error message.
        //
        // Parameters:
        //   name:
        //     The name of the field that caused the validation failure.
        //
        // Returns:
        //     The formatted error message.
        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0} must contain a timezone value", name);
        }

        //
        // Summary:
        //     Determines whether a specified object is valid.
        //
        // Parameters:
        //   value:
        //     The object to validate.
        //
        // Returns:
        //     true if the specified object is valid; otherwise, false.
        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return true;
            }
            string str = (string)value;
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(str);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
