using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomMessengerBlazor.Attributes
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public StrongPasswordAttribute()
        {
            ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one digit.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var password = value as string;
            if (password == null)
                return false;

            // Check for minimum 8 characters, at least one letter and one digit
            return password.Length >= 8 &&
                   password.Length <= 30 &&
                   Regex.IsMatch(password, @"[A-Za-z]") &&
                   Regex.IsMatch(password, @"\d");
        }
    }
}
