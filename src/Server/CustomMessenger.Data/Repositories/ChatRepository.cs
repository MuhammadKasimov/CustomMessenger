using CustomMessenger.Data.Extensions;
using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using Npgsql;
using NpgsqlTypes;

namespace CustomMessenger.Data.Repositories
{
    public class ChatRepository(string connectionString) : IChatRepository
    {
        public async Task CreateAsync(Chat chat)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("INSERT INTO chats (id, firstuserid, seconduserid, createdat) values (@_id, @_firstuserid, @_seconduserid, @_createdat)", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, chat.Id);
                    command.Parameters.AddWithValue("_firstuserid", NpgsqlDbType.Uuid, chat.FirstUserId);
                    command.Parameters.AddWithValue("_seconduserid", NpgsqlDbType.Uuid, chat.SecondUserId);
                    command.Parameters.AddWithValue("_createdat", NpgsqlDbType.TimestampTz, chat.CreatedAt);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM chats WHERE id = @_id;", connection))
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

        public async Task<Chat> GetIncludeByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT DISTINCT ON(c.firstuserid, c.seconduserid, c.createdat, c.updatedat) c.id, c.firstuserid, c.seconduserid, c.createdat, c.updatedat, ARRAY_AGG(m.content) AS contents, ARRAY_AGG(m.senderid) AS senders, ARRAY_AGG(m.id) as messageids FROM chats AS c 
                                                                            LEFT JOIN messages AS m ON m.chatid = c.id WHERE c.id = @_id GROUP BY c.id ;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToChatWithMessages();
                    }
                    return null;
                }
            }
        }
        public async Task<Chat> GetByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM chats WHERE id = @_id;", connection))
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
                using (var command = new NpgsqlCommand("SELECT * FROM chats WHERE firstuserid = @_firstmember AND seconduserid = @_secondmember OR firstuserid = @_secondmember AND seconduserid = @_firstmember;", connection))
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
