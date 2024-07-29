using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
