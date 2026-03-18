using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Library_Management_System.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categories;
        private readonly IBookService _books;

        public CategoriesController(ICategoryService categories, IBookService books)
        {
            _categories = categories;
            _books = books;
        }

        // ================================
        // LIST CATEGORY
        // ================================
        public IActionResult Index()
        {
            var list = _categories.GetCategories();
            return View(list);
        }

        // ================================
        // BOOKS BY CATEGORY
        // ================================
        public IActionResult Books(int id)
        {
            var books = _books.GetBooks()
                              .Where(b => b.CategoryId == id)
                              .ToList();

            ViewBag.Category = _categories.GetCategories()
                                          .FirstOrDefault(c => c.CategoryId == id)?
                                          .CategoryName;

            return View(books);
        }
    }
}