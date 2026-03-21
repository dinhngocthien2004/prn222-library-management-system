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

        // ================================
        // CHECK LOGIN
        // ================================
        private IActionResult EnsureLogin()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return null;
        }

        // ================================
        // CHECK ROLE
        // ================================
        private bool IsMember()
        {
            var role = HttpContext.Session.GetInt32("RoleID");
            return role == 3;
        }

        // ================================
        // LIST BOOK
        // ================================
        public IActionResult Index(string keyword, int? year, int? categoryId, int page = 1)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            var books = _books.GetBooks();

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();

                books = books.Where(b =>
                       (b.Title != null && b.Title.ToLower().Contains(keyword))
                    || (b.Publisher != null && b.Publisher.ToLower().Contains(keyword))
                    || (b.Description != null && b.Description.ToLower().Contains(keyword))
                    || (b.Isbn != null && b.Isbn.ToLower().Contains(keyword))
                    || (b.PublishedYear.ToString().Contains(keyword))
                );
            }

            if (year.HasValue)
            {
                books = books.Where(b => b.PublishedYear == year.Value);
            }

            if (categoryId.HasValue)
            {
                books = books.Where(b => b.CategoryId == categoryId.Value);
            }

            ViewBag.Categories = new SelectList(
                _categories.GetCategories(),
                "CategoryId",
                "CategoryName"
            );

            ViewBag.Keyword = keyword;
            ViewBag.Year = year;
            ViewBag.CategoryId = categoryId;

            int pageSize = 7;
            int totalBooks = books.Count();
            int totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

            var pagedBooks = books
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedBooks);
        }

        // ================================
        // DETAILS
        // ================================
        public IActionResult Details(int? id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (id == null) return NotFound();

            var book = _books.GetBookById(id.Value);

            if (book == null) return NotFound();

            return View(book);
        }

        // ================================
        // CREATE
        // ================================
        public IActionResult Create()
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            ViewBag.CategoryId = new SelectList(
                _categories.GetCategories(),
                "CategoryId",
                "CategoryName"
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Title,Isbn,Publisher,CategoryId,PublishedYear,Description,ImageUrl")] Book book,
            int Quantity
        )
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (Quantity <= 0)
            {
                ModelState.AddModelError("", "Số lượng phải lớn hơn 0");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(
                    _categories.GetCategories(),
                    "CategoryId",
                    "CategoryName",
                    book.CategoryId
                );

                return View(book);
            }

            book.DateAdded = DateTime.Now;
            _books.SaveBook(book);

            // Tạo BookCopy
            for (int i = 0; i < Quantity; i++)
            {
                _books.AddBookCopy(new BookCopy
                {
                    BookId = book.BookId,
                    Barcode = Guid.NewGuid().ToString(),
                    Status = 1
                });
            }

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // EDIT
        // ================================
        public IActionResult Edit(int? id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (id == null) return NotFound();

            var book = _books.GetBookById(id.Value);

            if (book == null) return NotFound();

            ViewBag.CategoryId = new SelectList(
                _categories.GetCategories(),
                "CategoryId",
                "CategoryName",
                book.CategoryId
            );

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("BookId,Title,Isbn,Publisher,CategoryId,PublishedYear,Description,ImageUrl,DateAdded")] Book book,
            int Quantity // 🔥 thêm
        )
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (id != book.BookId) return NotFound();

            var existingBook = _books.GetBookById(id);

            int currentQuantity = existingBook.BookCopies.Count;

            // 🔼 TĂNG
            if (Quantity > currentQuantity)
            {
                int add = Quantity - currentQuantity;

                for (int i = 0; i < add; i++)
                {
                    _books.AddBookCopy(new BookCopy
                    {
                        BookId = id,
                        Barcode = Guid.NewGuid().ToString(),
                        Status = 1
                    });
                }
            }

            // 🔽 GIẢM
            else if (Quantity < currentQuantity)
            {
                int remove = currentQuantity - Quantity;

                var removable = existingBook.BookCopies
                    .Where(c => c.Status == 1)
                    .Take(remove)
                    .ToList();

                foreach (var copy in removable)
                {
                    _books.DeleteBookCopy(copy);
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(
                    _categories.GetCategories(),
                    "CategoryId",
                    "CategoryName",
                    book.CategoryId
                );

                return View(book);
            }

            _books.UpdateBook(book);

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // DELETE
        // ================================
        public IActionResult Delete(int? id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (id == null) return NotFound();

            var book = _books.GetBookById(id.Value);

            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            var book = _books.GetBookById(id);

            if (book != null)
            {
                _books.DeleteBook(book);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}