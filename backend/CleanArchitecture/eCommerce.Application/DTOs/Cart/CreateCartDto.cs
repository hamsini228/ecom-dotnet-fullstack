using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart;

public class CreateCartDto
{
    public DateTime CartDate { get; set; } = DateTime.Now;

    public int CustomerId { get; set; }
}
