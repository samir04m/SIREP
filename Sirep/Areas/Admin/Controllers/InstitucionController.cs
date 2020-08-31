using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirep.Data;
using Sirep.Models;

namespace Sirep.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InstitucionController : Controller
    {
        private ApplicationDbContext _context;

        public InstitucionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Admin/Instituciones")]
        public IActionResult Instituciones()
        {
            var lista = _context.Instituciones.ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                _context.Instituciones.Add(institucion);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = institucion.Id });
            }
            return View(institucion);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Institucion institucion = _context.Instituciones.Find(id);
            if (institucion == null)
            {
                return NotFound();
            }
            return View(institucion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(institucion).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = institucion.Id });
            }
            return View(institucion);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Institucion institucion = _context.Instituciones.Find(id);
            if (institucion == null)
            {
                return NotFound();
            }
            return View(institucion);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Institucion institucion = _context.Instituciones.Find(id);
            if (institucion == null)
            {
                return NotFound();
            }
            return View(institucion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Institucion institucion = _context.Instituciones.Find(id);
            _context.Instituciones.Remove(institucion);
            _context.SaveChanges();
            return RedirectToAction("Instituciones");
        }
    }
}