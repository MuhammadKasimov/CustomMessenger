using CustomMessenger.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomMessenger.Service.DTO.Users
{
    public class MemberForCreation
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid GroupId { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
