using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Administrator()
        {
            return View();
        }
    }
}
