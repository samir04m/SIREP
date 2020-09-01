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
    public class RepresentanteController : Controller
    {
        private ApplicationDbContext _context;

        public RepresentanteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Admin/Representantes")]
        public IActionResult Representantes()
        {
            var lista = _context.RepresentantesLegales.Include("Centros").ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RepresentanteLegal representante)
        {
            if (ModelState.IsValid)
            {
                _context.RepresentantesLegales.Add(representante);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = representante.Id });
            }
            return View(representante);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            RepresentanteLegal representante = _context.RepresentantesLegales.Find(id);
            if (representante == null)
            {
                return NotFound();
            }
            return View(representante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RepresentanteLegal representante)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(representante).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = representante.Id });
            }
            return View(representante);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            RepresentanteLegal representante = _context.RepresentantesLegales.Include("Centros").Where(x => x.Id == id).FirstOrDefault();
            if (representante == null)
            {
                return NotFound();
            }
            return View(representante);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();
            
            RepresentanteLegal representante = _context.RepresentantesLegales.Include("Centros").Where(x => x.Id == id).FirstOrDefault();
            
            if (representante == null) return NotFound();

            if (representante.Centros.Count() > 0) return RedirectToAction("Details", new { id = representante.Id });

            return View(representante);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepresentanteLegal representante = _context.RepresentantesLegales.Include("Centros").Where(x => x.Id == id).FirstOrDefault();
            if (representante.Centros.Count() == 0)
            {
                _context.RepresentantesLegales.Remove(representante);
                _context.SaveChanges();
            }
            return RedirectToAction("Representantes");
        }

    }
}