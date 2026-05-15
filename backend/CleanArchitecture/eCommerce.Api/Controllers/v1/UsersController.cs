using Asp.Versioning;
using AutoMapper;
using eCommerce.Application.DTOs.User;
using eCommerce.Application.Services;
using eCommerce.Domain;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IMapper _mapper;

    public UsersController(UserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUserWithRole(CreateUserDto user ,int roleId)
    {
        var user1 = _mapper.Map<User>(user);

        if (ModelState.IsValid)
        {
            var result = await _userService.CreateUserAsync(user1, roleId);
            if(result == true)
            {
                return Ok(new { message = "User created sucessfully" });
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return BadRequest();
    }
}
