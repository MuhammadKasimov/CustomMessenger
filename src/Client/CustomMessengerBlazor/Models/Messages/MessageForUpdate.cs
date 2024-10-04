using System.ComponentModel.DataAnnotations;

namespace CustomMessenger.Service.DTO.Messages
{
    public class MessageForUpdate
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
