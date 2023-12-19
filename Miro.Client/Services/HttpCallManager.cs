using Miro.Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.WebRequestMethods;

namespace Miro.Client.Services
{
    public class HttpCallManager : IHttpCallManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7108/";

        public HttpCallManager(string baseApiUrl)
        {
            _httpClient = new HttpClient();
            _baseApiUrl = baseApiUrl;
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseApiUrl}{endpoint}")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonResponse);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return default(T);
            }
        }
    }
}
