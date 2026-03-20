using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class DashboardController : Controller
    {
        private readonly LibraryManagementDbContext _context;

        public DashboardController(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public IActionResult Statistics()
        {
            // Tổng số đầu sách
            var totalBooks = _context.Books.Count();

            // Tổng số bản sao sách
            var totalCopies = _context.BookCopies.Count();

            // Sách đang được mượn (giả sử IsAvailable = false là đang mượn)
            var borrowedCopies = _context.BookCopies
                .Where(c => c.IsAvailable == false)
                .Count();

            // Sách còn lại
            var availableCopies = _context.BookCopies
                .Where(c => c.IsAvailable == true)
                .Count();

            // Top sách mượn nhiều nhất (dựa vào BorrowCount)
            var topBooks = _context.Books
                .OrderByDescending(b => b.BorrowCount)
                .Take(5)
                .Select(b => new TopBookViewModel
                {
                    Title = b.Title,
                    BorrowCount = b.BorrowCount
                })
                .ToList();

            var model = new BookStatisticsViewModel
            {
                TotalBooks = totalBooks,
                TotalCopies = totalCopies,
                TotalBorrowedCopies = borrowedCopies,
                TotalAvailableCopies = availableCopies,
                TopBooks = topBooks
            };

            return View(model);
        }
    }
}
