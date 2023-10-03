using BlazorApp.Shared.Utilities;
using BlazorApp.Shared;

namespace BlazorApp.Server.Services.UserService
{
    public interface ICustomerService
    {
        Task<ServiceResponse<List<Customer>>> GetAllCustomersAsync();
        Task<ServiceResponse<Customer>> GetCustomerByIDAsync(Guid customerId);
        Task<ServiceResponse<bool>> CreateCustomerAsync(Customer customer);
        Task<ServiceResponse<bool>> DeleteCustomerAsync(Guid customerId);
        Task<ServiceResponse<bool>> UpdateCustomerAsync(Customer customer);
    }
}
