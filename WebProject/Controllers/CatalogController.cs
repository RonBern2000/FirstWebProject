using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Models;
using WebProject.Repository;

namespace WebProject.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IRepository _repository;

        public CatalogController(IRepository repository)
        {
            _repository = repository;
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
                animals = _repository.GetAnimals(category);
            return View(animals);
        }
        [HttpPost]
        public IActionResult RedirectToCatalog(string category)
        {
            return RedirectToAction("Catalog", new { category = category });
        }
    }
}
