using BlazorApp.Shared;
using BlazorApp.Shared.Utilities;

namespace BlazorApp.Client.Services.CustomerService
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
