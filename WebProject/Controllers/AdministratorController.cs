using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Models;
using WebProject.Repository;

namespace WebProject.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IRepository _repository;

        public AdministratorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Administrator(string category)
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
        public IActionResult RedirectAdministrator(string category)
        {
            return RedirectToAction("Administrator", new { category = category });
        }

        [HttpGet]
        public IActionResult AddAnimal()
        {
            return null;
        }
        [HttpPost]
        public IActionResult AddAnimal(int id)
        {
            return null;
        }

        [HttpGet]
        public IActionResult EditAnimal(int id)
        {
            return null;
        }
        public IActionResult EditAnimal()
        {
            return null;
        }

        [HttpGet]
        public IActionResult DeleteAnimalForm(int id)
        {
            var animal = _repository.GetAnimal(id);
            return View(animal);
        }
        
        public IActionResult DeleteAnimal(int id, string name)
        { //Needs popups for successs and failure also add js validation
            var animalToRemove = _repository.GetAnimal(id);
            if(animalToRemove.Name == name)
            {
                _repository.RemoveAnimal(animalToRemove);
                _repository.SaveChanges();
                return RedirectToAction("Administrator");
            }
            return RedirectToAction("Administrator");
        }
    }
}
