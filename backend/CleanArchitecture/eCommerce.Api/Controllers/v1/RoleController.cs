using Asp.Versioning;
using AutoMapper;
using eCommerce.Application.DTOs.Role;
using eCommerce.Application.Services;
using eCommerce.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[EnableCors("BajajPolicy")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;
    private readonly IMapper _mapper;

    public RoleController(RoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
    {
        var roles = await _roleService.GetRolesAsync();
        if (roles.Count() > 0)
        {
            return Ok(_mapper.Map<IEnumerable<RoleDto>>(roles));
        }
        else
        {
            return NoContent();
        }
    }


    

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RoleDto>> GetRoleDetails(int id)
    {
        var role = await _roleService.GetRoleDetailsAsync(id);
        if (role != null)
        {
            return Ok(_mapper.Map<RoleDto>(role));
        }
        else
        {
            return NoContent();
        }
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CreateRoleDto>> CreateRole(CreateRoleDto createRoleDto)
    {
        if (ModelState.IsValid)
        {
            var role = _mapper.Map<Role>(createRoleDto);
            var result = await _roleService.CreateRole(role);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetRoleDetails), new
                {
                    id = role.RoleId
                }, _mapper.Map<RoleDto>(role)
                );
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return BadRequest();
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<ActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
    {
        if (ModelState.IsValid)
        {
            var role = _mapper.Map<Role>(updateRoleDto);
            var result = await _roleService.UpdateRole(role);
            if (result > 0)
            {
                return NoContent();
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return BadRequest();
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteRole(int id)
    {
        int result = await _roleService.DeleteRole(id);
        if (result > 0)
        {
            return NoContent();
        }
        return new ObjectResult(new { error = "Internal server error" })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };


    }
}
