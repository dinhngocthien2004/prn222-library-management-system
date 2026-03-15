using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library_Management_System.Controllers
{
    public class PublishersController : Controller
    {
        private readonly LibraryManagementDbContext _context;

        public PublishersController(LibraryManagementDbContext context)
        {
            _context = context;
        }

        // Danh sách tác giả
        public IActionResult Index()
        {
            var publishers = _context.Books
                .Select(b => b.Publisher)
                .Distinct()
                .ToList();

            return View(publishers);
        }

        // Xem sách của tác giả
        public IActionResult Books(int id)
        {
            var books = _context.Books
                .Include(b => b.Authors)
                .Where(b => b.Authors.Any(a => a.AuthorId == id))
                .ToList();

            return View(books);
        }

        public IActionResult BooksByPublisher(string name)
        {
            var books = _context.Books
                .Where(b => b.Publisher == name)
                .ToList();

            return View(books);
        }
    }
}