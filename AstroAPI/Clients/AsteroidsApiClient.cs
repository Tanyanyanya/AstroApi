using AstroAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstroAPI.Clients
{
    public class AsteroidsApiClient
    {
        private HttpClient _clientt;
        private static string _address;
        public static string _apiKey;

        public AsteroidsApiClient()
        {
            _address = Constants.address;
            _apiKey = Constants.apiKey;

            _clientt = new HttpClient();
            _clientt.BaseAddress = new Uri(_address);
        }

        public async Task<Near_Earth_Objects> GetAsteroidsApi(string id)
        {
            var response = await _clientt.GetAsync($"/neo/rest/v1/neo/{id}?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Near_Earth_Objects>(content);
            return result;
        }
    }
}
