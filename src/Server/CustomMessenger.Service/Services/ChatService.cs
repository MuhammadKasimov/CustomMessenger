using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using CustomMessenger.Service.DTO.Chats;
using CustomMessenger.Service.Exceptions;
using CustomMessenger.Service.Helpers;
using CustomMessenger.Service.Interfaces;
using Mapster;

namespace CustomMessenger.Service.Services
{
    public class ChatService(IChatRepository chatRepository, IUserRepository userRepository) : IChatService
    {
        public async Task CreateAsync(ChatForCreation dto)
        {
            var alreadyExistChat = await chatRepository.GetByMembersAsync(dto.FirstUserId, dto.SecondUserId);

            if (alreadyExistChat is not null)
                throw new HttpStatusCodeException(400, "Chat exist");

            await chatRepository.CreateAsync(dto.Adapt<Chat>());
        }

        public async Task DeleteAsync(Guid id)
        {
            var existChat = await chatRepository.GetByIdAsync(id)
                ?? throw new HttpStatusCodeException(404, "Chat not found");
            await chatRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ChatForView>> GetAllByUserAsync() =>
            (await chatRepository.GetAllByUserAsync((Guid)HttpContextHelper.UserId))
            .Adapt<IEnumerable<ChatForView>>();


        public async Task<ChatWithMessages> GetByIdAsync(Guid id)
        {
            var existChat = await chatRepository.GetIncludeByIdAsync(id);
            if (existChat is null)
                throw new HttpStatusCodeException(404, "User not found");

            return existChat.Adapt<ChatWithMessages>();
        }

    }
}
