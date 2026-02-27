using BusinessObjects.Entities;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _dao;
        public CategoryRepository(CategoryDAO dao) => _dao = dao;

        public IEnumerable<Category> GetCategories() => _dao.GetAll();
        public Category? GetCategoryById(int id) => _dao.GetById(id);
    }
}
