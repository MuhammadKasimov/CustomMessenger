using CustomMessenger.Domain.Entities;
using CustomMessenger.Service.DTO.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.Interfaces
{
    public interface IChatService
    {
        Task CreateAsync(ChatForCreation dto);
        Task DeleteAsync(Guid id);
        Task<ChatWithMessages> GetByIdAsync(Guid id);
        Task<IEnumerable<ChatForView>> GetAllByUserAsync();
    }
}
