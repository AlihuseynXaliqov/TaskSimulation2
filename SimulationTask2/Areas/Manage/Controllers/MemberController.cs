using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SimulationTask2.Areas.Manage.DTOS.Member;
using SimulationTask2.Areas.Manage.Helpers.Exception;
using SimulationTask2.DAL.Context;
using SimulationTask2.Helper.Files;
using SimulationTask2.Models;

namespace SimulationTask2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MemberController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment web;

        public MemberController(AppDbContext dbContext,IMapper mapper,IWebHostEnvironment web)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.web = web;
        }

        public IActionResult Index()
        {
            var members = dbContext.Members.Include(x => x.Position).ToList();
            return View(members);
        }

        public IActionResult Create()
        {
            ViewBag.Position = dbContext.Positions.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMemberDto dto)
        {


            if (!ModelState.IsValid)
            {
                ViewBag.Position = dbContext.Positions.ToList();
                return View(dto);
            }

            if (dto.imageUrl != null)
            {
                if (!dto.formFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("formFile", "Sekili duzgun sec");
                    return View();
                }
                if(dto.formFile.Length> 2097152)
                {

                    ModelState.AddModelError("formFile", "Sekili duzgun sec");
                    return View();
                }

            }
            dto.imageUrl = dto.formFile.Upload(web.WebRootPath, "Upload/Member");

            var position =mapper.Map<Member>(dto);
            dbContext.Members.Add(position);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int id)
        {
            ViewBag.Position = dbContext.Positions.ToList();
            if (id<=0) throw new NegativeIdException();
            var position=dbContext.Members.Include(x=>x.Position).FirstOrDefault(x => x.Id == id);
            var newPosition=mapper.Map<UpdateMemberDto>(position);
            return View(newPosition);

        }

        [HttpPost]
        public IActionResult Update(UpdateMemberDto dto)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Position = dbContext.Positions.ToList(); 
                return View(dto);
            }

            var position = dbContext.Members.Include(x => x.Position).FirstOrDefault(x => x.Id == dto.Id);
            if (dto.imageUrl != null)
            {
                if (!dto.formFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("formFile", "Sekili duzgun sec");
                    return View();
                }
                if (dto.formFile.Length > 2097152)
                {
                    ModelState.AddModelError("formFile", "Sekili duzgun sec");
                    return View();
                }
                if (!string.IsNullOrEmpty(dto.imageUrl))
                {
                    FileExtention.Delete(web.WebRootPath,"Upload/Member",dto.imageUrl);
                }
                position.imageUrl = dto.formFile.Upload(web.WebRootPath, "Upload/Member");

            }
            position.Name=dto.Name;
            position.PositionId=dto.PositionId;
            var newPosition = mapper.Map<Member>(position);
            dbContext.Members.Update(position);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var position = dbContext.Members.Include(x => x.Position).FirstOrDefault(x => x.Id == id);
            dbContext.Remove(position);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
