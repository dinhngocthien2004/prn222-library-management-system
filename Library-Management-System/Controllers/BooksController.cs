using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Library_Management_System.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _books;
        private readonly ICategoryService _categories;

        public BooksController(IBookService books, ICategoryService categories)
        {
            _books = books;
            _categories = categories;
        }

        public IActionResult Index()
        {
            var list = _books.GetBooks();
            return View(list.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var p = _books.GetBookById(id.Value);
            if (p == null) return NotFound();

            return View(p);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categories.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Isbn,Publisher,CategoryId,PublishedYear,Description,ImageUrl,DateAdded")] Book p)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_categories.GetCategories(), "CategoryId", "CategoryName", p.CategoryId);
                return View(p);
            }

            _books.SaveBook(p);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var p = _books.GetBookById(id.Value);
            if (p == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(_categories.GetCategories(), "CategoryId", "CategoryName", p.CategoryId);

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookId,Title,Isbn,Publisher,CategoryId,PublishedYear,Description,ImageUrl,DateAdded")] Book p)
        {
            if (id != p.BookId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_categories.GetCategories(), "CategoryId", "CategoryName", p.CategoryId);
                return View(p);
            }

            _books.UpdateBook(p);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var p = _books.GetBookById(id.Value);
            if (p == null) return NotFound();

            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var p = _books.GetBookById(id);

            if (p != null)
            {
                _books.DeleteBook(p);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}