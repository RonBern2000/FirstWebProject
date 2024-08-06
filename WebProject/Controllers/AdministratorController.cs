using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Filters;
using WebProject.Models;
using WebProject.Repository;

namespace WebProject.Controllers
{
    [ServiceFilter(typeof(ActionsFilter))]
    public class AdministratorController : Controller
    {
        private readonly IRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<AdministratorController> _logger;
        public AdministratorController(IRepository repository, IWebHostEnvironment webHostEnvironment, ILogger<AdministratorController> logger)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
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
            var categories = await _repository.GetCategoriesNames();
            ViewBag.Categories = new SelectList(categories);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnimal(AnimalViewModel newAnimalView)
        {
            if (ModelState.IsValid && newAnimalView.Picture != null)
            {
                var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadFolder, newAnimalView.Picture.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
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
                return RedirectToAction("ResponseToOperation", new { operationSuccess = true });
            }
            return RedirectToAction("ResponseToOperation", new { operationSuccess = false });
        }

        [HttpGet]
        public async Task<IActionResult> EditAnimalForm(int id)
        {
            var animal = await _repository.GetAnimal(id);
            ViewBag.Animal = animal;
            var animalEditor = new AnimalModelEditor();
            animalEditor.Description = animal.Description;
            animalEditor.CategoryName = animal.Category!.Name;
            var categories = await _repository.GetCategoriesNames();
            ViewBag.Categories = new SelectList(categories);
            return View(animalEditor);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAnimal(AnimalModelEditor animalEditor, int id)
        {
            if (ModelState.IsValid)
            {
                var animalToUpdate = await _repository.GetAnimal(id);// animal to update
                animalToUpdate.Name = animalEditor.Name;
                animalToUpdate.Age = animalEditor.Age;
                animalToUpdate.Description = animalEditor.Description;

                if (animalEditor.CategoryName != animalToUpdate.Category!.Name) // if the category was changed
                {
                    var category = await _repository.GetCategoryByName(animalEditor.CategoryName!);
                    animalToUpdate.CategoryId = category.CategoryId;
                }
                if (animalEditor.Picture != null)
                {
                    var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploadFolder, animalEditor.Picture.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        animalEditor.Picture.CopyTo(fileStream);
                    }
                    animalToUpdate.PictureName = $"/images/{animalEditor.Picture!.FileName}";
                }

                await _repository.SaveChangesAsync();
                return RedirectToAction("ResponseToOperation", new { operationSuccess = true });
            }
            return RedirectToAction("ResponseToOperation", new { operationSuccess = false });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAnimalForm(int id)
        {
            var animal = await _repository.GetAnimal(id);
            return View(animal);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAnimal(int id, string name)
        {
            var animalToRemove = await _repository.GetAnimal(id);
            if (animalToRemove.Name == name)
            {
                await _repository.RemoveAnimal(animalToRemove);
                return RedirectToAction("Administrator");
            }
            return RedirectToAction("Administrator");
        }
        public IActionResult ResponseToOperation(bool operationSuccess)
        {
            ViewBag.OperationSuccess = operationSuccess;
            return View("Response");
        }
    }
}