using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirep.Data;
using Sirep.Models;

namespace Sirep.Areas.CentroAdmin.Controllers
{
    [Area("CentroAdmin")]
    [Authorize(Roles = "Centro")]
    public class LoteController : Controller
    {
        private ApplicationDbContext _context;

        public LoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Centro/Lotes/{CentroId}")]
        public IActionResult Lotes(int CentroId)
        {
            var centro = _context.Centros.Include("Lotes").Where(x => x.Id == CentroId).Single();
            foreach (var lote in centro.Lotes.ToList())
            {
                lote.Reproductores = _context.Reproductores.Where(x => x.LoteId == lote.Id).ToList();
            }
            return View(centro);
        }

        [HttpGet]
        [Route("/Lote/Create/{CentroId}")]
        public IActionResult Create(int CentroId)
        {
            ViewBag.CentroId = CentroId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Lote/Create/{CentroId}")]
        public ActionResult Create(Lote lote)
        {
            if (ModelState.IsValid)
            {
                _context.Lotes.Add(lote);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = lote.Id });
            }
            return View(lote);
        }

        [Route("/Lote/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Lote lotes = _context.Lotes.Find(id);
            if (lotes == null)
            {
                return NotFound();
            }
            return View(lotes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Lote/Edit/{id}")]
        public ActionResult Edit(Lote lote)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(lote).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = lote.Id });
            }
            return View(lote);
        }

        [Route("/Lote/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null) return BadRequest();
            
            Lote lote = _context.Lotes.Include("Centro").Include("Reproductores").Where(x => x.Id == id).FirstOrDefault();
            
            if (lote == null) return NotFound();
            
            return View(lote);
        }

        [Route("/Lote/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();

            Lote lote = _context.Lotes.Include("Centro").Include("Reproductores").Where(x => x.Id == id).FirstOrDefault();

            if (lote == null) return NotFound();

            if (lote.Reproductores.Count() > 0) return RedirectToAction("Details", new { id = lote.Id });

            return View(lote);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/Lote/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var CentroId = 0;
            Lote lote = _context.Lotes.Include("Reproductores").Where(x => x.Id == id).FirstOrDefault();
            if (lote.Reproductores.Count() == 0)
            {
                CentroId = lote.CentroId; 
                _context.Lotes.Remove(lote);
                _context.SaveChanges();
            }
            return RedirectToAction("Lotes", new { CentroId = CentroId });
        }
    }
}