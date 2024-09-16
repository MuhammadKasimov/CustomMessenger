using CustomMessenger.Domain.Entities;
using CustomMessenger.Service.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserForCreation dto);
        Task UpdateAsync(UserForUpdate dto);
        Task DeleteAsync(Guid id);
        Task<UserForView> GetByIdAsync(Guid id);
        Task<UserForView> GetSelfAsync();
        Task<UserForView> GetByNumberAsync(string phonenumber);
        Task<UserForView> GetByUsernameAsync(string username);
        Task<IEnumerable<UserForView>> GetAllAsync(string query);
        Task<object> LoginAsync(UserForLogin login);
    }
}
