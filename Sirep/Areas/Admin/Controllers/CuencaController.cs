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
    public class CuencaController : Controller
    {
        private ApplicationDbContext _context;

        public CuencaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Admin/Cuencas")]
        public IActionResult Cuencas()
        {
            var lista = _context.Cuencas.Include("Reproductores").ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cuenca cuenca)
        {
            if (ModelState.IsValid)
            {
                _context.Cuencas.Add(cuenca);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = cuenca.Id });
            }
            return View(cuenca);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Cuenca cuenca = _context.Cuencas.Find(id);
            if (cuenca == null)
            {
                return NotFound();
            }
            return View(cuenca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cuenca cuenca)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(cuenca).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = cuenca.Id });
            }
            return View(cuenca);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Cuenca cuenca = _context.Cuencas.Include("Reproductores").Where(x => x.Id == id).FirstOrDefault();
            if (cuenca == null)
            {
                return NotFound();
            }
            return View(cuenca);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();
            
            Cuenca cuenca = _context.Cuencas.Include("Reproductores").Where(x => x.Id == id).FirstOrDefault();

            if (cuenca == null) return NotFound();

            if (cuenca.Reproductores.Count() > 0) return RedirectToAction("Details", new { id = cuenca.Id });

            return View(cuenca);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuenca cuenca = _context.Cuencas.Include("Reproductores").Where(x => x.Id == id).FirstOrDefault();
            if (cuenca.Reproductores.Count() == 0)
            {
                _context.Cuencas.Remove(cuenca);
                _context.SaveChanges();
            }
            return RedirectToAction("Cuencas");
        }
    }
}