using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Repository;
using WebProject.Models;

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

        [HttpGet]
        public IActionResult Catalog(string category)
        {
            var categories = _repository.GetCategories().Select(c => c.Name);
            ViewBag.Categories = new SelectList(categories);

            IEnumerable<Animal> animals;
            if (category == null)
                animals = _repository.GetAnimals();
            else
                animals= _repository.GetAnimals(category);
            return View(animals);
        }
        [HttpPost]
        public IActionResult RedirectToCatalog(string category)
        {
            return RedirectToAction("Catalog", new {category = category});
        }
        public IActionResult Administrator()
        {
            return View();
        }
    }
}
