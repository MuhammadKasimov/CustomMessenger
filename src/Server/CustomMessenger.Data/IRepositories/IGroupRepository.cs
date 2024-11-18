using CustomMessenger.Domain.Entities;

namespace CustomMessenger.Data.IRepositories
{
    public interface IGroupRepository
    {
        Task CreateAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(Guid id);
        Task<Group> GetIncludeByIdAsync(Guid id);
        Task<Group> GetByIdAsync(Guid id);
        Task<Group> GetIncludeByUniqueNameAsync(string uniqueName);
        Task<Group> GetByUniqueNameAsync(string uniqueName);
        Task<IEnumerable<Group>> SearchAsync(string query);
    }
}
