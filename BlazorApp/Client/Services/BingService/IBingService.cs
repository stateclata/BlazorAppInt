using BlazorApp.Shared;
using BlazorApp.Shared.Utilities;

namespace BlazorApp.Client.Services.BingService
{
    public interface IBingService
    {
        Task<ServiceResponse<BingMapResponse>> SearchBingAsync(string query);
    }
}
