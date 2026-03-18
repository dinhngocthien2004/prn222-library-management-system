using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    public class ReportsController : Controller
    {
        private readonly LibraryManagementDbContext _context;

        public ReportsController(LibraryManagementDbContext context)
        {
            _context = context;
        }

        // ===== READ =====
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        // ===== POPULAR BOOKS =====
        public IActionResult PopularBooks()
        {
            var data = _context.Books
                .OrderByDescending(b => b.BorrowCount)
                .ToList();

            return View(data);
        }

        // ===== CREATE =====
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.BorrowCount = 0; // mặc định
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // ===== UPDATE =====
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Update(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // ===== DELETE =====
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
