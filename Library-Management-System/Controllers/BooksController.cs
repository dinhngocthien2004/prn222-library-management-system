using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Library_Management_System.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _books;
        private readonly ICategoryService _categories;
        private readonly ILoanService _loans;
        private readonly IBookCopyService _bookCopies;
        private readonly LibraryManagementDbContext _context;

        public BooksController(
            IBookService books,
            ICategoryService categories,
            ILoanService loans,
            IBookCopyService bookCopies, LibraryManagementDbContext context
        )
        {
            _books = books;
            _categories = categories;
            _loans = loans;
            _bookCopies = bookCopies;
            _context = context;
        }

        private IActionResult EnsureLogin()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return null;
        }

        private bool IsMember()
        {
            var role = HttpContext.Session.GetInt32("RoleID");
            return role == 3;
        }

        private bool IsAdminOrLibrarian()
        {
            var role = HttpContext.Session.GetInt32("RoleID");
            return role == 1 || role == 2;
        }

        // ================================
        // 📚 INDEX
        // ================================
        //public IActionResult Index()
        //{
        //    // Lấy tất cả sách mới nhất từ database
        //    var books = _context.Books
        //        .Include(b => b.Category)
        //        .Include(b => b.BookCopies)
        //        .ToList();

        //    return View(books);
        //}
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
                books = books.Where(b => b.PublishedYear == year.Value);

            if (categoryId.HasValue)
                books = books.Where(b => b.CategoryId == categoryId.Value);

            ViewBag.Categories = new SelectList(
                _categories.GetCategories(),
                "CategoryId",
                "CategoryName"
            );

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
        // 🔍 DETAILS
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
        // ➕ CREATE (GET)
        // ================================
        public IActionResult Create()
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            ViewBag.Categories = new SelectList(
                _categories.GetCategories(),
                "CategoryId",
                "CategoryName"
            );

            return View();
        }

        // ================================
        // ➕ CREATE (POST)
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book, int Quantity)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                book.DateAdded = DateTime.Now;

                _books.SaveBook(book);

                // tạo bản copy
                for (int i = 0; i < Quantity; i++)
                {
                    _bookCopies.AddBookCopy(new BookCopy
                    {
                        BookId = book.BookId,
                        IsAvailable = true,
                        Barcode = Guid.NewGuid().ToString() // 🔥 tạo barcode tự động, không null
                    });
                }

                return RedirectToAction(nameof(Index));
            }

            // 🔥 FIX: load lại dropdown khi lỗi
            ViewBag.Categories = new SelectList(
                _categories.GetCategories(),
                "CategoryId",
                "CategoryName",
                book.CategoryId
            );

            return View(book);
        }

        // ================================
        // ✏️ EDIT (GET)
        // ================================
        public IActionResult Edit(int? id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            // ❌ CHẶN MEMBER
            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (id == null) return NotFound();

            var book = _books.GetBookById(id.Value);
            if (book == null) return NotFound();

            ViewBag.Categories = new SelectList(
                _categories.GetCategories(),
                "CategoryId",
                "CategoryName",
                book.CategoryId
            );

            return View(book);
        }

        // ================================
        // ✏️ EDIT (POST)
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            // ❌ CHẶN MEMBER
            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (id != book.BookId) return NotFound();

            if (ModelState.IsValid)
            {
                _books.UpdateBook(book);
                return RedirectToAction(nameof(Index));
            }

            // 🔥 FIX: LOAD LẠI DROPDOWN
            ViewBag.Categories = new SelectList(
                _categories.GetCategories(),
                "CategoryId",
                "CategoryName",
                book.CategoryId
            );

            return View(book);
        }

        // ================================
        // 🗑 DELETE (GET)
        // ================================
        public IActionResult Delete(int? id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            // ❌ CHẶN MEMBER
            if (IsMember())
                return RedirectToAction(nameof(Index));

            if (id == null) return NotFound();

            var book = _books.GetBookById(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        // ================================
        // 🗑 DELETE (POST)
        // ================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            if (IsMember())
                return RedirectToAction(nameof(Index));

            var book = _books.GetBookById(id);
            if (book == null) return NotFound();

            _books.DeleteBook(book);

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // 📥 BORROW CONFIRM
        // ================================
        public IActionResult BorrowSuccess()
        {
            return View(); 
        }
        public IActionResult BorrowConfirm(int id)
        {
            var check = EnsureLogin();
            if (check != null) return check;

            // 🔥 CHỈ MEMBER MỚI ĐƯỢC MƯỢN
            if (!IsMember())
            {
                return RedirectToAction(nameof(Index));
            }

            var book = _books.GetBookById(id);

            if (book == null) return NotFound();

            return View(book);
        }       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrow(int bookId, int quantity)
        {
            // Kiểm tra login
            var check = EnsureLogin();
            if (check != null) return check;

            if (!IsMember())
                return RedirectToAction(nameof(Index));

            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Lấy Book từ cùng DbContext để EF track
            var book = _context.Books
                .Include(b => b.BookCopies) // lấy luôn BookCopies
                .FirstOrDefault(b => b.BookId == bookId);

            if (book == null) return NotFound();

            // Kiểm tra số lượng muốn mượn
            if (quantity < 1 || quantity > 10)
            {
                ViewBag.Error = "Chỉ được mượn từ 1 đến 10 cuốn!";
                return View("BorrowConfirm", book);
            }

            //// Lấy số lượng sách đã mượn của user
            //int currentBorrowed = _context.Loans
            //    .Count(l => l.UserId == userId && l.Copy.BookId == bookId && !l.IsReturned);

            //if (currentBorrowed + quantity > 10)
            //{
            //    ViewBag.Error = $"Bạn đã mượn {currentBorrowed} cuốn. Tối đa là 10!";
            //    return View("BorrowConfirm", book);
            //}

            // Lấy các bản copy còn sẵn
            var availableCopies = book.BookCopies
                .Where(c => c.IsAvailable)
                .Take(quantity)
                .ToList();

            if (availableCopies.Count < quantity)
            {
                ViewBag.Error = "Không đủ sách trong kho!";
                return View("BorrowConfirm", book);
            }

            // ===========================
            // Thêm Loan và đánh dấu copy đã mượn
            // ===========================
            foreach (var copy in availableCopies)
            {
                _context.Loans.Add(new Loan
                {
                    UserId = userId,
                    CopyId = copy.CopyId,
                    LoanDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    IsReturned = false
                });

                copy.IsAvailable = false; // EF track tự động
            }

            // ===========================
            // Trừ số lượng sách
            // ===========================
            book.Quantity -= quantity;
            if (book.Quantity < 0) book.Quantity = 0;

            // Lưu tất cả 1 lần
            _context.SaveChanges();

            // Quay về Index để UI hiển thị số lượng mới
            return RedirectToAction("BorrowSuccess");
        }
    }
}