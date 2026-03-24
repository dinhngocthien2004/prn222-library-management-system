using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

public class RolesController : Controller
{
    private readonly IUserService _service;
    private readonly LibraryManagementDbContext _context;

    public RolesController(IUserService service, LibraryManagementDbContext context)
    {
        _service = service;
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _service.GetUsersExceptAdmin();

        ViewBag.Roles = _context.Roles
            .Where(r => r.RoleName != "Admin")
            .ToList();

        return View(_service.GetUsers());
    }

    [HttpPost]
    public IActionResult AssignRole(int userId, int roleId)
    {
        var dao = new AccountDAO(_context);
        dao.AssignRole(userId, roleId);

        return RedirectToAction("Index");
    }

}