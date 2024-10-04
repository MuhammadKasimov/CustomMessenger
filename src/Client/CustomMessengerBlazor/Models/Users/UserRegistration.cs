using CustomMessengerBlazor.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CustomMessengerBlazor.Models.Users
{
    public class UserRegistration
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, StrongPassword]
        public string Password { get; set; } = string.Empty;
        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, ValidUniquename]
        public string Username { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        [AllowNull,ValidEmail]
        public string Email { get; set; } = string.Empty;
    }
}
