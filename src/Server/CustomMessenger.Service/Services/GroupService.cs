using CustomMessenger.Data.IRepositories;
using CustomMessenger.Domain.Entities;
using CustomMessenger.Domain.Enums;
using CustomMessenger.Service.DTO.Groups;
using CustomMessenger.Service.DTO.Users;
using CustomMessenger.Service.Exceptions;
using CustomMessenger.Service.Helpers;
using CustomMessenger.Service.Interfaces;
using Mapster;

namespace CustomMessenger.Service.Services
{
    public class GroupService(IGroupRepository groupRepository, IMemberRepository memberRepository, IUserRepository userRepository) : IGroupService
    {
        public async Task CreateAsync(GroupForCreation dto)
        {
            // should be added creator
            var user = await userRepository.GetByIdAsync((Guid)HttpContextHelper.UserId);
            var existGroup = await groupRepository.GetByUniqueNameAsync(dto.UniqueName);
            if (existGroup is not null)
                throw new HttpStatusCodeException(400, "This name is taken");

            await groupRepository.CreateAsync(dto.Adapt<Group>());

            var createdGroup = await groupRepository.GetByUniqueNameAsync(dto.UniqueName);
            var member = new Member
            {
                UserId = user.Id,
                GroupId = createdGroup.Id,
                Role = Role.Creator
            };
            await memberRepository.CreateAsync(member);
        }


        public async Task UpdateAsync(GroupForUpdate dto)
        {
            var existGroup = await groupRepository.GetByUniqueNameAsync(dto.UniqueName);
            if (existGroup is not null && existGroup.Id != dto.Id)
                throw new HttpStatusCodeException(400, "This name is taken");

            var existUser = await memberRepository.GetByIdsAsync((Guid)HttpContextHelper.UserId, dto.Id);
            if (existUser.Role == Role.Member)
                throw new HttpStatusCodeException(403, "You don't have permission to change group data");

            await groupRepository.UpdateAsync(dto.Adapt<Group>());
        }

        public async Task DeleteAsync(Guid id)
        {
            var existGroup = await groupRepository.GetByIdAsync(id)
                ?? throw new HttpStatusCodeException(404, "Group not found");

            var existUser = await memberRepository.GetByIdsAsync((Guid)HttpContextHelper.UserId, id);
            if (existUser.Role != Role.Creator)
                throw new HttpStatusCodeException(403, "You don't have permission to delete group");

            await groupRepository.DeleteAsync(id);
        }

        public async Task<SingleGroupForView> GetByIdAsync(Guid id)
        {
            var existGroup = await groupRepository.GetByIdAsync(id)
                ?? throw new HttpStatusCodeException(404, "Group not found");

            return existGroup.Adapt<SingleGroupForView>();
        }

        public async Task<SingleGroupForView> GetByUniqueNameAsync(string uniquename)
        {
            var existGroup = await groupRepository.GetByUniqueNameAsync(uniquename)
                ?? throw new HttpStatusCodeException(404, "Group not found");

            return existGroup.Adapt<SingleGroupForView>();
        }

        public async Task<IEnumerable<GroupForView>> SearchAsync(string query)
        {
            var groups = await groupRepository.SearchAsync(query);

            return groups.Adapt<IEnumerable<GroupForView>>();
        }

        public async Task AddMemberAsync(MemberForCreation dto)
        {
            var existMember = await memberRepository.GetByIdsAsync(dto.UserId, dto.GroupId);
            if (existMember != null)
                throw new HttpStatusCodeException(400, "User is not group member");

            var existUser = await memberRepository.GetByIdsAsync((Guid)HttpContextHelper.UserId, dto.GroupId)
                ?? throw new HttpStatusCodeException(401, "You are not group member");
            if (existUser.Role == Role.Member)
                throw new HttpStatusCodeException(403, "You don't have permission to add new members");

            await memberRepository.CreateAsync(existMember);
        }
        public async Task DeleteMemberAsync(Guid id)
        {
            var existMember = await memberRepository.GetByIdAsync(id)
                ?? throw new HttpStatusCodeException(404, "Member not found");

            existMember = await memberRepository.GetByIdsAsync((Guid)HttpContextHelper.UserId, existMember.GroupId)
                ?? throw new HttpStatusCodeException(401, "You are not group member");
            if (existMember.Role == Role.Member)
                throw new HttpStatusCodeException(403, "You don't have permission to delete members");

            await memberRepository.DeleteAsync(id);
        }
        public async Task ChangeRole(Guid memberid, Role role)
        {
            var existMember = await memberRepository.GetByIdAsync(memberid);
            if (existMember is null)
                throw new HttpStatusCodeException(404, "User not in this groups");

            existMember = await memberRepository.GetByIdsAsync((Guid)HttpContextHelper.UserId, existMember.GroupId)
                ?? throw new HttpStatusCodeException(401, "You are not group member");

            if (existMember.Role == Role.Member)
                throw new HttpStatusCodeException(403, "You don't have permission to delete members");

            await memberRepository.ChangeRoleAsync(memberid, role);
        }
    }
}