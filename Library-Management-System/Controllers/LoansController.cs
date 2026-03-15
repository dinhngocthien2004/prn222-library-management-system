using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    public class LoansController : Controller
    {     
        private readonly ILoanService _loans;
        private readonly IBookCopyService _bookcopies;
        private readonly IUserService _user;
     
        public LoansController(ILoanService loans, IBookCopyService bookcopies, IUserService users)
        {
            _loans = loans;
            _bookcopies = bookcopies;
            _user = users;
           
        }
        public IActionResult History()
        {
            var loans = _loans.GetHistory();

            return View(loans);
        }

        public IActionResult Index()
        {
            var list = _loans.GetLoans();
            return View(list.ToList());
        }

        public IActionResult Details(int? id)
        {

            if (id is null) return NotFound();
            var p = _loans.GetLoanById(id.Value);
            if (p is null) return NotFound();
            return View(p);
        }
        // xem cỉ lại
        public IActionResult Create()
        {
            ViewData["CopyId"] = new SelectList(_bookcopies.GetBookCopies(), "CopyId", "Barcode");
            ViewData["UserId"] = new SelectList(_user.GetUsers(), "UserId", "Email");
            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create([Bind("LoanId,UserId,CopyId,LoanDate,DueDate,ReturnDate,Status")] Loan p)
        {
            p.LoanDate = DateTime.Now;   
            if (ModelState.IsValid)
            {
                ViewData["CopyId"] = new SelectList(_bookcopies.GetBookCopies(), "CopyId", "Barcode", p.CopyId);
                ViewData["UserId"] = new SelectList(_user.GetUsers(), "UserId", "Email", p.UserId);
                return View(p);
                
            }
            _loans.SaveLoan(p);
            return RedirectToAction(nameof(Index));
        }
    
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var p = _loans.GetLoanById(id.Value);
            if (p == null) return NotFound();

            ViewBag.CopyId = new SelectList(
                _bookcopies.GetBookCopies(),
                "CopyId",
                "Barcode",
                p.CopyId
            );

            ViewBag.UserId = new SelectList(
                _user.GetUsers(),
                "UserId",
                "Email",
                p.UserId
            );
            return View(p);
           
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("LoanId,UserId,CopyId,LoanDate,DueDate,ReturnDate,Status")] Loan p)
        {
            if (id != p.LoanId) return NotFound();

            if (ModelState.IsValid)
            {
                ViewBag.CopyId = new SelectList(
                _bookcopies.GetBookCopies(),
                "CopyId",
                "Barcode",
                p.CopyId
            );

                ViewBag.UserId = new SelectList(
                    _user.GetUsers(),
                    "UserId",
                    "Email",
                    p.UserId
                );

                return View(p);
               
            }

            _loans.UpdateLoan(p);
            return RedirectToAction("Index");

        }


        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
            var p = _loans.GetLoanById(id.Value);
            if (p is null) return NotFound();
            return View(p);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var p = _loans.GetLoanById(id);
            if (p is not null) _loans.DeleteLoan(p);
            return RedirectToAction(nameof(Index));
        }
    }
}
