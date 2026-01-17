using LibraryManagementSystem.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetBooksForDisplay();
            return View(books);
        }
    }
}
