using CustomMessenger.Data.Extensions;
using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using Npgsql;
using NpgsqlTypes;

namespace CustomMessenger.Data.Repositories
{
    public class GroupRepository(string connectionString) : IGroupRepository
    {
        public async Task CreateAsync(Group group)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("INSERT INTO groups (id, name, uniquename, createdat) VALUES (@_id, @_name, @_uniquename, @_createdat);", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, group.Id);
                    command.Parameters.AddWithValue("_name", NpgsqlDbType.Varchar, group.Name);
                    command.Parameters.AddWithValue("_uniquename", NpgsqlDbType.Varchar, group.UniqueName);
                    command.Parameters.AddWithValue("_createdat", NpgsqlDbType.TimestampTz, group.CreatedAt);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Group group)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("UPDATE groups SET name = @_name, uniquename = @_uniquename WHERE id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_name", NpgsqlDbType.Varchar, group.Name);
                    command.Parameters.AddWithValue("_uniquename", NpgsqlDbType.Varchar, group.UniqueName);
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, group.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM groups WHERE id = @_id", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<Group>> SearchAsync(string query)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                if (!string.IsNullOrEmpty(query))
                    query = query.Replace(query, $"%{query}%");
                using (var command = new NpgsqlCommand("SELECT * FROM groups WHERE @_query is null OR name like @_query OR uniquename like @_query;", connection))
                {
                    var groups = new List<Group>();
                    command.Parameters.AddWithValue("_query", NpgsqlDbType.Varchar, query is null ? DBNull.Value : query);

                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        groups.Add(reader.MapReaderToGroup());
                    }
                    return groups;
                }
            }
        }

        public async Task<Group> GetIncludeByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT DISTINCT ON (g.name, g.uniquename, g.createdat, g.updatedat) g.id, g.name, g.uniquename, g.createdat, g.updatedat, ARRAY_AGG(m.id) AS memberids, ARRAY_AGG(m.userid) AS userids,
                                                                            ARRAY_AGG(m.role) AS roles, ARRAY_AGG(u.name) AS names, 
                                                                            ARRAY_AGG(me.id) AS messageids, ARRAY_AGG(me.senderid) as senders, ARRAY_AGG(me.content) contents FROM groups AS g 
                                                                            LEFT JOIN messages AS me ON me.groupid = g.id
                                                                            LEFT JOIN members AS m ON m.groupid = g.id
                                                                            LEFT JOIN users AS u ON u.id = m.userid WHERE g.id = @_id GROUP BY g.id;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        return reader.MapReaderToGroupWithUsers();
                    }
                    return null;
                }
            }
        }
        public async Task<Group> GetByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM groups as g WHERE g.id = @_id;", connection))
                {
                    command.Parameters.AddWithValue("_id", NpgsqlDbType.Uuid, id);

                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        return reader.MapReaderToGroup();
                    }
                    return null;
                }
            }
        }

        public async Task<Group> GetIncludeByUniqueNameAsync(string uniqueName)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT DISTINCT ON (g.name, g.uniquename, g.createdat, g.updatedat) g.id, g.name, g.uniquename, g.createdat, g.updatedat, ARRAY_AGG(m.id) AS memberids, ARRAY_AGG(m.userid) AS userids,
                                                                            ARRAY_AGG(m.role) AS roles, ARRAY_AGG(u.name) AS names, 
                                                                            ARRAY_AGG(me.id) AS messageids, ARRAY_AGG(me.senderid) as senders, ARRAY_AGG(me.content) contents FROM groups AS g 
                                                                            LEFT JOIN messages AS me ON me.groupid = g.id
                                                                            LEFT JOIN members AS m ON m.groupid = g.id
                                                                            LEFT JOIN users AS u ON u.id = m.userid WHERE uniquename = @_uniquename GROUP BY g.id;", connection))
                {
                    command.Parameters.AddWithValue("_uniquename", NpgsqlDbType.Varchar, uniqueName);

                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        return reader.MapReaderToGroupWithUsers();
                    }
                    return null;
                }
            }
        }
        public async Task<Group> GetByUniqueNameAsync(string uniqueName)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM groups WHERE uniquename = @_uniquename;", connection))
                {
                    command.Parameters.AddWithValue("_uniquename", NpgsqlDbType.Varchar, uniqueName);

                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        return reader.MapReaderToGroup();
                    }
                    return null;
                }
            }
        }
    }
}
