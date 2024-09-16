using CustomMessenger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Data.IRepositories
{
    public interface IGroupRepository
    {
        Task CreateAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(Guid id);
        Task<Group> GetByIdAsync(Guid id);
        Task<Group> GetByUniqueNameAsync(string uniqueName);
        Task<IEnumerable<Group>> SearchAsync(string query);
    }
}
