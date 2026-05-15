using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required(ErrorMessage ="Email is required")]
    [MaxLength(100)]
    [EmailAddress(ErrorMessage ="Invalid email format")]
    public string Email { get; set; }= string.Empty;
    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    public ICollection<Role>? Roles { get; set; }

    public Customer? Customer { get; set; }

}
