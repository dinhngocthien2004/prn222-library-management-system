using BusinessObjects.Entities;
using Repositoriess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicess
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo) => _repo = repo;

        public IEnumerable<Category> GetCategories() => _repo.GetCategories();
        public Category? GetCategoryById(int id) => _repo.GetCategoryById(id);
    }
}
