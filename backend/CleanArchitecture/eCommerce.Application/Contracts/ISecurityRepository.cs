
using eCommerce.Domain;

namespace eCommerce.Application.Contracts;

public interface ISecurityRepository
{
    Task<User> AutenticateCredentialsAsync(string email);

}
