using AutoMapper;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Mvc.UI.Areas.Products.DTOs;
using Bajaj.eCommerce.Mvc.UI.Filters;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


namespace Bajaj.eCommerce.Mvc.UI.Areas.Products.Controllers;
[Area("Products")]
//[BajajController]
public class HomeController : Controller
{
    private readonly ICommonRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    public HomeController(ICommonRepository<Product> ProductRepo, IMapper mapper , IMemoryCache memoryCache)
    {
        _productRepository = ProductRepo;
        _mapper = mapper;
        _memoryCache =memoryCache; 
    }

    //private IEnumerable<ProductDto> GetProducts()
    //{
    //    if (!_memoryCache.TryGetValue("products", out IEnumerable<ProductDto> products))
    //    {
    //        products = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.GetAllAsync().Result);
    //        var cacheEntryOptions = new MemoryCacheEntryOptions();
    //        cacheEntryOptions.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(120);
    //        cacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(1);
    //        _memoryCache.Set("products", products, cacheEntryOptions);
    //    }
    //    return products;
    //}
    private async Task<IEnumerable<ProductDto>> GetProducts()
    {
        if (!_memoryCache.TryGetValue("products", out IEnumerable<ProductDto> products))
        {
            var data = await _productRepository.GetAllAsync();
            products = _mapper.Map<IEnumerable<ProductDto>>(data);

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(120),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };

            _memoryCache.Set("products", products, cacheEntryOptions);
        }

        return products;
    }
    public async Task<IActionResult> Index(int page = 1, string sort = "name_asc",string search="")
    {
        int pageSize = 8;
        var products = await GetProducts();

        if (!string.IsNullOrWhiteSpace(search))
            products = products.Where(p =>
                p.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(search, StringComparison.OrdinalIgnoreCase));

        // Apply sorting
        products = sort switch
        {
            "name_desc" => products.OrderByDescending(p => p.ProductName),
            "price_asc" => products.OrderBy(p => p.UnitPrice),
            "price_desc" => products.OrderByDescending(p => p.UnitPrice),
            _ => products.OrderBy(p => p.ProductName), // default: name_asc
        };

        var totalItems = products.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var pagedProducts = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.CurrentSort = sort;   // ← needed by the view's dropdown & pagination links

        return View(pagedProducts);
    }
    //public async Task<IActionResult> Index()
    //{
    //    //var products = await _productRepository.GetAllAsync();
    //    var products = GetProducts();
    //    return View(_mapper.Map<List<ProductDto>>(products));
    //}
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return View(_mapper.Map<ProductDetailsDTO>(product));

    }
    public async Task<IActionResult> CategoryProducts(int cateogeryid )
    {
        //var products =await _productRepository.GetAllAsync();
        //return View("Index",_mapper.Map<List<ProductDto>>(properties.Where(p=>p.CategoryId==cateogeryid)));
        var products =await GetProducts();
        var filtered = products.Where(p => p.CategoryId == cateogeryid);

        return View("Index", filtered);
    }
}
