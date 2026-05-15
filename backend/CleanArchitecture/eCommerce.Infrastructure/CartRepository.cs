using eCommerce.Application.Contracts;
using eCommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure;

public class CartRepository :ICartRepository
{
    private readonly eCommerceDbContext _dbContext;

    public CartRepository(eCommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Cart?> GetCartWithItemsAsync(int cartId)
    {
        return await _dbContext.Carts.Include(c => c.CartItems)
                                      .ThenInclude(ci => ci.Product)
                                      .FirstOrDefaultAsync(c => c.CartId == cartId);
    }

}
