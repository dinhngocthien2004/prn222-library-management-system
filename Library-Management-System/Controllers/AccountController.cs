using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.AspNetCore.Http;
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
    public class AccountController : Controller
    {

        private readonly IUserService _accounts;
        public AccountController(IUserService accounts) => _accounts = accounts;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _accounts.Login(email, password);
            if (user is null)
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("Username", user.FullName);
            return RedirectToAction("Index", "Books");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
