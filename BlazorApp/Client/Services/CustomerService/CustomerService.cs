using BlazorApp.Shared;
using BlazorApp.Shared.Utilities;
using System.Net.Http.Json;

namespace BlazorApp.Client.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _http;

        public CustomerService(HttpClient http)
        {
            _http = http;
        }


        public async Task<ServiceResponse<bool>> CreateCustomerAsync(Customer customer)
        {
            var result = await _http.PostAsJsonAsync($"/api/customer/create", customer);
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

        public async Task<ServiceResponse<bool>> DeleteCustomerAsync(Guid customerId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<bool>>($"/api/customer/delete/{customerId}");
            if (result == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Unable to contact server"
                };
            }
            return result;
        }

        public async Task<ServiceResponse<List<Customer>>> GetAllCustomersAsync()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Customer>>>("/api/customer/getall");
            if (result == null)
            {
                return new ServiceResponse<List<Customer>>
                {
                    Success = false,
                    Message = "Unable to contact server"
                };
            }
            return result;
        }

        public async Task<ServiceResponse<Customer>> GetCustomerByIDAsync(Guid customerId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Customer>>($"/api/customer/get/{customerId}");
            if (result == null)
            {
                return new ServiceResponse<Customer>
                {
                    Success = false,
                    Message = "Unable to contact server"
                };
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateCustomerAsync(Customer customer)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<bool>>($"/api/customer/update/{customer}");
            if (result == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Unable to contact server"
                };
            }
            return result;
        }
    }
}
