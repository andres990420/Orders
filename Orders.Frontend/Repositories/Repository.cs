using System.Text;
using System.Text.Json;

namespace Orders.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClien;
        private readonly JsonSerializerOptions _jsonDefaultOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        public Repository(HttpClient httpClien)
        {
            _httpClien = httpClien;
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
            var responseHttp = await _httpClien.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode) 
            {
                var response = await UnSerializeAnswer<T>(responseHttp);
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<T>(default, false, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)
        {
            var messangeJson = JsonSerializer.Serialize(model);
            var messangeContent = new StringContent(messangeJson, Encoding.UTF8, "aplication/json");
            var responseHttp = await _httpClien.PostAsync(url, messangeContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model)
        {
            var messangeJson = JsonSerializer.Serialize(model);
            var messangeContent = new StringContent(messangeJson, Encoding.UTF8, "aplication/json");
            var responseHttp = await _httpClien.PostAsync(url, messangeContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnSerializeAnswer<TActionResponse>(responseHttp);
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);
        }

        private async Task<T> UnSerializeAnswer<T>(HttpResponseMessage responseHttp)
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!;
        }
    }
}