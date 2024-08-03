using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Models;
using WebProject.Repository;

namespace WebProject.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdministratorController(IRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult AddAnimalForm()
        {
            var animalView = new AnimalViewModel();
            var categories = _repository.GetCategories().Select(c => c.Name);
            ViewBag.Categories = new SelectList(categories);
            return View(animalView);
        }
        [HttpPost]
        public IActionResult AddAnimal(AnimalViewModel newAnimalView)
        {
            if (ModelState.IsValid && newAnimalView.Picture != null)
            {
                var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath,"images");
                var filePath = Path.Combine(uploadFolder,newAnimalView.Picture.FileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    newAnimalView.Picture.CopyTo(fileStream);
                }

                var categoryId = _repository.GetCategoryByName(newAnimalView.CategoryName!).CategoryId;
                var animal = new Animal()
                {
                    Name = newAnimalView.Name,
                    Age = newAnimalView.Age,
                    Description = newAnimalView.Description,
                    CategoryId = categoryId,
                    PictureName = $"/images/{newAnimalView.Picture!.FileName}"
                };
                _repository.AddAnimal(animal);
                _repository.SaveChanges();
                return RedirectToAction("Administrator"); // Need to add some indicator for success
            }
            return RedirectToAction("Administrator"); // Need to add some indicator for failure
        }

        [HttpGet]
        public IActionResult EditAnimalForm(int id)
        {
            var animal = _repository.GetAnimal(id);
            ViewBag.Animal = animal;
            var animalEditor = new AnimalViewModel();
            var categories = _repository.GetCategories().Select(c => c.Name);
            ViewBag.Categories = new SelectList(categories);
            return View(animalEditor);
        }
        public IActionResult EditAnimal(AnimalViewModel newAnimalView, int id)
        {
            if (ModelState.IsValid) 
            {
                var animalToUpdate = _repository.GetAnimal(id);// animal to update
                animalToUpdate.Name = newAnimalView.Name;
                animalToUpdate.Age = newAnimalView.Age;

                if(newAnimalView.CategoryName != null && newAnimalView.CategoryName != animalToUpdate.Category!.Name) // if the category was changed
                    animalToUpdate.CategoryId = _repository.GetCategoryByName(newAnimalView.CategoryName).CategoryId;
                if(newAnimalView.Description != null && newAnimalView.Description != animalToUpdate.Description)
                    animalToUpdate.Description = newAnimalView.Description;
                if(newAnimalView.Picture != null)
                {
                    var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploadFolder, newAnimalView.Picture.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        newAnimalView.Picture.CopyTo(fileStream);
                    }
                    animalToUpdate.PictureName = $"/images/{newAnimalView.Picture!.FileName}";
                }

                _repository.SaveChanges();
                return RedirectToAction("Administrator"); // Need to add some indicator for success
            }
            return RedirectToAction("Administrator"); // Need to add some indicator for failure
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
