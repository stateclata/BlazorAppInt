using BlazorApp.Shared.Utilities;
using BlazorApp.Server.Services.UserService;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getall"),Authorize]
        public async Task<ActionResult<ServiceResponse<List<Customer>>>> GetAllCustomersAsync()
        {
            var response = await _customerService.GetAllCustomersAsync();
            if (!response.Success)
            {
                return response;
            }
            return Ok(response);
        }

        [HttpGet("getpaged"),Authorize]
        public async Task<ActionResult<ServiceResponse<PagedList<Customer>>>> GetPagedCustomersAsync(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _customerService.GetPagedCustomersAsync(pageNumber, pageSize);
            if (!response.Success)
            {
                return response;
            }
            return Ok(response);
        }

        [HttpGet("get/{customerId}"),Authorize]
        public async Task<ActionResult<ServiceResponse<Customer>>> GetCustomerByID(Guid customerID)
        {
            var response = await _customerService.GetCustomerByIDAsync(customerID);
            if (!response.Success)
            {
                return response;
            }
            return Ok(response);
        }

        [HttpPost("create"),Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateCustomerAsync(Customer customer)
        {
            var response = await _customerService.CreateCustomerAsync(customer);
            if (!response.Success)
            {
                return response;
            }
            return Ok(response);
        }

        [HttpPost("delete"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCustomerAsync([FromBody] Guid customerId)
        {
            var response = await _customerService.DeleteCustomerAsync(customerId);
            if (!response.Success)
            {
                return response;
            }
            return Ok(response);
        }

        [HttpPost("update"),Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateCustomerAsync(Customer customer)
        {
            var response = await _customerService.UpdateCustomerAsync(customer);
            if (!response.Success)
            {
                return response;
            }
            return Ok(response);
        }
    }
}