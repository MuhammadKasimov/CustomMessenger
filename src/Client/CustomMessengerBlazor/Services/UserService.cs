using CustomMessenger.Service.DTO.Users;
using CustomMessengerBlazor.Exceptions;
using CustomMessengerBlazor.Helpers;
using CustomMessengerBlazor.Interfaces;
using CustomMessengerBlazor.Models.Users;
using Newtonsoft.Json;

namespace CustomMessengerBlazor.Services
{
    public class UserService : IUserService
    {
        public async Task CreateAsync(UserRegistration dto)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(dto));
                var response = await client.PostAsync($"{ApiSettings.URI}/users/register", content);
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{ApiSettings.URI}/users/{id}");
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<IEnumerable<UserForView>> GetAllAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/users?query={query}");
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());

                var users = JsonConvert.DeserializeObject<IEnumerable<UserForView>>( await response.Content.ReadAsStringAsync());

                return users;
            }
        }

        public async Task<UserForView> GetByIdAsync(Guid id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/users/{id}");
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(content);
                
                var user = JsonConvert.DeserializeObject<UserForView>(content);
                return user;
            }
        }

        public async Task<UserForView> GetByNumberAsync(string phonenumber)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/users/phone/{phonenumber}");
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(content);

                var user = JsonConvert.DeserializeObject<UserForView>(content);
                return user;
            }
        }

        public async Task<UserForView> GetByUsernameAsync(string username)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/users/username/{username}");
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(content);

                var user = JsonConvert.DeserializeObject<UserForView>(content);
                return user;
            }
        }

        public async Task<UserForView> GetSelfAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/self");
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(content);

                var user = JsonConvert.DeserializeObject<UserForView>(content);
                return user;
            }
        }

        public async Task<AccessToken> LoginAsync(UserLogin login)
        {
            using (HttpClient client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(login));
                var response = await client.PostAsync($"{ApiSettings.URI}/users/login", stringContent);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(content);

                var token = JsonConvert.DeserializeObject<AccessToken>(content);
                return token;
            }
        }

        public async Task UpdateAsync(UserForUpdate dto)
        {
            using (HttpClient client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(dto));
                var response = await client.PutAsync($"{ApiSettings.URI}/users/self",stringContent);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(content);
            }
        }
    }
}
