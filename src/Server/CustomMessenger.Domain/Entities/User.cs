using CustomMessenger.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Domain.Entities
{
    public class User : Auditable
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Chat> Chats { get; set; } 
        public IEnumerable<Message> Messages { get; set; }
    }
}
