namespace CustomMessenger.Service.DTO.Messages
{
    public class MessageForView
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? GroupId { get; set; }
    }
}
