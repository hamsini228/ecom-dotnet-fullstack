using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace eCommerce.Domain;

public class Role
{
    [Required]
    public int RoleId { get; set; }
    [Required(ErrorMessage = "Role name is mandatory")]
    [MaxLength(50)]
    public string RoleName { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;

    public ICollection<User>? Users { get; set; }
}
