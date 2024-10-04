using System.ComponentModel.DataAnnotations;

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
