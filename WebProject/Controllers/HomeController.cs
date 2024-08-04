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
        public async Task<IActionResult> Index()
        {
            var topTwoAnimals = await _repository.Top2Aniamls();
            return View(topTwoAnimals);
        }
    }
}
