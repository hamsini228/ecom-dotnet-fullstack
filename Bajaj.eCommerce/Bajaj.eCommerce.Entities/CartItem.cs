using System.ComponentModel.DataAnnotations;

namespace Bajaj.eCommerce.Entities;

public class CartItem
{
    [Key]
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
    public int Size { get; set; }

    public int ProductId {  get; set; }
    public Product Product { get; set; }


    public int CartId {  get; set; }
    public Cart Cart { get; set; }

}
