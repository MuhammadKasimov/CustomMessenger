using CustomMessenger.Domain.Entities;
using CustomMessenger.Hubs;
using CustomMessenger.Service.DTO.Messages;
using CustomMessenger.Service.Helpers;
using CustomMessenger.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CustomMessenger.Controllers
{
    [Authorize]
    [ApiController,Route("messages")]
    public class MessageController(IMessageService messageService, IHubContext<ChatHub> hub) :  ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(MessageForCreation message)
        {
            await hub.Clients.All.SendAsync("ReceiveMessage", message.Content, $"{message.ChatId}|{message.GroupId}");

            await messageService.CreateAsync(message);
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> ChangeText(MessageForUpdate messageForUpdate)
        {
            await messageService.UpdateAsync(messageForUpdate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            await messageService.DeleteAsync(id);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllByChat(string search, Guid? chatId, Guid? UserId ,Guid?  groupId)
        {
            return Ok(await messageService.GetAllAsync(search, chatId, HttpContextHelper.UserId, groupId));
        }
    }
} 
