using eCommerce.Application.Contracts;
using eCommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure;

public class SecurityRepository : ISecurityRepository
{
    private readonly eCommerceDbContext _dbContext;

    public SecurityRepository(eCommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> AutenticateCredentialsAsync(string email)
    {
        return await _dbContext.Users.Include(u=>u.Roles).FirstOrDefaultAsync(u => u.Email == email);
        //var loggedInUser =(from user in _dbContext.Users
        //     where user.Email == email && user.Password == password
        //     select user).FirstOrDefault();      

    }
}
