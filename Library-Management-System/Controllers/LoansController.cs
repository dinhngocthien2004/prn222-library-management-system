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
        //private readonly LibraryManagementDbContext _context;

        //public LoansController(LibraryManagementDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: Loans
        //public async Task<IActionResult> Index()
        //{
        //    var libraryManagementDbContext = _context.Loans.Include(l => l.Copy).Include(l => l.User);
        //    return View(await libraryManagementDbContext.ToListAsync());
        //}

        //// GET: Loans/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var loan = await _context.Loans
        //        .Include(l => l.Copy)
        //        .Include(l => l.User)
        //        .FirstOrDefaultAsync(m => m.LoanId == id);
        //    if (loan == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(loan);
        //}

        //// GET: Loans/Create
        //public IActionResult Create()
        //{
        //    ViewData["CopyId"] = new SelectList(_context.BookCopies, "CopyId", "Barcode");
        //    ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
        //    return View();
        //}

        //// POST: Loans/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("LoanId,UserId,CopyId,LoanDate,DueDate,ReturnDate,Status")] Loan loan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(loan);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CopyId"] = new SelectList(_context.BookCopies, "CopyId", "Barcode", loan.CopyId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", loan.UserId);
        //    return View(loan);
        //}

        //// GET: Loans/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var loan = await _context.Loans.FindAsync(id);
        //    if (loan == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CopyId"] = new SelectList(_context.BookCopies, "CopyId", "Barcode", loan.CopyId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", loan.UserId);
        //    return View(loan);
        //}

        //// POST: Loans/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("LoanId,UserId,CopyId,LoanDate,DueDate,ReturnDate,Status")] Loan loan)
        //{
        //    if (id != loan.LoanId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(loan);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LoanExists(loan.LoanId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CopyId"] = new SelectList(_context.BookCopies, "CopyId", "Barcode", loan.CopyId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", loan.UserId);
        //    return View(loan);
        //}

        //// GET: Loans/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var loan = await _context.Loans
        //        .Include(l => l.Copy)
        //        .Include(l => l.User)
        //        .FirstOrDefaultAsync(m => m.LoanId == id);
        //    if (loan == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(loan);
        //}

        //// POST: Loans/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var loan = await _context.Loans.FindAsync(id);
        //    if (loan != null)
        //    {
        //        _context.Loans.Remove(loan);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LoanExists(int id)
        //{
        //    return _context.Loans.Any(e => e.LoanId == id);
        //}



        private readonly ILoanService _loans;
        private readonly IBookCopyService _bookcopies;
        private readonly IUserService _user;
        private readonly LibraryManagementDbContext _context;

        public LoansController(ILoanService loans, IBookCopyService bookcopies, IUserService users, LibraryManagementDbContext context)
        {
            _loans = loans;
            _bookcopies = bookcopies;
            _user = users;
            _context = context;
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Barcode");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create([Bind("LoanId,UserId,CopyId,LoanDate,DueDate,ReturnDate,Status")] Loan p)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CopyId"] = new SelectList(_bookcopies.GetBookCopies(), "CopyId", "Barcode", p.CopyId);
                return View(p);
            }
            _loans.SaveLoan(p);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int? id)
        {
            if (id is null) return NotFound();
            var p = _loans.GetLoanById(id.Value);
            if (p is null) return NotFound();
            ViewData["CopyId"] = new SelectList(_bookcopies.GetBookCopies(), "CopyId", "Barcode", p.CopyId);
            return View(p);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LoanId,UserId,CopyId,LoanDate,DueDate,ReturnDate,Status")] Loan p)
        {
            if (id != p.LoanId) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewData["CopyId"] = new SelectList(_bookcopies.GetBookCopies(), "CopyId", "Barcode", p.CopyId);
                return View(p);
            }
            _loans.UpdateLoan(p);
            return RedirectToAction(nameof(Index));
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
