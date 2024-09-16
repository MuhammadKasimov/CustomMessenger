using CustomMessenger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Data.IRepositories
{
    public interface IChatRepository
    {
        Task CreateAsync(Chat chat);
        Task DeleteAsync(Guid id);
        Task<Chat> GetByIdAsync(Guid id);
        Task<IEnumerable<Chat>> GetAllByUserAsync(Guid userId);
        Task<Chat> GetByMembersAsync(Guid firstMember, Guid secondMember);

    }
}
