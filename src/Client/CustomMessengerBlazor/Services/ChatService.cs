using CustomMessenger.Service.DTO.Chats;
using CustomMessengerBlazor.Interfaces;

namespace CustomMessengerBlazor.Services
{
    public class ChatService : IChatService
    {
        public Task CreateAsync(ChatForCreation dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatForView>> GetAllByUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ChatWithMessages> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
