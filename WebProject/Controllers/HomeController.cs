using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProject.Filters;
using ZooLib.Repository;

namespace WebProject.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ActionsFilter))]
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
            var topTwoAnimals = await _repository.Top2Animals();
            return View(topTwoAnimals);
        }
    }
}
