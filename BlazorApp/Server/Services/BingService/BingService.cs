using BlazorApp.Shared;
using BlazorApp.Shared.Utilities;
using static System.Net.WebRequestMethods;

namespace BlazorApp.Server.Services.BingService
{
    public class BingService : IBingService
    {
        private readonly string _apiKey = "Av2p2_kUbT7muhJgZzpwb_qbH-JhGLfq3hzMafVJhCrR8aXqO_Lfyrodpz0bSO0Z";
        private readonly HttpClient _http;

        public BingService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponse<BingMapResponse>> SearchBing(string query)
        {
            var result = await _http.GetFromJsonAsync<BingMapResponse>($"https://dev.virtualearth.net/REST/v1/Locations?query={query}&key={_apiKey}");
            if (result == null)
            {
                return new ServiceResponse<BingMapResponse>
                {
                    Success = false,
                    Message = "Failed to contact Bing Servers"
                };
            }

            return new ServiceResponse<BingMapResponse>
            {
                Data = result
            };
        }
    }
}
