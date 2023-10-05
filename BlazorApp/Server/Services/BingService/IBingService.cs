using BlazorApp.Shared;
using BlazorApp.Shared.Utilities;

namespace BlazorApp.Server.Services.BingService
{
    public interface IBingService
    {
        Task<ServiceResponse<BingMapResponse>> SearchBing(string query);
    }
}
