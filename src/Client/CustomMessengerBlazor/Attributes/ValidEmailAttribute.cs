using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomMessengerBlazor.Attributes
{
    public class ValidEmailAttribute : ValidationAttribute
    {
        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string email = value.ToString();

            if (Regex.IsMatch(email, EmailPattern))
            {
                return ValidationResult.Success;
            }

            // Return error message if the validation fails
            return new ValidationResult("Invalid email address format.");
        }
    }
}
