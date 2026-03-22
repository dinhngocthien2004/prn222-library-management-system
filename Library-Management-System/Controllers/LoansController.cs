using BusinessObjects.Entities;
using DataAccessObjects;
using DataAccessObjects.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var role = HttpContext.Session.GetInt32("RoleID");

            if (role == null)
            {
                return RedirectToAction("Login", "Account");
            }
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
            if (HttpContext.Session.GetInt32("RoleID") != 1)
            {
                return RedirectToAction("Index");
            }
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
            if (HttpContext.Session.GetInt32("RoleID") != 1)
            {
                return RedirectToAction("Index");
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
            if (HttpContext.Session.GetInt32("RoleID") != 1)
            {
                return RedirectToAction("Index");
            }
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
            if (HttpContext.Session.GetInt32("RoleID") != 1)
            {
                return RedirectToAction("Index");
            }
            return View(p);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var p = _loans.GetLoanById(id);
            if (p is not null) _loans.DeleteLoan(p);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MyLoans()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdStr);

            var loans = _loans.GetLoans()
                .Where(l => l.UserId == userId)
                .Select(l => new
                {
                    ISBN = l.Copy.Book.Isbn,
                    Title = l.Copy.Book.Title,
                    Category = l.Copy.Book.Category.CategoryName,
                    Quantity = 1,
                    BorrowDate = l.LoanDate
                })
                .ToList();

            return View(loans);
        }
    }
}
