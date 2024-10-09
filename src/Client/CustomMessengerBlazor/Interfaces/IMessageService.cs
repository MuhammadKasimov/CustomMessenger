using CustomMessenger.Service.DTO.Messages;

namespace CustomMessengerBlazor.Interfaces
{
    public interface IMessageService
    {
        Task CreateAsync(MessageForCreation message);
        Task UpdateAsync(MessageForUpdate messageForUpdate);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<MessageForView>> GetAllAsync(string search = null, Guid? chatId = null, Guid? userId = null, Guid? groupId = null);
    }
}
