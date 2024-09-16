using CustomMessenger.Service.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.DTO.Messages
{
    [FilledChatOrGroup]
    public class MessageForCreation
    {
        [Required]
        public string Content { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? GroupId { get; set; }
    }
}
