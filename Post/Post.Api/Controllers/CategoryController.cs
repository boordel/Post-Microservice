using Microsoft.AspNetCore.Mvc;
using Post.Api.Application.Repositories;
using Post.Domain.Entities.CategoryAggregate;
using System.Net;

namespace Post.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CachedCategoryRepository _categoryRepository;

    public CategoryController(CachedCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        try
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

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
