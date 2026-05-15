using eCommerce.Application.Contracts;
using eCommerce.Application.VMS;
using eCommerce.Domain;

namespace eCommerce.Application.Services;

public class SecurityService
{
    private readonly ISecurityRepository _securityRepository;
    private readonly ITokenManager _tokenManager;

    public SecurityService(ISecurityRepository securityRepository, ITokenManager tokenManager)
    {
        _securityRepository = securityRepository;
        _tokenManager = tokenManager;
    }
    public async Task<AuthenticationResponseVM> CheckCredential(string email , string password)
    {
        var user =await _securityRepository.AutenticateCredentialsAsync(email);
        if (user == null) 
        { 
            return new AuthenticationResponseVM()
            {
                IsAuthenticated = false,
                Message = "Inavalid Credentials!"
            };
        }
        else
        {
            var passwordVerifiication = BCrypt.Net.BCrypt.Verify(password,user.Password);
            if (!passwordVerifiication) 
            {
                return new AuthenticationResponseVM()
                {
                    IsAuthenticated = false,
                    Message = "Inavalid Credentials!"
                };
            }
            return new AuthenticationResponseVM
            {
                IsAuthenticated = true,
                Email = user.Email,
                RollName = user.Roles.ToList()[0].RoleName,
                Token = _tokenManager.GetToken(user, user.Roles.ToList()[0].RoleName),
                Message = "Authentication Sucsessfull",
                UserId =user.UserId
            };
        }
    }
}
