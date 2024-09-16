using CustomMessenger.Domain.Commons;

namespace CustomMessenger.Domain.Entities
{
    public class Group : Auditable
    {
        public string Name { get; set; }
        public List<Member> Members { get; set; }
        public List<Message> Messages { get; set; }
        public string UniqueName { get; set; }
    }
}