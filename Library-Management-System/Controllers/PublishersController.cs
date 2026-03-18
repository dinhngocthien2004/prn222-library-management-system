using Microsoft.AspNetCore.Mvc;
using DataAccessObjects;
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

        // Danh sách Nhà xuất bản
        public IActionResult Index()
        {
            var publishers = _context.Books
                .Select(b => b.Publisher)
                .Distinct()
                .ToList();

            return View(publishers);
        }

        // Xem sách theo Nhà xuất bản
        public IActionResult BooksByPublisher(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index");
            }

            var books = _context.Books
                .Where(b => b.Publisher == name)
                .ToList();

            return View(books);
        }
    }
}