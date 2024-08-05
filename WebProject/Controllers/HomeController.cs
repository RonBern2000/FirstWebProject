using Microsoft.AspNetCore.Mvc;
using WebProject.Filters;
using WebProject.Repository;

namespace WebProject.Controllers
{
    [TypeFilter(typeof(ErrorsExceptionFilter))]
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Home controller/Index was entered");
            var topTwoAnimals = await _repository.Top2Aniamls();
            return View(topTwoAnimals);
        }
    }
}
