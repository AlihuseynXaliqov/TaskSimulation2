using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationTask2.DAL.Context;

namespace SimulationTask2.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly AppDbContext dbContext;

        public HomeController(AppDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ViewBag.Position=dbContext.Positions.ToList();
           var members= dbContext.Members.Include(x=>x.Position).ToList();
            return View(members);
        }
    }
}
