using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain;

public class Cart
{
    [Key]
    public int CartId { get; set; }
    public DateTime CartDate { get; set; } = DateTime.Now;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<CartItem>? CartItems { get; set; }
}
