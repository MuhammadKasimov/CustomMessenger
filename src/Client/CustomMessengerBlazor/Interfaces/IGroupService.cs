using CustomMessenger.Domain.Enums;
using CustomMessenger.Service.DTO.Groups;
using CustomMessenger.Service.DTO.Users;

namespace CustomMessengerBlazor.Interfaces
{
    public interface IGroupService
    {
        Task CreateAsync(GroupForCreation dto);
        Task UpdateAsync(GroupForUpdate dto);
        Task DeleteAsync(Guid id);
        Task<SingleGroupForView> GetByIdAsync(Guid id);
        Task<SingleGroupForView> GetByUniqueNameAsync(string uniquename);
        Task<IEnumerable<GroupForView>> SearchAsync(string query);
        Task AddMemberAsync(MemberForCreation member);
        Task DeleteMemberAsync(Guid id);
        Task ChangeRole(Guid memberid, Role role);
    }
}
