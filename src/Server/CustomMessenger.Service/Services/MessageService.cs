using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using CustomMessenger.Service.DTO.Messages;
using CustomMessenger.Service.Exceptions;
using CustomMessenger.Service.Helpers;
using CustomMessenger.Service.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.Services
{
    public class MessageService(IMessageRepository messageRepository, IChatRepository chatRepository, IGroupRepository groupRepository) : IMessageService
    {
        public async Task CreateAsync(MessageForCreation dto)
        {
            if (dto.ChatId is null)
            {
                var existGroup = await groupRepository.GetByIdAsync((Guid)dto.GroupId) 
                    ?? throw new HttpStatusCodeException(404, "Chat not found");
            }
            else
            {
                var existChat = await chatRepository.GetByIdAsync((Guid)dto.ChatId)
                    ?? throw new HttpStatusCodeException(404, "Group not found");
            }

            var message = dto.Adapt<Message>();
            
            message.SenderId = (Guid)HttpContextHelper.UserId;
            
            await messageRepository.CreateAsync(message);

        }

        public async Task UpdateAsync(MessageForUpdate dto)
        {
            var existMessage = await messageRepository.GetByIdAsync(dto.Id);
            if (existMessage is null)
                throw new HttpStatusCodeException(404, "No message found");
            await messageRepository.UpdateAsync(dto.Id,dto.Content);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existMessage = await messageRepository.GetByIdAsync(id);
            if (existMessage is null)
                throw new HttpStatusCodeException(404, "No message found");

            await messageRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MessageForView>> GetAllAsync(string search = null, Guid? chatId = null, Guid? userId = null, Guid? groupId = null)
        {
            var messages = await messageRepository.GetAllAsync(search, chatId, userId, groupId);

            return messages.Adapt<IEnumerable<MessageForView>>();
        }
    }
}
