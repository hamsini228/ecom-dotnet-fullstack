namespace eCommerce.Application.DTOs.CartItem;

public class CartItemDto
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
    public int? Size { get; set; }

    public int ProductId { get; set; }
    public int CartId { get; set; }
}
