using LibraryManagementSystem.BLL.Services;
using LibraryManagementSystem.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanService _service;
        public LoanController(LoanService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAllLoans());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                _service.CreateLoan(loan);
                return RedirectToAction("Index");
            }
            return View(loan);
        }

        public IActionResult Return(int id)
        {
            _service.ReturnBook(id);
            return RedirectToAction("Index");
        }
    }
}
