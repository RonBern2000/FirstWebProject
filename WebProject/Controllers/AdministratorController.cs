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
        public async Task<IActionResult> Administrator(string category)
        {
            var categories = await _repository.GetCategoriesNames();
            ViewBag.Categories = new SelectList(categories);

            IEnumerable<Animal> animals;
            if (category == null)
                animals = await _repository.GetAnimals();
            else
                animals = await _repository.GetAnimals(category);
            return View(animals);
        }

        [HttpPost]
        public IActionResult RedirectAdministrator(string category)
        {
            return RedirectToAction("Administrator", new { category = category });
        }

        [HttpGet]
        public async Task<IActionResult> AddAnimalForm()
        {
            var animalView = new AnimalViewModel();
            var categories = await _repository.GetCategoriesNames();
            ViewBag.Categories = new SelectList(categories);
            return View(animalView);
        }
        [HttpPost]
        public async Task<IActionResult> AddAnimal(AnimalViewModel newAnimalView)
        {
            if (ModelState.IsValid && newAnimalView.Picture != null)
            {
                var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath,"images");
                var filePath = Path.Combine(uploadFolder,newAnimalView.Picture.FileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    newAnimalView.Picture.CopyTo(fileStream);
                }

                var categoryId = await _repository.GetCategoryByName(newAnimalView.CategoryName!);
                var animal = new Animal()
                {
                    Name = newAnimalView.Name,
                    Age = newAnimalView.Age,
                    Description = newAnimalView.Description,
                    CategoryId = categoryId.CategoryId,
                    PictureName = $"/images/{newAnimalView.Picture!.FileName}"
                };
                await _repository.AddAnimal(animal);
                return RedirectToAction("Administrator"); // Need to add some indicator for success
            }
            return RedirectToAction("Administrator"); // Need to add some indicator for failure
        }

        [HttpGet]
        public async Task<IActionResult> EditAnimalForm(int id)
        {
            var animal = await _repository.GetAnimal(id);
            ViewBag.Animal = animal;
            var animalEditor = new AnimalViewModel();
            var categories = await _repository.GetCategoriesNames();
            ViewBag.Categories = new SelectList(categories);
            return View(animalEditor);
        }
        public async Task<IActionResult> EditAnimal(AnimalViewModel newAnimalView, int id)
        {
            if (ModelState.IsValid) 
            {
                var animalToUpdate = await _repository.GetAnimal(id);// animal to update
                animalToUpdate.Name = newAnimalView.Name;
                animalToUpdate.Age = newAnimalView.Age;

                if (newAnimalView.CategoryName != null && newAnimalView.CategoryName != animalToUpdate.Category!.Name) // if the category was changed
                {
                    var category = await _repository.GetCategoryByName(newAnimalView.CategoryName);
                    animalToUpdate.CategoryId = category.CategoryId;
                } 
                if (newAnimalView.Description != null && newAnimalView.Description != animalToUpdate.Description)
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
        public async Task<IActionResult> DeleteAnimalForm(int id)
        {
            var animal = await _repository.GetAnimal(id);
            return View(animal);
        }
        public async Task<IActionResult> DeleteAnimal(int id, string name)
        { //Needs popups for successs and failure also add js validation
            var animalToRemove = await _repository.GetAnimal(id);
            if(animalToRemove.Name == name)
            {
                await _repository.RemoveAnimal(animalToRemove);
                return RedirectToAction("Administrator");
            }
            return RedirectToAction("Administrator");
        }
    }
}
