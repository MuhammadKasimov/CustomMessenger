using CustomMessenger.Domain.Entities;
using CustomMessenger.Domain.Enums;
using Npgsql;
using System.Data;
using System.Text;

namespace CustomMessenger.Data.Extensions
{
    public static class MappingExtensions
    {
        public static Chat MapReaderToChat(this NpgsqlDataReader reader)
        {
            return new Chat
            {
                Id = reader.GetGuid("id"),
                FirstUserId = reader.GetGuid("firstuserid"),
                SecondUserId = reader.GetGuid("seconduserid"),
                CreatedAt = reader.GetDateTime("createdat"),
                UpdatedAt = reader.IsDBNull("updatedat") ? null : reader.GetDateTime("updatedat")
            };
        }
        public static Chat MapReaderToChatWithMessages(this NpgsqlDataReader reader)
        {
            if (reader.GetFieldValue<Guid?[]>("senders")[0] is null)
            {
                return new Chat
                {
                    Id = reader.GetGuid("id"),
                    FirstUserId = reader.GetGuid("firstuserid"),
                    SecondUserId = reader.GetGuid("seconduserid"),
                    CreatedAt = reader.GetDateTime("createdat"),
                    UpdatedAt = reader.IsDBNull("updatedat") ? null : reader.GetDateTime("updatedat")
                };
            }
            var messageSenders = reader.GetFieldValue<Guid[]>("senders");
            var contents = reader.GetFieldValue<string[]>("contents");
            var messageIds = reader.GetFieldValue<Guid[]>("messageids");
            var messages = new List<Message>();
            for (int i = 0; i < messageIds.Length; i++)
            {
                messages.Add(new Message
                {
                    Id = messageIds[i],
                    SenderId = messageSenders[i],
                    Content = contents[i]
                });
            }
            return new Chat
            {
                Id = reader.GetGuid("id"),
                FirstUserId = reader.GetGuid("firstuserid"),
                SecondUserId = reader.GetGuid("seconduserid"),
                Messages = messages,
                CreatedAt = reader.GetDateTime("createdat"),
                UpdatedAt = reader.IsDBNull("updatedat") ? null : reader.GetDateTime("updatedat")
            };
        }
        public static Group MapReaderToGroup(this NpgsqlDataReader reader)
        {
            return new Group
            {
                Id = reader.GetGuid("id"),
                Name = reader.GetString("name"),
                UniqueName = reader.GetString("uniquename"),
                CreatedAt = reader.GetDateTime("createdat"),
                UpdatedAt = reader.IsDBNull("updatedat") ? null : reader.GetDateTime("updatedat")
            };
        }
        public static Group MapReaderToGroupWithUsers(this NpgsqlDataReader reader)
        {
            var memberids = reader.GetFieldValue<Guid[]>("memberids");
            var users = reader.GetFieldValue<Guid[]>("userids");
            var roles = reader.GetFieldValue<int[]>("roles");
            var names = reader.GetFieldValue<string[]>("names");

            var messageSenders = reader.GetFieldValue<Guid?[]>("senders");
            var contents = reader.GetFieldValue<string[]>("contents");
            
            var messageIds = reader.GetFieldValue<Guid?[]>("messageids");
            if (messageIds[0] is null)
            {
                messageIds = [];
                messageSenders = [];
                contents = [];
            }
            var members = new List<Member>();

            var messages = new List<Message>();

            for (int i = 0; i < contents.Length; i++)
            {
                messages.Add(new Message
                {
                    Id = (Guid)messageIds[i],
                    SenderId = (Guid)messageSenders[i],
                    Content = contents[i]
                });
            }
            for (int i = 0; i < memberids.Length; i++)
            {
                members.Add(new Member
                {
                    Id = memberids[i],
                    UserId = users[i],
                    Role = (Role)roles[i],
                    User = new User
                    {
                        Name = names[i]
                    }
                });
            }
            return new Group
            {
                Id = reader.GetGuid("id"),
                Name = reader.GetString("name"),
                UniqueName = reader.GetString("uniquename"),
                CreatedAt = reader.GetDateTime("createdat"),
                UpdatedAt = reader.IsDBNull("updatedat") ? null : reader.GetDateTime("updatedat"),
                Members = members,
                Messages = messages
            };
        }
        public static Member MapReaderToMember(this NpgsqlDataReader reader)
        {
            return new Member
            {
                Id = reader.GetGuid("id"),
                GroupId = reader.GetGuid("groupid"),
                UserId = reader.GetGuid("userid"),
                Role = (Role)reader.GetInt32("role"),
                CreatedAt = reader.GetDateTime("createdat"),
                UpdatedAt = reader.IsDBNull("updatedat") ? null : reader.GetDateTime("updatedat")
            };
        }

        public static Message MapReaderToMessage(this NpgsqlDataReader reader)
        {
            return new Message
            {
                Id = reader.GetGuid("id"),
                GroupId = reader.IsDBNull("groupid") ? null : reader.GetGuid("groupid"),
                SenderId = reader.GetGuid("senderid"),
                ChatId = reader.IsDBNull("chatid") ? null : reader.GetGuid("chatid"),
                Content = reader.GetString("content"),
                CreatedAt = reader.GetDateTime("createdat"),
                UpdatedAt = reader.IsDBNull("updatedat") ? null : reader.GetDateTime("updatedat")
            };
        }

        public static User MapReaderToUser(this NpgsqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetGuid("id"),
                Bio = reader.IsDBNull("bio") ? null : reader.GetString("bio"),
                Email = reader.IsDBNull("email") ? null : reader.GetString("email"),
                PhoneNumber = reader.GetString("phonenumber"),
                Username = reader.GetString("username"),
                Name = reader.GetString("name"),
                Password = reader.GetString("password"),
                CreatedAt = reader.GetDateTime("createdat"),
                UpdatedAt = reader.IsDBNull("updatedat") ? null : reader.GetDateTime("updatedat")
            };
        }
    }
}
