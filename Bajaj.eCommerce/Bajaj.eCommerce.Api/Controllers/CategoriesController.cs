using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Categories;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bajaj.eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICommonRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriesController(ICommonRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();
        if (categories.Count > 0)
        {
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }
        else
        {
            return NoContent();
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryDetails(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            return Ok(_mapper.Map<CategoryDto>(category));
        }
        else
        {
            return NoContent();
        }
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        if (ModelState.IsValid)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var result = await _categoryRepository.AddAsync(category);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetCategoryDetails), new
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

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<ActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        if (ModelState.IsValid)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            var result = await _categoryRepository.UpdateAsync(category);
            if (result > 0)
            {
                return NoContent();
            }
            return new ObjectResult(new { error = "Internal server error" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        return BadRequest();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        int result = await _categoryRepository.DeleteAsync(id);

        if (result > 0)
        {
            return NoContent();
        }
        return new ObjectResult(new { error = "Internal server error" })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

    }
}
