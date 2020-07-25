using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyToes.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AtDbContext _appDbContext;

        public CategoryRepository(AtDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> GetAllCategories => _appDbContext.Categories;
    }
}