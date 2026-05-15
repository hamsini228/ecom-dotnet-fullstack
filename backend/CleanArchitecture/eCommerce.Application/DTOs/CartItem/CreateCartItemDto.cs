namespace eCommerce.Application.DTOs.CartItem;

public class CreateCartItemDto
{

    public int Quantity { get; set; }
    public int? Size { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
}
