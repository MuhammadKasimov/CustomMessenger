using CustomMessenger.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
