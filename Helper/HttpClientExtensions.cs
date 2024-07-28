using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TaskManagementUI.Helper
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(requestUri, content);
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PutAsync(requestUri, content);
        }
    }

}