using CustomMessenger.Domain.Entities;

namespace CustomMessenger.Data.IRepositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByNumberAsync(string phonenumber);
        Task<IEnumerable<User>> GetAllAsync(string query);
    }
}
