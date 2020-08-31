using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sirep.Data;
using Sirep.Library;
using Sirep.Models;

namespace Sirep.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CentroPiscicolaController : Controller
    {
        private ApplicationDbContext _context;
        private EntitySelectList _selectList;

        public CentroPiscicolaController(ApplicationDbContext context)
        {
            _context = context;
            _selectList = new EntitySelectList (context);
        }

        [Route("/Admin/Centros")]
        public IActionResult Centros()
        {
            var lista = _context.Centros.Include("Departamento").Include("RepresentanteLegal").ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DepartamentoId = _selectList.DepartamentosSelectList();
            ViewBag.RepresentanteLegalId = _selectList.RepresentantesSelectList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Centro centro)
        {
            if (ModelState.IsValid)
            {
                _context.Centros.Add(centro);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = centro.Id });
            }
            return View(centro);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Centro centro = _context.Centros.Find(id);
            if (centro == null)
            {
                return NotFound();
            }
            ViewBag.DepartamentoId = _selectList.DepartamentosSelectList(true);
            ViewBag.RepresentanteLegalId = _selectList.RepresentantesSelectList(true);
            return View(centro);
        }

        [HttpPost]
        public ActionResult Edit(Centro centro)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(centro).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = centro.Id });
            }
            return View(centro);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Centro centro = _context.Centros
                                    .Include("Departamento").Include("RepresentanteLegal")
                                    .Where(x => x.Id == id).FirstOrDefault();
            if (centro == null)
            {
                return NotFound();
            }
            return View(centro);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Centro centro = _context.Centros
                                    .Include("Departamento").Include("RepresentanteLegal")
                                    .Where(x => x.Id == id).FirstOrDefault();
            if (centro == null)
            {
                return NotFound();
            }
            return View(centro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Centro centro = _context.Centros.Find(id);
            _context.Centros.Remove(centro);
            _context.SaveChanges();
            return RedirectToAction("Representantes");
        }

    }
}