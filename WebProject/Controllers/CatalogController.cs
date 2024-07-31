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

        [HttpGet]
        public IActionResult AnimalDetails(int id) 
        {
            ViewBag.CommentModel = new Comment();
            var animal = _repository.GetAnimal(id);
            var compositeAnimalComment = new CompositeAnimalCommentModel() 
            {
                Animal = animal,
                Comment = new Comment() { AnimalId = animal.AnimalId }
            };
            return View(compositeAnimalComment);
        }

        [HttpPost]
        public IActionResult CreateComment(Comment comment) 
        {
            if (ModelState.IsValid) 
            {
                var newComment = new Comment() 
                {
                    AnimalId = comment.AnimalId ,
                    CommentText = comment.CommentText
                };
                _repository.AddComment(newComment);
                _repository.SaveChanges();

                return RedirectToAction("AnimalDetails", new { id = comment.AnimalId });
            }

            var animal = _repository.GetAnimal(comment.AnimalId);
            var compositeAnimalComment = new CompositeAnimalCommentModel()
            {
                Animal = animal,
                Comment = new Comment() { AnimalId = comment.AnimalId }
            };
            return View("AnimalDetails", compositeAnimalComment);
        }
    }
}
