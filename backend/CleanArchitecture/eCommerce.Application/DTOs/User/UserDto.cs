using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.User;

public class UserDto
{
    [Required(ErrorMessage = "Email is required")]
    [MaxLength(100)]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

}
