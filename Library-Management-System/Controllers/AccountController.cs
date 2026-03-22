using BusinessObjects.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Library_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _accounts;

        public AccountController(IUserService accounts)
        {
            _accounts = accounts;   
        }

        // Hiển thị trang Login
        [HttpGet]
        public IActionResult Login()
        {
            // Nếu đã đăng nhập rồi thì quay về trang chủ
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // Xử lý Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _accounts.Login(email, password);

            // Sai tài khoản
            if (user == null)
            {
                ViewBag.Error = "Invalid email or password!";
                return View();
            }

            // Tài khoản bị khóa
            if (user.IsActive == false)
            {
                ViewBag.Error = "Your account has been locked!";
                return View();
            }

            // Xóa session cũ
            HttpContext.Session.Clear();

            // Lưu thông tin vào session
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("Username", user.FullName);
            HttpContext.Session.SetInt32("RoleID", user.RoleId);

            // Login xong → về Trang Chủ
            return RedirectToAction("Index", "Home");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}