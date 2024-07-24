using Microsoft.AspNetCore.Mvc;
using WebProject.Repository;

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
            var data = _repository.GetAllData();
            return View(data);
        }
    }
}
