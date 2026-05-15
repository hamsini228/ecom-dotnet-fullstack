
using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Categories;
using Bajaj.eCommerce.Api.DTOs.Products;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bajaj.eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly ICommonRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(ICommonRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        if (products.Count > 0)
        {
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }
        else
        {
            return NoContent();
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("paged")]
    public async Task<ActionResult<ProductPagedDto>> GetProductsPaged(
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("pageNumber and pageSize must be greater than 0.");

        var allProducts = await _productRepository.GetAllAsync();

        if (allProducts.Count == 0)
            return NoContent();

        int totalRecords = allProducts.Count;
        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        var paged = allProducts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new ProductPagedDto
        {
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            Products = _mapper.Map<List<ProductDto>>(paged)
        };
        return Ok(result);
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProductDetails(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
        {
            return Ok(_mapper.Map<ProductDto>(product));
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
    public async Task<ActionResult<CreateProductDto>> CreateProduct(CreateProductDto createProductDto)
    {
        if (ModelState.IsValid)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var result = await _productRepository.AddAsync(product);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetProductDetails), new
                {
                    id = product.ProductId
                }, _mapper.Map<ProductDto>(product)
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
    public async Task<ActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        if (ModelState.IsValid)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var result = await _productRepository.UpdateAsync(product);
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
    public async Task<ActionResult> DeleteProduct(int id)
    {
        int result = await _productRepository.DeleteAsync(id);
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
