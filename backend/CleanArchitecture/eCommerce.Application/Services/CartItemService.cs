using eCommerce.Application.Contracts;
using eCommerce.Domain;

namespace eCommerce.Application.Services;

public class CartItemService
{
    private readonly ICommonRepository<CartItem> _cartItemRepository;

    public CartItemService(ICommonRepository<CartItem> cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
    {
        return await _cartItemRepository.GetAllAsync();
    }
    public async Task<CartItem> GetCartItemDetailsAsync(int id)
    {
        return await _cartItemRepository.GetByIdAsync(id);
    }
    public async Task<int> CreateCartItem(CartItem cartItem)
    {
        return await _cartItemRepository.AddAsync(cartItem);
    }
    public async Task<int> UpdateCartItem(CartItem cartItem)
    {
        return await _cartItemRepository.UpdateAsync(cartItem);
    }
    public async Task<int> DeleteCartItem(int id)
    {
        return await _cartItemRepository.DeleteAsync(id);
    }
}
