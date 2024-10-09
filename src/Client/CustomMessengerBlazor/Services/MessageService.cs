using CustomMessenger.Service.DTO.Messages;
using CustomMessengerBlazor.Interfaces;

namespace CustomMessengerBlazor.Services
{
    public class MessageService : IMessageService
    {
        public Task CreateAsync(MessageForCreation message)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageForView>> GetAllAsync(string search = null, Guid? chatId = null, Guid? userId = null, Guid? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MessageForUpdate messageForUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
