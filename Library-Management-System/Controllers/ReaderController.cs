using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessObjects;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Management_System.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService _service;
        private readonly IUserService _userService;

        public ReaderController(IReaderService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var readers = _service.GetReaders();
            return View(readers);
        }

        public IActionResult Create()
        {
            ViewBag.UserId = new SelectList(_userService.GetUsers(), "UserId", "Email");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Reader r)
        {
            _service.SaveReader(r);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var reader = _service.GetReaderById(id);

            ViewBag.UserId = new SelectList(_userService.GetUsers(), "UserId", "Email", reader.UserId);

            return View(reader);
        }

        [HttpPost]
        public IActionResult Edit(Reader r)
        {
            _service.UpdateReader(r);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var reader = _service.GetReaderById(id);
            return View(reader);
        }

        [HttpPost]
        public IActionResult Delete(Reader r)
        {
            _service.DeleteReader(r);
            return RedirectToAction(nameof(Index));
        }
    }
}




