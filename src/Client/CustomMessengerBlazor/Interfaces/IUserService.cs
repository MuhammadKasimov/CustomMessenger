using CustomMessenger.Service.DTO.Users;
using CustomMessengerBlazor.Models.Users;

namespace CustomMessengerBlazor.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserRegistration dto);
        Task UpdateAsync(UserForUpdate dto);
        Task DeleteAsync(Guid id);
        Task<UserForView> GetByIdAsync(Guid id);
        Task<UserForView> GetSelfAsync();
        Task<UserForView> GetByNumberAsync(string phonenumber);
        Task<UserForView> GetByUsernameAsync(string username);
        Task<IEnumerable<UserForView>> GetAllAsync(string query);
        Task<object> LoginAsync(UserLogin login);
    }
}
