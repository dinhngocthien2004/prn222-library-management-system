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

        // Trang tổng
        public IActionResult Index()
        {
            return View();
        }

        // 📊 Sách mượn nhiều
        public IActionResult PopularBooks()
        {
            var data = _context.Books
                .OrderByDescending(b => b.BorrowCount)
                .ToList();

            return View(data);
        }

        // ⏰ Sách quá hạn
        public IActionResult Overdue()
        {
            var data = _context.BorrowRecords
                .Include(b => b.Book)
                .Where(b => b.DueDate < DateTime.Now)
                .ToList();

            return View(data);
        }
    }
}
