using CustomMessenger.Data.Extensions;
using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using Npgsql;
using NpgsqlTypes;

namespace CustomMessenger.Data.Repositories
{
    public class MessageRepository(string connectionString) : IMessageRepository
    {
        public async Task CreateAsync(Message message)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("INSERT INTO messages (id, content, senderid, chatid, groupid, createdat) VALUES (@_id, @_content, @_senderid, @_chatid, @_groupid, @_createdat);", connection))
                {

                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, message.Id);
                    command.Parameters.AddWithValue("_content", NpgsqlDbType.Varchar, message.Content);
                    command.Parameters.AddWithValue("_senderid", NpgsqlDbType.Uuid, message.SenderId);
                    command.Parameters.AddWithValue("_chatid", NpgsqlDbType.Uuid, message.ChatId is null ? DBNull.Value : message.ChatId);
                    command.Parameters.AddWithValue("_groupid", NpgsqlDbType.Uuid, message.GroupId is null ? DBNull.Value : message.GroupId);
                    command.Parameters.AddWithValue("_createdat", NpgsqlDbType.TimestampTz, message.CreatedAt);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Guid id, string message)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("UPDATE messages SET content = @_content WHERE id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_content", NpgsqlDbType.Varchar, message);
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM messages WHERE id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<Message> GetByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM messages WHERE id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        return reader.MapReaderToMessage();
                    }
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Message>> GetAllAsync(string search, Guid? chatId, Guid? senderId, Guid? groupId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM messages 
                                                                            WHERE 
                                                                            CASE
                                                                                WHEN @_search IS NOT NULL OR @_search != '' THEN content = @_search
                                                                                WHEN @_chatid IS NOT NULL THEN chatid = @_chatid
                                                                                WHEN @_groupid IS NOT NULL THEN groupid = @_groupid
                                                                                WHEN @_senderid IS NOT NULL THEN senderid = @_senderid
                                                                                ELSE TRUE
                                                                            END;", connection))
                {
                    var messages = new List<Message>();
                    command.Parameters.AddWithValue("_search", NpgsqlDbType.Varchar, search);
                    command.Parameters.AddWithValue("_chatid", NpgsqlDbType.Uuid, chatId is null ? DBNull.Value : chatId);
                    command.Parameters.AddWithValue("_senderid", NpgsqlDbType.Uuid, senderId is null ? DBNull.Value : senderId);
                    command.Parameters.AddWithValue("_groupid", NpgsqlDbType.Uuid, groupId is null ? DBNull.Value : groupId);

                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        messages.Add(reader.MapReaderToMessage());
                    }
                    return messages;
                }
            }
        }
    }
}
