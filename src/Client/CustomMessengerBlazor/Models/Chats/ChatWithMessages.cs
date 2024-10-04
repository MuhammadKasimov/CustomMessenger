namespace CustomMessenger.Service.DTO.Chats
{
    public class ChatWithMessages
    {
        public Guid FirstUserId { get; set; }
        public Guid SecondUserId { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }

    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; }
    }

}
