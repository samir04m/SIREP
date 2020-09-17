using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirep.Data;
using Sirep.Library;
using Sirep.Models;

namespace Sirep.Areas.CentroAdmin.Controllers
{
    [Area("CentroAdmin")]
    [Authorize(Roles = "Centro")]
    public class ReproductorController : Controller
    {
        private ApplicationDbContext _context;
        private EntitySelectList _selectList;

        public ReproductorController(ApplicationDbContext context)
        {
            _context = context;
            _selectList = new EntitySelectList(context);
        }

        [Route("/Centro/Reproductores/{CentroId}")]
        public IActionResult Reproductores(int CentroId)
        {
            var reproductores = _context.Reproductores.Include("Especie").Include("Lote")
                                            .Where(x => x.Lote.CentroId == CentroId).OrderBy(x=>x.Especie.NombreComun).ToList();
            ViewBag.Centro = _context.Centros.Find(CentroId);
            return View(reproductores);
        }

        [HttpGet]
        [Route("/Reproductor/Create/{CentroId}")]
        public IActionResult Create(int CentroId)
        {
            ViewBag.EspecieId = _selectList.EspeciesSelectList();
            ViewBag.LoteId = _selectList.LotesSelectList(CentroId);
            ViewBag.CuencaId = _selectList.CuencasSelectList();
            ViewBag.CentroId = CentroId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Reproductor/Create/{CentroId}")]
        public ActionResult Create(Reproductor reproductor)
        {
            if (ModelState.IsValid)
            {
                _context.Reproductores.Add(reproductor);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = reproductor.Id });
            }
            return View(reproductor);
        }

        [Route("/Reproductor/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            
            Reproductor reproductor = _context.Reproductores.Include("Lote").Where(x => x.Id == id).FirstOrDefault();

            if (reproductor == null) return NotFound();

            ViewBag.EspecieId = _selectList.EspeciesSelectList(true);
            ViewBag.LoteId = _selectList.LotesSelectList(reproductor.Lote.CentroId, true);
            ViewBag.CuencaId = _selectList.CuencasSelectList(true);
            ViewBag.CentroId = reproductor.Lote.CentroId;

            return View(reproductor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Reproductor/Edit/{id}")]
        public ActionResult Edit(Reproductor reproductor)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(reproductor).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = reproductor.Id });
            }
            return View(reproductor);
        }

        [Route("/Reproductor/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null) return BadRequest();

            Reproductor reproductor = _context.Reproductores.Include("Especie").Include("Lote").Include("Cuenca")
                                                            .Include("Datos").Include("Imagenes").Include("Locus")
                                                            .Where(x => x.Id == id).FirstOrDefault();

            if (reproductor == null) return NotFound();
            
            ViewBag.CentroId = reproductor.Lote.CentroId;

            return View(reproductor);
        }

        [Route("/Reproductor/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();

            Reproductor reproductor = _context.Reproductores.Include("Especie").Include("Lote").Include("Cuenca")
                                                            .Where(x => x.Id == id).FirstOrDefault();

            if (reproductor == null) return NotFound();

            ViewBag.CentroId = reproductor.Lote.CentroId;

            return View(reproductor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/Reproductor/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            int CentroId = 0;
            Reproductor reproductor = _context.Reproductores.Include("Lote").Where(x => x.Id == id).FirstOrDefault();
            if (reproductor != null)
            {
                CentroId = reproductor.Lote.CentroId;
                _context.Reproductores.Remove(reproductor);
                _context.SaveChanges();
            }
            return RedirectToAction("Reproductores", new { CentroId = CentroId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Reproductor/CreateLocus")]
        public ActionResult CreateLocus(int ReproductorId, int ValorA, int ValorB, int Numero)
        {
            if (!ReproductorId.Equals(0) && !ValorA.Equals(0) && !ValorB.Equals(0) && !Numero.Equals(0))
            {
                var reproductor = _context.Reproductores.Find(ReproductorId);
                if (reproductor != null)
                {
                    var locus = new LocusReproductor
                    {
                        ReproductorId = ReproductorId,
                        ValorA = ValorA,
                        ValorB = ValorB,
                        Numero = Numero
                    };
                    _context.Add(locus);
                    _context.SaveChanges();
                    return RedirectToAction("Details", new { id = reproductor.Id });
                }
            }
            return BadRequest();
        }

        [Route("/Reproductor/EditLocus/{id}")]
        public ActionResult EditLocus(int? id)
        {
            if (id == null) return BadRequest();

            LocusReproductor locus = _context.LocusReproductores.Find(id);

            if (locus == null) return NotFound();

            return View(locus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Reproductor/EditLocus/{id}")]
        public ActionResult EditLocus(LocusReproductor locus)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(locus).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = locus.ReproductorId });
            }
            return View(locus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Reproductor/RemoveLocus")]
        public ActionResult RemoveLocus(int LocusId)
        {
            if (!LocusId.Equals(0))
            {
                var locus = _context.LocusReproductores.Find(LocusId);
                if (locus != null)
                {
                    _context.Remove(locus);
                    _context.SaveChanges();
                    return RedirectToAction("Details", new { id = locus.ReproductorId });
                }
            }
            return BadRequest();
        }


    }
}