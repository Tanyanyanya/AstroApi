using AstroAPI.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstroAPI.Clients
{
    public class PicOfDayClient
    {
        private HttpClient _client;
        private static string _address;
        public static string _apiKey;

        public PicOfDayClient()
        {
            _address = Constants.address;
            _apiKey = Constants.apiKey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }

        public async Task<PicOfDay> GetSpaceWeather()
        {
            var response = await _client.GetAsync($"/planetary/apod?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<PicOfDay>(content);
            return result;
        }
    }
    
}
