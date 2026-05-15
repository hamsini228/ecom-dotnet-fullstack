
using eCommerce.Application.Contracts;
using eCommerce.Domain;
using System.Collections.Generic;

namespace eCommerce.Application.Services;

public class CategoryService
{
    private readonly ICommonRepository<Category> _categoryRepository;

    public CategoryService(ICommonRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }
    public async Task<Category> GetCategoryDetailsAsync(int id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }
    public async Task<int> CreateCategory(Category category)
    {
        return await _categoryRepository.AddAsync(category);
    }
    public async Task<int> UpdateCategory(Category category)
    {
        return await _categoryRepository.UpdateAsync(category);
    }
    public async Task<int> DeleteCategory(int id)
    {
        return await _categoryRepository.DeleteAsync(id);
    }
}
