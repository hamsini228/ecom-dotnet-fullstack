using Asp.Versioning;
using AutoMapper;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services;
using eCommerce.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[EnableCors("BajajPolicy")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(ProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }
 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await _productService.GetProductsAsync();
        if (products.Count() > 0)
        {
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
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
    public async Task<ActionResult> GetProductsPaged(
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10,
       [FromQuery] string? searchTerm = null,       
       [FromQuery] string? sortBy = "name",        
       [FromQuery] string? sortOrder = "asc")
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("pageNumber and pageSize must be greater than 0.");

        var allProducts = await _productService.GetProductsAsync();

        if (allProducts.Count() == 0)
            return NoContent();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            allProducts = allProducts.Where(p =>
                p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
            );
        }


        allProducts = sortBy?.ToLower() switch
        {
            "name" => sortOrder == "desc"
                        ? allProducts.OrderByDescending(p => p.ProductName)
                        : allProducts.OrderBy(p => p.ProductName),

            "price" => sortOrder == "desc"
                        ? allProducts.OrderByDescending(p => p.ProductName)
                        : allProducts.OrderBy(p => p.UnitPrice),

            _ => allProducts.OrderBy(p => p.ProductId) 
        };


        int totalRecords = allProducts.Count();
        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        var paged = allProducts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(new
        {
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            Products = _mapper.Map<List<ProductDto>>(paged)
        });
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProductDetails(int id)
    {
        var product = await _productService.GetProductDetailsAsync(id);
        if (product != null)
        {
            return Ok(_mapper.Map<ProductDto>(product));
        }
        else
        {
            return NoContent();
        }
    }
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CreateProductDto>> CreateProduct(CreateProductDto createProductDto)
    {
        if (ModelState.IsValid)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var result = await _productService.CreateProduct(product);
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<ActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        if (ModelState.IsValid)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var result = await _productService.UpdateProduct(product);
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        int result = await _productService.DeleteProduct(id);
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
