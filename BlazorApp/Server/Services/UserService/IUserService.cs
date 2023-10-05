using BlazorApp.Shared.UserManagement;
using BlazorApp.Shared.Utilities;

namespace BlazorApp.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<bool>> Register(UserRegister userRegister);
        Task<ServiceResponse<string>> Login(UserLogin userLogin);
    }
}
