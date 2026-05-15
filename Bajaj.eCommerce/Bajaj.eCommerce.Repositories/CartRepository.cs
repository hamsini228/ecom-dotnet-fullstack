using Bajaj.eCommerce.Dal;
using Bajaj.eCommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bajaj.eCommerce.Repositories;

public class CartRepository:ICartRepository
{
    private readonly eCommerceDbContext _dbContext;

    public CartRepository(eCommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Cart>> GetCartsByCustomerIdAsync(int customerId)
    {
        return await _dbContext.Carts
            .Where(c => c.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<List<YourCartVM>> GetYourCartItemsAsync(int cartId)
    {
        var cartItems = from cart in _dbContext.Carts
                        join
                        cartItem in _dbContext.CartItems
                        on cart.CartId equals cartItem.CartId
                        join
                        product in _dbContext.Products
                        on cartItem.ProductId equals product.ProductId
                        join
                        Category in _dbContext.Categories
                           on product.CategoryId equals Category.CategoryId
                        where cart.CartId == cartId
                        select new YourCartVM
                        {
                            CartItemId = cartItem.CartItemId,
                            CartId = cart.CartId,
                            CategoryName = Category.CategoryName,
                            ProductName = product.ProductName,
                            Picture = product.Picture,
                            Price = product.UnitPrice,
                            Quantity = cartItem.Quantity,
                            Size = cartItem.Size,
                            Discount =product.Discount
                        };
        return await cartItems.ToListAsync();
    }
    public async Task RemoveCartItemAsync(int cartItemId)
    {
        var item = await _dbContext.CartItems.FindAsync(cartItemId);

        if (item != null)
        {
            _dbContext.CartItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }

}
