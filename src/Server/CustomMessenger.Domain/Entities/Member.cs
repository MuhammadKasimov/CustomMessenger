using CustomMessenger.Domain.Commons;
using CustomMessenger.Domain.Enums;

namespace CustomMessenger.Domain.Entities
{
    public class Member : Auditable
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid GroupId { get; set; }
        public Role Role { get; set; }
    }
}