using Microsoft.AspNetCore.Mvc;
using Services;

namespace Library_Management_System.Controllers
{
    public class ReturnsController : Controller
    {
        private readonly ILoanService _service;

        public ReturnsController(ILoanService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReturnBook(int id)
        {
            _service.ReturnBook(id);

            return RedirectToAction("History", "Loans");
        }
    }
}
