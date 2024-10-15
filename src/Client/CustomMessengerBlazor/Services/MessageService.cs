using CustomMessenger.Service.DTO.Messages;
using CustomMessengerBlazor.Exceptions;
using CustomMessengerBlazor.Helpers;
using CustomMessengerBlazor.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CustomMessengerBlazor.Services
{
    public class MessageService : IMessageService
    {
        public async Task CreateAsync(MessageForCreation message)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(message));
                var response = await client.PostAsync($"{ApiSettings.URI}/messages/send", content);
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{ApiSettings.URI}/messages/{id}");
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<IEnumerable<MessageForView>> GetAllAsync(string search = null, Guid? chatId = null, Guid? userId = null, Guid? groupId = null)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiSettings.URI}/messages/");
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
                
                var messages = JsonConvert.DeserializeObject<IEnumerable<MessageForView>>(await response.Content.ReadAsStringAsync());

                return messages;
            }
        }

        public async Task UpdateAsync(MessageForUpdate messageForUpdate)
        {
            using (HttpClient client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(messageForUpdate));
                var response = await client.PutAsync($"{ApiSettings.URI}/messages/self", stringContent);
                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<HttpStatusCodeException>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
