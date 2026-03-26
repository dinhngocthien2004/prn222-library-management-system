using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using Services;
using static Library_Management_System.Helpers.SessionHelper;

public class UsersController : Controller
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var users = _service.GetUsersExceptAdmin();

        if (users == null)
        {
            return Content("Users NULL");
        }
        return View(users);
    }

    // DELETE USER
    [HttpGet]
    public IActionResult DeleteUser(int id)
    {
        _service.DeleteUser(id);
        return RedirectToAction("Index");
    }

    // GET: /Users/Create
    [HttpGet]
    public IActionResult Create()
    {
        var roles = _service.GetRoles(); // 🔥 lấy role
        ViewBag.Roles = roles;           // 🔥 truyền qua View

        return View();
    }

    // POST: /Users/Create
    [HttpPost]
    public IActionResult Create(string email, string fullName, string password, int roleId)
    {
        var user = new User
        {
            Email = email,
            FullName = fullName,
            PasswordHash = password,
            RoleId = roleId, // 🔥 lấy từ dropdown
            IsActive = true,
            JoinDate = DateTime.Now
        };

        _service.CreateUser(user);

        return RedirectToAction("Index");
    }
}
