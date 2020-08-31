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
    public class PermisoController : Controller
    {
        private ApplicationDbContext _context;

        public PermisoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Admin/Permisos")]
        public IActionResult Permisos()
        {
            var lista = _context.Permisos.ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                _context.Permisos.Add(permiso);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = permiso.Id });
            }
            return View(permiso);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Permiso permiso = _context.Permisos.Find(id);
            if (permiso == null)
            {
                return NotFound();
            }
            return View(permiso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(permiso).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = permiso.Id });
            }
            return View(permiso);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Permiso permiso = _context.Permisos.Find(id);
            if (permiso == null)
            {
                return NotFound();
            }
            return View(permiso);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Permiso permiso = _context.Permisos.Find(id);
            if (permiso == null)
            {
                return NotFound();
            }
            return View(permiso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permiso permiso = _context.Permisos.Find(id);
            _context.Permisos.Remove(permiso);
            _context.SaveChanges();
            return RedirectToAction("Permisos");
        }
    }
}