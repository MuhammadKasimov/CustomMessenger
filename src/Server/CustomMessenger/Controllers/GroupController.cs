using CustomMessenger.Domain.Enums;
using CustomMessenger.Service.DTO.Groups;
using CustomMessenger.Service.DTO.Users;
using CustomMessenger.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CustomMessenger.Controllers
{
    [ApiController, Route("groups")]
    [Authorize]
    public class GroupController(IGroupService groupService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GroupForCreation group)
        {
            await groupService.CreateAsync(group);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(GroupForUpdate groupForUpdate)
        {
            await groupService.UpdateAsync(groupForUpdate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await groupService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id) =>
            Ok(await groupService.GetByIdAsync(id));

        [HttpGet("uniquename/{uniquename}")]
        public async Task<IActionResult> GetByUniqueName(string uniquename) =>
            Ok(await groupService.GetByUniqueNameAsync(uniquename));
        [HttpGet]
        public async Task<IActionResult> SeearchAsync([FromQuery] string query) =>
            Ok(await groupService.SearchAsync(query));

        [HttpPost("members")]
        public async Task<IActionResult> AddMemberAsync(MemberForCreation member)
        {
            await groupService.AddMemberAsync(member);
            return Ok();
        }

        [HttpDelete("members/{id}")]
        public async Task<IActionResult> DeleteMemberAsync(Guid id)
        {
            await groupService.DeleteMemberAsync(id);
            return Ok();
        }

        [HttpPatch("members/role/{memberid}")]
        public async Task<IActionResult> ChangeRole(Guid memberid, Role role)
        {
            await groupService.ChangeRole(memberid, role);
            return Ok();
        }
    }
}
