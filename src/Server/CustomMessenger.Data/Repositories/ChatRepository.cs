using CustomMessenger.Data.Extensions;
using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Data.Repositories
{
    public class ChatRepository(string connectionString) : IChatRepository
    {
        public async Task CreateAsync(Chat chat)
        {
            using(var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("INSET INTO chats (firstuserid, seconduserid) values (@_firstuserid, @_seconduserid)", connection))
                {
                    command.Parameters.AddWithValue("_firstuserid", NpgsqlDbType.Uuid, chat.FirstUserId);
                    command.Parameters.AddWithValue("_seconduserid", NpgsqlDbType.Uuid, chat.SecondUserId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM chats WHERE id = @_id", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<Chat>> GetAllByUserAsync(Guid userId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM chats WHERE firstuserid = @_userid OR seconduserid = @_userid;", connection))
                {
                    var chats = new List<Chat>();
                    command.Parameters.AddWithValue("_userid", NpgsqlDbType.Uuid, userId);

                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        chats.Add(reader.MapReaderToChat());
                    }

                    return chats;
                }
            }
        }

        public async Task<Chat> GetByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT c.*, ARRAY_AGG(m.content) AS contents, ARRAY_AGG(m.senderid) AS senders FROM chats AS c 
                                                                            JOIN messages AS m ON m.chatid = c.id WHERE id = @_id GROUP BY c.*;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToChat();
                    }
                    return null;
                }
            }
        }
        public async Task<Chat> GetByMembersAsync(Guid firstMember, Guid secondMember)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM chats WHERE firstuserid = @_firstmember AND seconduserid = @_secondmember;", connection))
                {
                    command.Parameters.AddWithValue("_firstmember", NpgsqlDbType.Uuid, firstMember);
                    command.Parameters.AddWithValue("_secondmember", NpgsqlDbType.Uuid, firstMember);

                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToChat();
                    }
                    return null;
                }
            }
        }
    }
}
