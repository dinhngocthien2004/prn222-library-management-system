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

        private bool EnsureLogin()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                RedirectToAction("Login", "Account");
                return false;
            }
            return true;
        }

        public IActionResult Index()
        {
            if (!EnsureLogin()) return RedirectToAction("Login", "Account");
            var list = _books.GetBooks();
            return View(list.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (!EnsureLogin()) return RedirectToAction("Login", "Account");
            if (id is null) return NotFound();
            var p = _books.GetBookById(id.Value);
            if (p == null) return NotFound();

            return View(p);
        }

        public IActionResult Create()
        {
            if (!EnsureLogin()) return RedirectToAction("Login", "Account");
            ViewData["CategoryId"] = new SelectList(_categories.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Isbn,Publisher,CategoryId,PublishedYear,Description,ImageUrl,DateAdded")] Book p)
        {
            if (!EnsureLogin()) return RedirectToAction("Login", "Account");
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
            if (!EnsureLogin()) return RedirectToAction("Login", "Account");
            if (id is null) return NotFound();
            var p = _books.GetBookById(id.Value);
            if (p == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(_categories.GetCategories(), "CategoryId", "CategoryName", p.CategoryId);

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookId,Title,Isbn,Publisher,CategoryId,PublishedYear,Description,ImageUrl,DateAdded")] Book p)
        {
            if (!EnsureLogin()) return RedirectToAction("Login", "Account");
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
            if (!EnsureLogin()) return RedirectToAction("Login", "Account");
            if (id is null) return NotFound();
            var p = _books.GetBookById(id.Value);
            if (p == null) return NotFound();

            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!EnsureLogin()) return RedirectToAction("Login", "Account");
            var p = _books.GetBookById(id);

            if (p != null)
            {
                _books.DeleteBook(p);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}