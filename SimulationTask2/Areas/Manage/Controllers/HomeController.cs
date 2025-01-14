using Microsoft.AspNetCore.Mvc;
using SimulationTask2.DAL.Context;

namespace SimulationTask2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        private readonly AppDbContext dbContext;

        public HomeController(AppDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
