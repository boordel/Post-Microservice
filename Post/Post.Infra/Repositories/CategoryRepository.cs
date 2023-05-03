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

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Category entity)
    {
        throw new NotImplementedException();
    }
}
