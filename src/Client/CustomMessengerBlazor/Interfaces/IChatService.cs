﻿using CustomMessenger.Service.DTO.Chats;

namespace CustomMessengerBlazor.Interfaces
{
    public interface IChatService
    {
        Task CreateAsync(ChatForCreation dto);
        Task DeleteAsync(Guid id);
        Task<ChatWithMessages> GetByIdAsync(Guid id);
        Task<IEnumerable<ChatForView>> GetAllByUserAsync();
    }
}