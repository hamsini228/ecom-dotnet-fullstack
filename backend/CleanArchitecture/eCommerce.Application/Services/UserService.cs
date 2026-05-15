using eCommerce.Application.Contracts;
using eCommerce.Application.DTOs.User;
using eCommerce.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.Services;

public class UserService
{
    private readonly ICommonRepository<User> _userRepository;
    private readonly ICommonRepository<Role> _roleRepository;
    private readonly CustomerService _customerService;

    public UserService(ICommonRepository<User> userRepository, CustomerService customerService, ICommonRepository<Role> roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _customerService = customerService;
    }

    public async Task<bool> CreateUserAsync(User user, int roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
        {
            throw new ArgumentException("Role not found");
        }
        var newUser = new User
        {
            Email = user.Email,
            Password = user.Password,
            Roles = new List<Role>()
        };
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        newUser.Password = passwordHash;
        newUser.Roles.Add(role);
        var result = await _userRepository.AddAsync(newUser);
        if (result > 0)
        {
            var customer = new Customer
            {
                UserId = newUser.UserId,
                Email = newUser.Email,
                ContactName = "New Customer",
                Address = "ibis pune,viman nagar",
                City = "pune",
                Phone = "9989382040",
                Zipcode = 506111
            };

            await _customerService.CreateCustomer(customer);
        }
        return result > 0;
    }
}
