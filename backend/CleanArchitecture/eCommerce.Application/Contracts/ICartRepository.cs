using eCommerce.Domain;

namespace eCommerce.Application.Contracts;

public interface ICartRepository
{
    //Task<Cart?> GetCartByCustomerIdAsync(int customerId);
    Task<Cart?> GetCartWithItemsAsync(int cartId);
}
