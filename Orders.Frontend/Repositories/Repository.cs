using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Orders.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonDefaultOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
            var responseHttp = await _httpClient.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnSerializeAnswerAsync<T>(responseHttp);
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<T>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)
        {
            var messangeJson = JsonSerializer.Serialize(model);
            var messangeContent = new StringContent(messangeJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messangeContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model)
        {
            var messangeJson = JsonSerializer.Serialize(model);
            var messangeContent = new StringContent(messangeJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messangeContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnSerializeAnswerAsync<TActionResponse>(responseHttp);
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model)
        {
            var messangeJson = JsonSerializer.Serialize(model);
            var messangeContent = new StringContent(messangeJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PutAsync(url, messangeContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model)
        {
            var messangeJson = JsonSerializer.Serialize(model);
            var messangeContent = new StringContent(messangeJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PutAsync(url, messangeContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnSerializeAnswerAsync<TActionResponse>(responseHttp);
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> DeleteAsync<T>(string url)
        {
            var responseHttp = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        private async Task<T> UnSerializeAnswerAsync<T>(HttpResponseMessage responseHttp)
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            Console.WriteLine(response);
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!;
        }
    }
}