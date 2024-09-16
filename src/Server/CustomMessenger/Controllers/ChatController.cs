using CustomMessenger.Service.DTO.Chats;
using CustomMessenger.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomMessenger.Controllers
{
    [ApiController, Route("chats")]
    public class ChatController(IChatService chatService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ChatForCreation chat)
        {
            await chatService.CreateAsync(chat);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsyc(Guid id)
        {
            await chatService.DeleteAsync(id);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllByUserAsync([FromQuery] Guid userId) =>
            Ok(await chatService.GetAllByUserAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id) =>
            Ok(await chatService.GetByIdAsync(id));
    }
}
