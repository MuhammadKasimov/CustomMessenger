using System.ComponentModel.DataAnnotations;

namespace CustomMessengerBlazor.Models.Users
{
    public class UserLogin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
