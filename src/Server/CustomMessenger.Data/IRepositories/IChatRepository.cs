using CustomMessenger.Domain.Entities;

namespace CustomMessenger.Data.IRepositories
{
    public interface IChatRepository
    {
        Task CreateAsync(Chat chat);
        Task DeleteAsync(Guid id);
        Task<Chat> GetIncludeByIdAsync(Guid id);
        Task<IEnumerable<Chat>> GetAllByUserAsync(Guid userId);
        Task<Chat> GetByMembersAsync(Guid firstMember, Guid secondMember);
        Task<Chat> GetByIdAsync(Guid id);
    }
}
