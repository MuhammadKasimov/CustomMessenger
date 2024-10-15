using CustomMessenger.Service.DTO.Chats;
using CustomMessengerBlazor.Exceptions;
using CustomMessengerBlazor.Helpers;
using CustomMessengerBlazor.Interfaces;
using Newtonsoft.Json;

namespace CustomMessengerBlazor.Services
{
    public class ChatService : IChatService
    {
        public async Task CreateAsync(ChatForCreation dto)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(dto));
                var response = await client.PostAsync($"{ApiSettings.URI}/chats", stringContent);

                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());

            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{ApiSettings.URI}/chats/{id}");

                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<IEnumerable<ChatForView>> GetAllByUserAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ApiSettings.URI);
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<ChatForView>>(content);
            }
        }

        public async Task<ChatWithMessages> GetByIdAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/{id}");
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ChatWithMessages>(content);
            }
        }
    }
    }
}
