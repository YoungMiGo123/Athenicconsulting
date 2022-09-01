using Microsoft.AspNetCore.Mvc;

namespace AthenicConsulting.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult ManageUsers()
        {
            return View();
        }
    }
}
