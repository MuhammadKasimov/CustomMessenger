using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.DTO.Chats
{
    public class ChatForCreation
    {
        [Required]
        public Guid FirstUserId { get; set; }
        [Required]
        public Guid SecondUserId { get; set; }
    }
}
