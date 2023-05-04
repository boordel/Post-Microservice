using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infra.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly PostDBContext _context;

    public CategoryRepository(PostDBContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context
                        .Categories
                        .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context
                        .Categories
                        .ToListAsync();
    }

    public Category Insert(Category entity)
    {
        return _context
                .Categories
                .Add(entity)
                .Entity;
    }

    public Category Update(Category entity)
    {
        return _context
                .Categories
                .Update(entity)
                .Entity;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var category = await _context
                                .Categories
                                .SingleOrDefaultAsync(c => c.Id == id);

        if (category != null)
            _context
                .Categories
                .Remove(category);
    }
}
