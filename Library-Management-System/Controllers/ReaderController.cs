using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessObjects;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

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
            var role = HttpContext.Session.GetInt32("RoleID");

            if (role == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var readers = _service.GetReaders();
            return View(readers);
        }

       
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("RoleID") != 1)
            {
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(_userService.GetUsers(), "UserId", "Email");
            return View();
        }

        [HttpPost]
       
        public IActionResult Create(Reader r)
        {
            if (HttpContext.Session.GetInt32("RoleID") != 1)
            {
                return RedirectToAction("Index");
            }
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
            if (HttpContext.Session.GetInt32("RoleID") != 1)
            {
                return RedirectToAction("Index");
            }
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
            if (HttpContext.Session.GetInt32("RoleID") != 1)
            {
                return RedirectToAction("Index");
            }
            _service.DeleteReader(r);
            return RedirectToAction(nameof(Index));
        }
    }
}




