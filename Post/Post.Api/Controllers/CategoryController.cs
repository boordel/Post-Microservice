using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Post.Api.Application.Queries.Category;
using Post.Domain.Entities.CategoryAggregate;
using System.Collections.Generic;
using System.Net;

namespace Post.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
	private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _memoryCache;

    public CategoryController(ICategoryRepository categoryRepository, IMemoryCache memoryCache)
    {
        _categoryRepository = categoryRepository;
        _memoryCache = memoryCache;
    }

    private const string CacheKey_CategoryList = "ctg_list";
    private const int CacheValidationMinutes = 5;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        try
        {
            var categories = await _memoryCache.GetOrCreateAsync<IEnumerable<Category>>(CacheKey_CategoryList, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(CacheValidationMinutes);
                return await _categoryRepository.GetAll();
            });

            if (categories == null)
                return NotFound();
            else
                return Ok(categories);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "No category found");
        }
    }
}
