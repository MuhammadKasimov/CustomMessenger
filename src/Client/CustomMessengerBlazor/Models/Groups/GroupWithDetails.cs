using CustomMessenger.Domain.Enums;

namespace CustomMessenger.Service.DTO.Groups
{
    public class SingleGroupForView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public List<GroupMemberView> Members { get; set; }
        public List<GroupMessage> Messages { get; set; }
    }

    public class GroupMessage
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; }
    }

    public class GroupMemberView
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Role Role { get; set; }
        public GroupUserView User { get; set; }
    }

    public class GroupUserView
    {
        public string Name { get; set; }
    }
}
