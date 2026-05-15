using eCommerce.Application.Contracts;
using eCommerce.Domain;


namespace eCommerce.Application.Services;

public class CartService
{
    private readonly ICommonRepository<Cart> _cartRepository;
    private readonly ICartRepository _cartSpecificRepository;

    public CartService(ICommonRepository<Cart> cartRepository , ICartRepository cartSpecificRepository)
    {
        _cartRepository = cartRepository;
        _cartSpecificRepository = cartSpecificRepository;
    }
    public async Task<Cart?> GetCartWithItemsAsync(int cartId)
    {
        return await _cartSpecificRepository.GetCartWithItemsAsync(cartId);
    }
    public async Task<IEnumerable<Cart>> GetCartsAsync()
    {
        return await _cartRepository.GetAllAsync();
    }
    public async Task<Cart> GetCartDetailsAsync(int id)
    {
        return await _cartRepository.GetByIdAsync(id);
    }
    public async Task<int> CreateCart(Cart Cart)
    {
        return await _cartRepository.AddAsync(Cart);
    }
    public async Task<int> UpdateCart(Cart Cart)
    {
        return await _cartRepository.UpdateAsync(Cart);
    }
    public async Task<int> DeleteCart(int id)
    {
        return await _cartRepository.DeleteAsync(id);
    }
}
