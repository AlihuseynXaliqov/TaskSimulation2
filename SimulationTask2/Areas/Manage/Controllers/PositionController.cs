using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationTask2.Areas.Manage.DTOS.Position;
using SimulationTask2.Areas.Manage.Helpers.Exception;
using SimulationTask2.DAL.Context;
using SimulationTask2.Models;

namespace SimulationTask2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public PositionController(AppDbContext dbContext, IMapper mapper)
        {

            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var positions = dbContext.Positions.ToList();
            return View(positions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePositionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            if (dto == null) throw new NotFoundException();

            var positions = mapper.Map<Position>(dto);
            dbContext.Positions.Add(positions);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Update(int Id)
        {
            if (Id <= 0) throw new NegativeIdException();
            var position = dbContext.Positions.FirstOrDefault(x => x.Id == Id);
            return View(position);
        }
        [HttpPost]
        public IActionResult Update(UpdatePositiionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var position = dbContext.Positions.FirstOrDefault(x => x.Id == dto.Id);
            if (position == null) return BadRequest("Tapilmadi");
            position.Name = dto.Name;
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int Id)
        {
            if (Id <= 0) throw new NegativeIdException();
            var position = dbContext.Positions.FirstOrDefault(x => x.Id == Id);
            dbContext.Remove(position);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
