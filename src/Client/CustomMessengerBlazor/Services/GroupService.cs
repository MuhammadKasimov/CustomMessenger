using CustomMessenger.Domain.Enums;
using CustomMessenger.Service.DTO.Groups;
using CustomMessenger.Service.DTO.Users;
using CustomMessengerBlazor.Interfaces;

namespace CustomMessengerBlazor.Services
{
    public class GroupService : IGroupService
    {
        public Task AddMemberAsync(MemberForCreation member)
        {
            throw new NotImplementedException();
        }

        public Task ChangeRole(Guid memberid, Role role)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(GroupForCreation dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMemberAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SingleGroupForView> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SingleGroupForView> GetByUniqueNameAsync(string uniquename)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupForView>> SearchAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(GroupForUpdate dto)
        {
            throw new NotImplementedException();
        }
    }
}
