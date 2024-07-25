using Microsoft.AspNetCore.Mvc;
using WebProject.Repository;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var topTwoAnimals = _repository.Top2Aniamls();
            return View(topTwoAnimals);
        }
        public IActionResult Catalog()
        {
            return View();
        }
        public IActionResult Administrator()
        {
            return View();
        }
    }
}
