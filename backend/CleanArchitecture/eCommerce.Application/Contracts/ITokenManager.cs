using eCommerce.Domain;

namespace eCommerce.Application.Contracts;

public interface ITokenManager
{
    string GetToken(User user,string roleName);
}
