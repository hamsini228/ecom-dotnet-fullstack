namespace Bajaj.eCommerce.Api.DTOs.Carts;

public class UpdateCartDto
{
    public int CartId { get; set; }
    public DateTime CartDate { get; set; } = DateTime.Now;

    public int CustomerId { get; set; }
}
