using BlazorAADE.Server.Data;
using BlazorApp.Shared.Utilities;
using BlazorApp.Server.Services.UserService;
using BlazorApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that Creates a new customer
        /// </summary>
        public async Task<ServiceResponse<bool>> CreateCustomerAsync(Customer customer)
        {
            try
            {
                var customerToCreate = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
                if (customerToCreate != null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "The customer already exists"
                    };
                }

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = "Customer created successfully"
                };
            }
            catch
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
        }

        /// <summary>
        /// Method that Deletes an existing customer
        /// </summary>
        public async Task<ServiceResponse<bool>> DeleteCustomerAsync(Guid customerId)
        {
            try
            {
                var customerToDelete = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
                if (customerToDelete == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "The customer does not exist"
                    };
                }

                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();

                return new ServiceResponse<bool>
                {
                    Message = "Customer deleted successfully"
                };
            }
            catch
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
        }

        /// <summary>
        /// Method that returns a list containing all the entries in the Customer table of the database
        /// </summary>
        public async Task<ServiceResponse<List<Customer>>> GetAllCustomersAsync()
        {
            try
            {
                var customerList = await _context.Customers.ToListAsync();
                if (customerList.Any())
                {
                    return new ServiceResponse<List<Customer>>
                    {
                        Data = customerList
                    };
                }

                return new ServiceResponse<List<Customer>>
                {
                    Message = "Customers table is empty"
                };
            }
            catch
            {
                return new ServiceResponse<List<Customer>>
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
        }

        /// <summary>
        /// Method that returns a paged list containing selected entries in the Customer table of the database
        /// </summary>
        public async Task<ServiceResponse<PagedList<Customer>>> GetPagedCustomersAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalCustomers = await _context.Customers.CountAsync();

                var customerList = await _context.Customers
                    .OrderBy(c => c.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalPages = (int)Math.Ceiling((double)totalCustomers / pageSize);

                if (customerList.Any())
                {
                    var response = new ServiceResponse<PagedList<Customer>>();
                    response.Data = new PagedList<Customer>
                    {
                        Items = customerList,
                        PageSize = pageSize,
                        PageNumber = pageNumber,
                        TotalPages = totalPages
                    };


                    return response;
                }

                return new ServiceResponse<PagedList<Customer>>
                {
                    Message = "Customers table is empty"
                };
            }
            catch
            {
                return new ServiceResponse<PagedList<Customer>>
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
        }

        /// <summary>
        /// Method that returns a certain customer based on the customer Guid provided
        /// </summary>
        public async Task<ServiceResponse<Customer>> GetCustomerByIDAsync(Guid customerId)
        {
            try
            {
                var customerToFetch = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
                if (customerToFetch == null)
                {
                    return new ServiceResponse<Customer>
                    {
                        Success = false,
                        Message = "Customer does not exist"
                    };
                }

                return new ServiceResponse<Customer>
                {
                    Data = customerToFetch
                };

            }
            catch
            {
                return new ServiceResponse<Customer>
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
        }

        /// <summary>
        /// Method that Updates an existing customer
        /// </summary>
        public async Task<ServiceResponse<bool>> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                var customerToUpdate = await _context.Customers
                    .AsNoTracking() // Detach the entity from the context
                    .FirstOrDefaultAsync(c => c.Id == customer.Id);

                if (customerToUpdate == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Customer does not exist"
                    };
                }

                _context.Entry(customerToUpdate).CurrentValues.SetValues(customer);
                _context.Update(customerToUpdate);
                await _context.SaveChangesAsync();

                return new ServiceResponse<bool>
                {
                    Message = "Customer updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "An error occurred while updating the customer."
                };
            }
        }

    }
}
