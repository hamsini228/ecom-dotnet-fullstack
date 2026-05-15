using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart;

public class UpdateCartDto
{
    public int CartId { get; set; }
    public DateTime CartDate { get; set; } = DateTime.Now;

    public int CustomerId { get; set; }
}
