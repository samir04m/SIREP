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
    public class EspecieController : Controller
    {
        private ApplicationDbContext _context;

        public EspecieController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Admin/Especies")]
        public IActionResult Especies()
        {
            var lista = _context.Especies.ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Especie especie)
        {
            if (ModelState.IsValid)
            {
                _context.Especies.Add(especie);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = especie.Id });
            }
            return View(especie);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Especie especie = _context.Especies.Find(id);
            if (especie == null)
            {
                return NotFound();
            }
            return View(especie);
        }

        [HttpPost]
        public ActionResult Edit(Especie especie)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(especie).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = especie.Id });
            }
            return View(especie);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Especie especie = _context.Especies.Find(id);
            if (especie == null)
            {
                return NotFound();
            }
            return View(especie);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Especie especie = _context.Especies.Find(id);
            if (especie == null)
            {
                return NotFound();
            }
            return View(especie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Especie especie = _context.Especies.Find(id);
            _context.Especies.Remove(especie);
            _context.SaveChanges();
            return RedirectToAction("Especies");
        }
    }
}