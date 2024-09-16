using CustomMessenger.Domain.Commons;

namespace CustomMessenger.Domain.Entities
{
    public class Chat : Auditable
    {
        public Guid FirstUserId { get; set; }
        public Guid SecondUserId { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}