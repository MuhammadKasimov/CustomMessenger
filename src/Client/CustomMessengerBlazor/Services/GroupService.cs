using CustomMessenger.Domain.Enums;
using CustomMessenger.Service.DTO.Groups;
using CustomMessenger.Service.DTO.Users;
using CustomMessengerBlazor.Exceptions;
using CustomMessengerBlazor.Helpers;
using CustomMessengerBlazor.Interfaces;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net;

namespace CustomMessengerBlazor.Services
{
    public class GroupService : IGroupService
    {
        #region Group
        public async Task CreateAsync(GroupForCreation dto)
        {
            using (var client = new HttpClient())
            {
                var strContent = new StringContent(JsonConvert.SerializeObject(dto));
                var response = await client.PostAsync($"{ApiSettings.URI}/groups", strContent);

                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task UpdateAsync(GroupForUpdate dto)
        {
            using (var client = new HttpClient())
            {
                var strContent = new StringContent(JsonConvert.SerializeObject(dto));
                var response = await client.PutAsync($"{ApiSettings.URI}/groups", strContent);

                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{ApiSettings.URI}/groups/{id}");

                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }


        public async Task<SingleGroupForView> GetByIdAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/groups/{id}");

                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SingleGroupForView>(content);
            }
        }

        public async Task<SingleGroupForView> GetByUniqueNameAsync(string uniquename)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/{uniquename}");

                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SingleGroupForView>(content);
            }
        }

        public async Task<IEnumerable<GroupForView>> SearchAsync(string query)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/query");
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<GroupForView>>(content);
            }
        }

        public async Task AddMemberAsync(MemberForCreation member)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(member));
                var response = await client.PostAsync($"{ApiSettings.URI}/members", content);
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }
        #endregion

        #region Member
        public Task DeleteMemberAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task ChangeRole(Guid memberid, Role role)
        {
            using (var client = new HttpClient())
            {

                var strContent = new StringContent(role.ToString());
                var formData = new MultipartFormDataContent
                {
                    { strContent, nameof(role) }
                };

                var response = await client.PatchAsync($"{ApiSettings.URI}/members/{memberid}", formData);
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }
        #endregion
    }
}
