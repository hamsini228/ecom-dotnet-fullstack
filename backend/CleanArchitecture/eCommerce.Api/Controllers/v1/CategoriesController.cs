using Asp.Versioning;
using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.Services;
using eCommerce.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace eCommerce.Api.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[EnableCors("BajajPolicy")]
[Route("api/v{version:apiVersion}/[controller]")]


public class CategoriesController(CategoryService _categoryService ,IMapper _mapper ,IMemoryCache _cache, ILogger<CategoriesController> _logger) : ControllerBase
{
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllAsync()
    {
        _logger.LogInformation("Get all categories");
        if (!_cache.TryGetValue("categories", out IEnumerable<CategoryDto> cachedCategories))
        {
            var categories = await _categoryService.GetCategoriesAsync();

            if (!categories.Any())
                return NoContent();
            cachedCategories = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(10);
            cacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(1);
            _cache.Set("categories", cachedCategories, cacheEntryOptions);
        }
        return Ok(cachedCategories);
    }
    //public async Task<ActionResult<IEnumerable<CategoryDto>>> GetallAsync()
    //{
    //    var categories = await _categoryService.GetCategoriesAsync();
    //    if(categories.Count() > 0)
    //    {
    //        return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
    //    }
    //    return NoContent();

    //}


    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching category {CategoryId}", id);
        var category = await _categoryService.GetCategoryDetailsAsync(id);
        if (category != null)
        {
            return Ok(_mapper.Map<CategoryDto>(category));
        }
        _logger.LogWarning("Category {CategoryId} not found", id);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        if (ModelState.IsValid)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var result = await _categoryService.CreateCategory(category);
            if (result > 0)
            {
                _cache.Remove("categories");
                return CreatedAtAction(nameof(GetById), new
                {
                    id = category.CategoryId
                }, _mapper.Map<CategoryDto>(category)
                );
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }
        return BadRequest();
    }

    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<ActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        if (ModelState.IsValid)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            var result = await _categoryService.UpdateCategory(category);
            if (result > 0)
            {
                _cache.Remove("categories");
                return NoContent();
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return BadRequest();
    }


    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        int result = await _categoryService.DeleteCategory(id);

        if (result > 0)
        {
            _cache.Remove("categories");
            return NoContent();
        }
        return new ObjectResult(new { error = "Internal server error" })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

    }
}
