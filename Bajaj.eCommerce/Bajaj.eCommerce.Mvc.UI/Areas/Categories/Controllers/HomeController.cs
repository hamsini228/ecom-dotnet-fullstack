using AutoMapper;
using Bajaj.eCommerce.Entities;
using Bajaj.eCommerce.Mvc.UI.Areas.Categories.DTOs;
using Bajaj.eCommerce.Mvc.UI.Areas.Products.DTOs;
using Bajaj.eCommerce.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Bajaj.eCommerce.Mvc.UI.Areas.Categories.Controllers;
[Area("Categories")]
public class HomeController : Controller
{
    private readonly ICommonRepository<Category> _categoryRepo;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public HomeController(ICommonRepository<Category> categoryRepo, IMapper mapper, IMemoryCache memoryCache)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
        _memoryCache = memoryCache;

    }
    private IEnumerable<CategoryDto> GetCategories()
    {
        if (!_memoryCache.TryGetValue("categories", out IEnumerable<CategoryDto> categories))
        {
            categories = _mapper.Map<IEnumerable<CategoryDto>>(_categoryRepo.GetAllAsync().Result);
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(120);
            cacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(1);
            _memoryCache.Set("categories", categories, cacheEntryOptions);
        }
        return categories;
    }

    public async Task<IActionResult> Index()
    {
        //var categories =await _categoryRepo.GetAllAsync();
        var categories = GetCategories();
        return View(_mapper.Map<List<CategoryDto>>(categories));
    }
    [Authorize(Roles ="Admin")]
    public IActionResult Create()
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {

            int result = await _categoryRepo.AddAsync(category);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));

            }
        }
        else
        {
            ModelState.AddModelError(" ", "Something went wrong");
        }
        return View(category);
    }

}
