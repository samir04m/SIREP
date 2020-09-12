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
    public class DatosReproductorController : Controller
    {
        private ApplicationDbContext _context;

        public DatosReproductorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/DatosReproductor/Create/{ReproductorId}")]
        public IActionResult Create(int ReproductorId)
        {
            var reproductor = _context.Reproductores.Include("Datos").Include("Lote").Where(x => x.Id == ReproductorId).FirstOrDefault();
            if (reproductor.Datos != null)
            {
                return Redirect("/Reproductor/Details/" + ReproductorId);
            }
            ViewBag.Reproductor = reproductor;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/DatosReproductor/Create/{ReproductorId}")]
        public ActionResult Create(DatosReproductor datos)
        {
            if (ModelState.IsValid)
            {
                _context.DatosReproductores.Add(datos);
                _context.SaveChanges();

                return Redirect("/Reproductor/Details/" + datos.ReproductorId);
            }
            ViewBag.Reproductor = _context.Reproductores.Find(datos.ReproductorId);
            return View(datos);
        }

        [Route("/DatosReproductor/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            DatosReproductor datos = _context.DatosReproductores.Include("Reproductor").Where(x => x.Id == id).FirstOrDefault();
            if (datos == null)
            {
                return NotFound();
            }
            return View(datos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/DatosReproductor/Edit/{id}")]
        public ActionResult Edit(DatosReproductor datos)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(datos).State = EntityState.Modified;
                _context.SaveChanges();
                return Redirect("/Reproductor/Details/"+datos.ReproductorId);
            }
            datos = _context.DatosReproductores.Include("Reproductor").Where(x => x.Id == datos.Id).FirstOrDefault();
            return View(datos);
        }
    }
}