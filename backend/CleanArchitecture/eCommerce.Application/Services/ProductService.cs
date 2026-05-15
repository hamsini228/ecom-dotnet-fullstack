using eCommerce.Application.Contracts;
using eCommerce.Domain;

namespace eCommerce.Application.Services;

public class ProductService
{
    private readonly ICommonRepository<Product> _productRepository;

    public ProductService(ICommonRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }
    public async Task<Product> GetProductDetailsAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }
    public async Task<int> CreateProduct(Product product)
    {
        return await _productRepository.AddAsync(product);
    }
    public async Task<int> UpdateProduct(Product product)
    {
        return await _productRepository.UpdateAsync(product);
    }
    public async Task<int> DeleteProduct(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }
}
