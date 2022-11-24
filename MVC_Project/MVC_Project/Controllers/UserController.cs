using Microsoft.AspNetCore.Mvc;

namespace MVC_Project.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
