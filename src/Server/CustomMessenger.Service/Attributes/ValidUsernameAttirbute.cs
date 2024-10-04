using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomMessenger.Service.Attributes
{
    public class ValidUniquenameAttribute : ValidationAttribute
    {
        public ValidUniquenameAttribute()
        {
            ErrorMessage = "Username must be no more than 16 characters long, contain only letters and digits, and must not start with a digit.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var username = value as string;
            if (username == null)
                return false;

            // Check if username is no more than 16 characters
            if (username.Length > 16)
                return false;

            // Check if username contains only letters and digits and doesn't start with a digit
            return Regex.IsMatch(username, @"^[A-Za-z][A-Za-z0-9]*$");
        }
    }
}
