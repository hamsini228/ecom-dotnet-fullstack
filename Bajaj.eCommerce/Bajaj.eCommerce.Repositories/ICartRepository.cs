namespace Bajaj.eCommerce.Repositories;
using Bajaj.eCommerce.Entities;

public interface ICartRepository
{
    Task<List<YourCartVM>> GetYourCartItemsAsync(int cartId);
    Task RemoveCartItemAsync(int cartItemId);
    Task<List<Cart>> GetCartsByCustomerIdAsync(int customerId);
}
