using eCommerce.Application.Contracts;
using eCommerce.Domain;

namespace eCommerce.Application.Services;

public  class CustomerService
{
    private readonly ICommonRepository<Customer> _customerRepository;

    public CustomerService(ICommonRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();
    }
    public async Task<Customer> GetCustomerDetailsAsync(int id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }
    public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.FirstOrDefault(c => c.UserId == userId);
    }
    public async Task<int> CreateCustomer(Customer customer)
    {
        return await _customerRepository.AddAsync(customer);
    }
    public async Task<int> UpdateCustomer(Customer customer)
    {
        return await _customerRepository.UpdateAsync(customer);
    }
    public async Task<int> DeleteCustomer(int id)
    {
        return await _customerRepository.DeleteAsync(id);
    }
}
