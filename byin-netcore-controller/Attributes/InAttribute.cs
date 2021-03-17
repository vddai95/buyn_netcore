using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace byin_netcore.Attributes
{
    public class InAttribute : ValidationAttribute
    {
        //
        // Summary:
        //     Gets or sets the list of string validated.
        //
        // Returns:
        //     The list of string validated.
        private IEnumerable<string> StringValidated { get; }

        //
        // Summary:
        //     Initializes a new instance of the byin_netcore.Attributes.InAttribute
        //     class by using a list of string validated.
        //
        // Parameters:
        //   stringValidated:
        //     list of string validated.
        public InAttribute(params string[] stringValidated)
        {
            StringValidated = stringValidated.ToList();
        }

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
            return string.Format("{0} must contain one of these values: {1}", name, StringValidated.ToString());
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
            string str = (string)value;
            return StringValidated.Contains(str);
        }
    }
}
