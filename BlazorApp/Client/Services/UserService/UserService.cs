using BlazorApp.Shared;
using BlazorApp.Shared.UserManagement;
using BlazorApp.Shared.Utilities;
using System.Net.Http.Json;

namespace BlazorApp.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponse<string>> Login(UserLogin userLogin)
        {
            var result = await _http.PostAsJsonAsync("api/user/login", userLogin);
            var resultContent = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if (resultContent == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Unable to contact server"
                };
            }
            return resultContent;
        }

        public async Task<ServiceResponse<bool>> Register(UserRegister userRegister)
        {
            var result = await _http.PostAsJsonAsync("api/user/register", userRegister);
            var resultContent = await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            if (resultContent == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Unable to contact server"
                };
            }
            return resultContent;
        }
    }
}
