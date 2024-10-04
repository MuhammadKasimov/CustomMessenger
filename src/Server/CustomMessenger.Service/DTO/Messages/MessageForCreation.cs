using CustomMessenger.Service.Attributes;
using System.ComponentModel.DataAnnotations;

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
