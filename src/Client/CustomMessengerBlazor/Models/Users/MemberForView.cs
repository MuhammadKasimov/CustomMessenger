using CustomMessenger.Domain.Enums;


namespace CustomMessenger.Service.DTO.Users
{
    public class MemberForView
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public Role Role { get; set; }
    }
}
