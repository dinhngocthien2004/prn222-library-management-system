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

        // Kiểm tra login
        private IActionResult EnsureLogin()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return null;
        }

        // Kiểm tra role member
        private bool IsMember()
        {
            var role = HttpContext.Session.GetInt32("RoleID");
            return role == 3;
        }

        public IActionResult Index()
        {
            var check = EnsureLogin();
            if (check != null) return check;

            var list = _books.GetBooks();
            return View(list.ToList());
        }

        public IActionResult Details(int? id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (id is null) return NotFound();

            var p = _books.GetBookById(id.Value);

            if (p == null) return NotFound();

            return View(p);
        }

        public IActionResult Create()
        {
            var check = EnsureLogin();
            if (check != null) return check;

            // Member không được thêm
            if (IsMember())
                return RedirectToAction(nameof(Index));

            ViewData["CategoryId"] = new SelectList(_categories.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Isbn,Publisher,CategoryId,PublishedYear,Description,ImageUrl,DateAdded")] Book p)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

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
            var check = EnsureLogin();
            if (check != null) return check;

            // Member không được sửa
            if (IsMember())
                return RedirectToAction(nameof(Index));

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
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

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
            var check = EnsureLogin();
            if (check != null) return check;

            // Member không được xóa
            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (id is null) return NotFound();

            var p = _books.GetBookById(id.Value);

            if (p == null) return NotFound();

            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            var p = _books.GetBookById(id);

            if (p != null)
            {
                _books.DeleteBook(p);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}