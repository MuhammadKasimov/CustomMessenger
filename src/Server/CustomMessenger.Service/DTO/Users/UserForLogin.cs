using CustomMessenger.Service.Attributes;

namespace CustomMessenger.Service.DTO.Users
{
    public class UserForLogin
    {
        [ValidUniquename]
        public string Username { get; set; }
        [StrongPassword]
        public string Password { get; set; }
    }
}
