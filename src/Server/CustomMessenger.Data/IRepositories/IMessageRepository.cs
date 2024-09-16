using CustomMessenger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Data.IRepositories
{
    public interface IMessageRepository
    {
        Task CreateAsync(Message message);
        Task UpdateAsync(Guid id, string message);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Message>> GetAllAsync(string search, Guid? chatId, Guid? userId, Guid? groupId);
        Task<Message> GetByIdAsync(Guid id);
    }
}
