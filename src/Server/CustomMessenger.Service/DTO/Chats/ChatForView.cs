using CustomMessenger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.DTO.Chats
{
    public class ChatForView
    {
        public Guid FirstUserId { get; set; }
        public Guid SecondUserId { get; set; }
    }
}
