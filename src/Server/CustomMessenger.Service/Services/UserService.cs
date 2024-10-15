using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using CustomMessenger.Service.DTO.Users;
using CustomMessenger.Service.Exceptions;
using CustomMessenger.Service.Extensions;
using CustomMessenger.Service.Helpers;
using CustomMessenger.Service.Interfaces;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomMessenger.Service.Services
{
    public class UserService(IUserRepository userRepository, IConfiguration configuration) : IUserService
    {
        public async Task CreateAsync(UserForCreation dto)
        {
            string validationErrors = string.Empty;
            var alredyExistUsers = await userRepository.GetAllAsync($"{dto.Username} {dto.Email} {dto.PhoneNumber}");

            if (alredyExistUsers.Any(u => u.Username == dto.Username))
                validationErrors += "Username is taken";

            if (alredyExistUsers.Any(u => u.PhoneNumber == dto.PhoneNumber))
                validationErrors += "|Phone number is already used";

            if (alredyExistUsers.Any(u => u.Email == dto.Email))
                validationErrors += "|Email is already used";

            if (!string.IsNullOrEmpty(validationErrors))
                throw new HttpStatusCodeException(400, validationErrors);

            dto.Password = dto.Password.Hash();
            await userRepository.CreateAsync(dto.Adapt<User>());
        }

        public async Task UpdateAsync(UserForUpdate dto)
        {
            string validationErrors = string.Empty;
            var existUser = await userRepository.GetByIdAsync((Guid)HttpContextHelper.UserId);
            if (existUser is null)
                validationErrors += "User not found";

            var alredyExistUsers = (await userRepository
                .GetAllAsync($"{dto.Username} {dto.Email} {dto.PhoneNumber}")).Where(u => u.Id != (Guid)HttpContextHelper.UserId);

            if (alredyExistUsers.Any(u => u.Username == dto.Username))
                validationErrors += "|Username is taken";

            if (alredyExistUsers.Any(u => u.PhoneNumber == dto.PhoneNumber))
                validationErrors += "|Phone number is already used";

            if (alredyExistUsers.Any(u => u.Email == dto.Email))
                validationErrors += "|Email is already used";

            if (!string.IsNullOrEmpty(validationErrors))
                throw new HttpStatusCodeException(400, validationErrors);

            dto.Password = dto.Password.Hash();

            await userRepository.UpdateAsync(dto.Adapt<User>());
        }

        public async Task DeleteAsync(Guid id)
        {
            var existUser = await userRepository.GetByIdAsync(id)
                ?? throw new HttpStatusCodeException(404, "User not found");
            await userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserForView>> GetAllAsync(string query)
        {
            var users = await userRepository.GetAllAsync(query);

            return users.Adapt<IEnumerable<UserForView>>();
        }

        public async Task<UserForView> GetByIdAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id)
                ?? throw new HttpStatusCodeException(404, "User with such id not found");

            return user.Adapt<UserForView>();
        }

        public async Task<UserForView> GetSelfAsync()
        {
            Console.WriteLine(HttpContextHelper.Accessor);
            var user = await userRepository.GetByIdAsync((Guid)HttpContextHelper.UserId);
            return user.Adapt<UserForView>();
        }

        public async Task<UserForView> GetByNumberAsync(string phonenumber)
        {
            var user = await userRepository.GetByNumberAsync(phonenumber)
                ?? throw new HttpStatusCodeException(404, "User with such number not found");

            return user.Adapt<UserForView>();
        }

        public async Task<UserForView> GetByUsernameAsync(string username)
        {
            var user = await userRepository.GetByUsernameAsync(username)
                ?? throw new HttpStatusCodeException(404, "User with such email not found");

            return user.Adapt<UserForView>();
        }

        public async Task<object> LoginAsync(UserForLogin login)
        {
            var user = await userRepository.GetByUsernameAsync(login.Username)
                ?? throw new HttpStatusCodeException(400, "Username or password is wrong");

            if (!login.Password.Verify(user.Password))
                throw new HttpStatusCodeException(200, "Username or password is wrong");

            var token = CreateToken(configuration["Jwt:Key"], configuration["Jwt:Issuer"], user.Id);

            return new
            {
                Token = token
            };
        }

        private string CreateToken(string key, string issuer, Guid id)
        {
            var claims = new[]
            {
               new Claim("Id", id.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
