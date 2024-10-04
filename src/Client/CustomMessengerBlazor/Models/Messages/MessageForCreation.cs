using System.ComponentModel.DataAnnotations;

namespace CustomMessenger.Service.DTO.Messages
{
    public class MessageForCreation
    {
        [Required]
        public string Content { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? GroupId { get; set; }
    }
}
