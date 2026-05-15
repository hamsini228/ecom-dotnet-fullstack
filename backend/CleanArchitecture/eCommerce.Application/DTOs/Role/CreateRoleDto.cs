using eCommerce.Domain;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Role;

public class CreateRoleDto
{
    [Required(ErrorMessage = "Role name is mandatory")]
    [MaxLength(50)]
    public string RoleName { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
}
