using CustomMessenger.Service.DTO.Messages;

namespace CustomMessenger.Service.Interfaces
{
    public interface IMessageService
    {
        Task CreateAsync(MessageForCreation message);
        Task UpdateAsync(MessageForUpdate messageForUpdate);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<MessageForView>> GetAllAsync(string search = null, Guid? chatId = null, Guid? userId = null, Guid? groupId = null);
    }
}
