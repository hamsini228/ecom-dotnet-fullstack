namespace eCommerce.Application.DTOs.CartItem;

public class UpdateCartItemDto
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
    public int? Size { get; set; }

    public int ProductId { get; set; }
    public int CartId { get; set; }
}
