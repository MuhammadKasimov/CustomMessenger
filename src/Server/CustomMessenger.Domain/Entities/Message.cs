using CustomMessenger.Domain.Commons;

namespace CustomMessenger.Domain.Entities
{
    public class Message : Auditable
    {
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? GroupId { get; set; }
    }
}