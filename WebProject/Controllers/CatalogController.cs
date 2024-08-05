using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Filters;
using WebProject.Models;
using WebProject.Repository;

namespace WebProject.Controllers
{
    [TypeFilter(typeof(ErrorsExceptionFilter))]
    public class CatalogController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Catalog(string category)
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
        public IActionResult RedirectToCatalog(string category)
        {
            return RedirectToAction("Catalog", new { category = category });
        }

        [HttpGet]
        public async Task<IActionResult> AnimalDetails(int id) 
        {
            var animal = await _repository.GetAnimal(id);
            var compositeAnimalComment = new CompositeAnimalCommentModel() 
            {
                Animal = animal,
                Comment = new Comment() { AnimalId = animal.AnimalId }
            };
            return View(compositeAnimalComment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment) 
        {
            if (ModelState.IsValid) 
            {
                var newComment = new Comment() 
                {
                    AnimalId = comment.AnimalId ,
                    CommentText = comment.CommentText
                };
                await _repository.AddComment(newComment);

                return RedirectToAction("AnimalDetails", new { id = comment.AnimalId });
            }

            var animal = await _repository.GetAnimal(comment.AnimalId);
            var compositeAnimalComment = new CompositeAnimalCommentModel()
            {
                Animal = animal,
                Comment = new Comment() { AnimalId = comment.AnimalId }
            };
            return View("AnimalDetails", compositeAnimalComment);
        }
    }
}
