using BlazorApp.Shared;
using BlazorApp.Shared.Utilities;

namespace BlazorApp.Client.Services.BingService
{
    public class BingService : IBingService
    {
        private readonly HttpClient _http;


        public BingService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<BingMapResponse>> SearchBingAsync(string query)
        {
            var result = await _http.PostAsJsonAsync($"/api/bing/create", query);
            var resultContent = await result.Content.ReadFromJsonAsync<ServiceResponse<BingMapResponse>>();
            if (resultContent == null)
            {
                return new ServiceResponse<BingMapResponse>
                {
                    Success = false,
                    Message = "Unable to contact server"
                };
            }
            return resultContent;
        }
    }
}
