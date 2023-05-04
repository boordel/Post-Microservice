using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities.CategoryAggregate;
using Post.Domain.SeedWorks;
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

    public async Task<Category?> GetByIdAsync(int id) =>
        await _context
            .Set<Category>()
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Category>?> GetAllAsync() =>
        await _context
            .Set<Category>()
            .ToListAsync();

    public Category Insert(Category entity) =>
        _context
            .Set<Category>()
            .Add(entity)
            .Entity;

    public Category Update(Category entity) =>
        _context
            .Set<Category>()
            .Update(entity)
            .Entity;

    public async Task DeleteByIdAsync(int id)
    {
        var category = await _context
                                .Set<Category>()
                                .SingleOrDefaultAsync(c => c.Id == id);

        if (category != null)
            _context
                .Set<Category>()
                .Remove(category);
    }
}
