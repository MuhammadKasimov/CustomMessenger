using CustomMessenger.Data.Extensions;
using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Data.Repositories
{
    public class UserRepository(string connectionString) : IUserRepository
    {
        public async Task CreateAsync(User user)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO users (id, username, name, password, phonenumber, email, createdat) VALUES (@_id,@_username, @_name, @_password, @_phonenumber, @_email, @_createdat);", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, user.Id);
                    command.Parameters.AddWithValue("_createdat",NpgsqlDbType.TimestampTz, user.CreatedAt);
                    command.Parameters.AddWithValue("_phonenumber",
                        NpgsqlDbType.Varchar, user.PhoneNumber is null ? DBNull.Value : user.PhoneNumber);
                    command.Parameters.AddWithValue("_username",
                        NpgsqlDbType.Varchar, user.Username is null ? DBNull.Value : user.Username);
                    command.Parameters.AddWithValue("_password",
                        NpgsqlDbType.Varchar, user.Password is null ? DBNull.Value : user.Password);
                    command.Parameters.AddWithValue("_email",
                        NpgsqlDbType.Varchar, user.Email is null ? DBNull.Value : user.Email);
                    command.Parameters.AddWithValue("_name",
                        NpgsqlDbType.Varchar, user.Name is null ? DBNull.Value : user.Email);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("UPDATE users SET username = @_username, name = @_name, password = @_password, phonenumber = @_phonenumber, email = @email, updatedat = @_updatedat WHERE id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, user.Id);
                    command.Parameters.AddWithValue("_phonenumber",
                        NpgsqlDbType.Varchar, user.PhoneNumber is null ? DBNull.Value : user.PhoneNumber);
                    command.Parameters.AddWithValue("_username",
                        NpgsqlDbType.Varchar, user.Username is null ? DBNull.Value : user.Username);
                    command.Parameters.AddWithValue("_password",
                        NpgsqlDbType.Varchar, user.Password is null ? DBNull.Value : user.Password);
                    command.Parameters.AddWithValue("_email",
                        NpgsqlDbType.Varchar, user.Email is null ? DBNull.Value : user.Email);
                    command.Parameters.AddWithValue("_name",
                        NpgsqlDbType.Varchar, user.Name is null ? DBNull.Value : user.Email);
                    command.Parameters.AddWithValue("_updatedat",
                        NpgsqlDbType.Timestamp, user.UpdatedAt);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM users WHERE id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync(string query)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                query = $"%{query}%";
                using (var command = new NpgsqlCommand(@"SELECT * FROM users WHERE 
                                                                            @_query is null
                                                                            OR name LIKE @_query 
                                                                            OR username LIKE @_query 
                                                                            OR email LIKE @_query 
                                                                            OR phonenumber LIKE @_query;", connection))
                {
                    var users = new List<User>();
                    command.Parameters.AddWithValue("_query", NpgsqlDbType.Varchar ,query is null ? DBNull.Value : query);
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        users.Add(reader.MapReaderToUser());
                    }
                    return users;
                }
            }
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM users WHERE id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToUser();
                    }
                    return null;
                }
            }
        }

        public async Task<User> GetByNumberAsync(string phonenumber)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM users WHERE phonenumber = @_phonenumber;", connection))
                {
                    command.Parameters.AddWithValue("_phonenumber", NpgsqlDbType.Varchar, phonenumber);
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToUser();
                    }
                    return null;
                }
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM users WHERE username = @_username;", connection))
                {
                    command.Parameters.AddWithValue("_username", NpgsqlDbType.Varchar, username);
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToUser();
                    }
                    return null;
                }
            }
        }
        
        public async Task<User> GetByEmailAsync(string email)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM users WHERE email = @_email;", connection))
                {
                    command.Parameters.AddWithValue("_email", NpgsqlDbType.Varchar, email);
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToUser();
                    }
                    return null;
                }
            }
        }
    }
}
