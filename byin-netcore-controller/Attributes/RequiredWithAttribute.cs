using System;
using System.ComponentModel.DataAnnotations;

namespace byin_netcore.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredWithAttribute : RequiredAttribute
    {
        //
        // Summary:
        //     Gets or sets the PropertyName need to be coexisted.
        //
        // Returns:
        //     The PropertyName need to be coexisted.
        private string PropertyName { get; set; }

        //
        // Summary:
        //     Initializes a new instance of the byin_netcore.Attributes.RequiredWithAttribute
        //     class by using a  property string name.
        //
        // Parameters:
        //   propertyName:
        //     name of the property need to be coexisted.
        public RequiredWithAttribute(string propertyName)
        {
            PropertyName = propertyName;
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
            return string.Format("{0} must coexiste with {1}", name, PropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            if (string.IsNullOrEmpty(type.GetProperty(PropertyName).GetValue(instance)?.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
