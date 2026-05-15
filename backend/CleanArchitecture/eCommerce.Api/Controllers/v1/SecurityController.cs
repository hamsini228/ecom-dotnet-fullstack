using Asp.Versioning;
using eCommerce.Application.Contracts;
using eCommerce.Application.DTOs.User;
using eCommerce.Application.Services;
using eCommerce.Application.VMS;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class SecurityController : ControllerBase
{
    private readonly SecurityService _securitySevice;

    public SecurityController(SecurityService securitySevice)
    {
        _securitySevice = securitySevice;


    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthenticationResponseVM>> Post(UserDto user)
    {
        if (ModelState.IsValid) 
        { 
            var response = await _securitySevice.CheckCredential(user.Email, user.Password);
            if (response != null) 
                return Ok(response);
        }
        return BadRequest();
    }
}
