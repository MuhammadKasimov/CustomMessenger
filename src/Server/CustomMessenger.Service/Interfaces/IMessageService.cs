using CustomMessenger.Domain.Entities;
using CustomMessenger.Service.DTO.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
