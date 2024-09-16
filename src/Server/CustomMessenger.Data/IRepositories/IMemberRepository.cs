using CustomMessenger.Domain.Entities;
using CustomMessenger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Data.IRepositories
{
    public interface IMemberRepository
    {
        Task CreateAsync(Member member);
        Task DeleteAsync(Guid id);
        Task<Member> GetByIdsAsync(Guid userId, Guid groupId);
        Task<Member> GetByIdAsync(Guid id);
        Task ChangeRoleAsync(Guid id, Role role);
    }
}
