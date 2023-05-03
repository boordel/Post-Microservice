using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Api.Application.Queries.Category;
using Post.Domain.Entities.CategoryAggregate;

namespace Post.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
	private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryList>>> GetCategoriesAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAll();
            if (categories == null)
                return NotFound();
            else
            {
                var list = (from c in categories
                            select new CategoryList
                            {
                                Id = c.Id,
                                Title = c.Title,
                                PostCount = 1
                            });

                return Ok(list);
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "No category found");
        }
    }
}
