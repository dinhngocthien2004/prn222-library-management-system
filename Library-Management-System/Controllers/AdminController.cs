using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IUserService _userService;
    private readonly LibraryManagementDbContext _context;

    public AdminController(IUserService userService, LibraryManagementDbContext context)
    {
        _userService = userService;
        _context = context;
    }

    // ===== NGƯỜI DÙNG =====
    public IActionResult Users()
    {
        var users = _userService.GetUsers();
        return View(users);
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        _userService.CreateUser(user);
        return RedirectToAction("Users");
    }

    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return RedirectToAction("Users");
    }

    // ===== PHÂN QUYỀN =====
    public IActionResult Roles()
    {
        var users = _userService.GetUsersExceptAdmin();

        foreach (var u in users)
        {
            Console.WriteLine(u.Email);
        }
        // ❌ Không cho hiện role Admin
        ViewBag.Roles = _context.Roles
            .Where(r => r.RoleName != "Admin")
            .ToList();

        return View(users);
    }

    [HttpPost]
    public IActionResult AssignRole(int userId, int roleId)
    {
        _userService.AssignRole(userId, roleId);
        return RedirectToAction("Roles");
    }
}