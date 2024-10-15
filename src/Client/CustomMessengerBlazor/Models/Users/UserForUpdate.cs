using CustomMessengerBlazor.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CustomMessenger.Service.DTO.Users
{
    public class UserForUpdate
    {
        [Required, MaxLength(60)]
        public string Name { get; set; }
        [Required, StrongPassword]
        public string Password { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }
        [Required, ValidUniquename]
        public string Username { get; set; }
        [MaxLength(250)]
        public string Bio { get; set; }
        [ValidEmail]
        public string Email { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
