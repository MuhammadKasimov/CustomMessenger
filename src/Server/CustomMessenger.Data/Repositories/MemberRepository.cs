using CustomMessenger.Data.Extensions;
using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using CustomMessenger.Domain.Enums;
using Npgsql;
using NpgsqlTypes;
using System.Text.RegularExpressions;

namespace CustomMessenger.Data.Repositories
{
    public class MemberRepository(string connectionString) : IMemberRepository
    {
        public async Task CreateAsync(Member member)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("INSERT INTO members (userid, groupid, iscreator, isadmin) VALUES (@_userid, @_groupid, @_role)", connection))
                {
                    command.Parameters.AddWithValue("_userid",NpgsqlDbType.Uuid, member.UserId);
                    command.Parameters.AddWithValue("_groupid",NpgsqlDbType.Uuid, member.GroupId);
                    command.Parameters.AddWithValue("_role",NpgsqlDbType.Integer, (int)member.Role);
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand($"DELETE FROM members WHERE userid = @_userid AND groupid = @_groupid", connection))
                {
                    command.Parameters.AddWithValue("_userid", NpgsqlDbType.Uuid, id);
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task ChangeRoleAsync(Guid id, Role role)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand($"UPDATE users SET role = _role WHERE id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);
                    command.Parameters.AddWithValue("role", NpgsqlDbType.Integer, (int)role);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<Member> GetByIdsAsync(Guid userId, Guid groupId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand($"SELECT * FROM members WHERE userid = @_userid AND groupid = @_groupid", connection))
                {
                    command.Parameters.AddWithValue("_userid", NpgsqlDbType.Uuid, userId);
                    command.Parameters.AddWithValue("_groupid", NpgsqlDbType.Uuid, groupId);

                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToMember();
                    }
                    return null;
                }
            }
        }
        public async Task<Member> GetByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand($"SELECT * FROM members WHERE id = @_id", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToMember();
                    }
                    return null;
                }
            }
        }
    }
}