namespace CustomMessenger.Service.DTO.Chats
{
    public class ChatForView
    {
        public Guid Id { get; set; }
        public Guid FirstUserId { get; set; }
        public Guid SecondUserId { get; set; }
    }
}
