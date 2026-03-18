using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library_Management_System.Controllers
{
    public class BorrowRecordsController : Controller
    {
        private readonly LibraryManagementDbContext _context;

        public BorrowRecordsController(LibraryManagementDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public IActionResult Index()
        {
            var data = _context.BorrowRecords
                               .Include(x => x.Book)
                               .ToList();

            return View(data);
        }

        // ================= CREATE =================
        public IActionResult Create()
        {
            ViewBag.BookId = new SelectList(_context.Books, "BookId", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(BorrowRecord record)
        {
            if (!ModelState.IsValid)
            {
                _context.BorrowRecords.Add(record);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(_context.Books, "BookId", "Title");
            return View(record);
        }

        // ================= EDIT =================
        public IActionResult Edit(int id)
        {
            var record = _context.BorrowRecords.Find(id);
            if (record == null) return NotFound();

            ViewBag.BookId = new SelectList(_context.Books, "BookId", "Title", record.BookId);
            return View(record);
        }

        [HttpPost]
        public IActionResult Edit(BorrowRecord record)
        {
            if (!ModelState.IsValid)
            {
                _context.Update(record);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(_context.Books, "BookId", "Title", record.BookId);
            return View(record);
        }

        // ================= DELETE =================
        public IActionResult Delete(int id)
        {
            var record = _context.BorrowRecords
                                 .Include(x => x.Book)
                                 .FirstOrDefault(x => x.BorrowRecordId == id);

            if (record == null) return NotFound();

            return View(record);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var record = _context.BorrowRecords.Find(id);

            if (record != null)
            {
                _context.BorrowRecords.Remove(record);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
