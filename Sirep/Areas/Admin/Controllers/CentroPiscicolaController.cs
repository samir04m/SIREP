using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sirep.Areas.Admin.Models;
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

        [HttpGet]
        public IActionResult Create2()
        {
            ViewBag.DepartamentoId = _selectList.DepartamentosSelectList();
            ViewBag.RepresentanteLegalId = _selectList.RepresentantesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
                                    .Include("CentroUsuarios").Include("PermisoCentros")
                                    .Where(x => x.Id == id).FirstOrDefault();
            if (centro == null)
            {
                return NotFound();
            }
            ViewBag.Usuarios = _selectList.UsuariosCentroSelectList(centro.CentroUsuarios);
            ViewBag.Permisos = _selectList.PermisosSelectList();
            ViewBag.Instituciones = _selectList.InstitucionesSelectList();
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
            return RedirectToAction("Centros");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUsuario(int CentroId, int UsuarioId)
        {
            if (!CentroId.Equals(0) && !UsuarioId.Equals(0))
            {
                var centro = _context.Centros.Find(CentroId);
                var usuario = _context.Usuarios.Find(UsuarioId);
                if (centro != null && usuario != null)
                {
                    CentroUsuario centrousuario = new CentroUsuario();
                    centrousuario.CentroId = CentroId;
                    centrousuario.UsuarioId = UsuarioId;
                    _context.Add(centrousuario);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Details", new { id = CentroId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUsuario(int CentroId, int CentroUsuarioId)
        {
            if (!CentroUsuarioId.Equals(0))
            {
                var centrousuario = _context.CentroUsuarios.Find(CentroUsuarioId);
                if (centrousuario != null)
                {
                    _context.Remove(centrousuario);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Details", new { id = CentroId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPermiso(int CentroId, int PermisoId, int InstitucionId)
        {
            if (!CentroId.Equals(0) && !PermisoId.Equals(0) && !InstitucionId.Equals(0))
            {
                var centro = _context.Centros.Find(CentroId);
                var permiso = _context.Permisos.Find(PermisoId);
                var institucion = _context.Permisos.Find(InstitucionId);
                if (centro != null && permiso != null && institucion != null)
                {
                    PermisoCentro permisoCentro = new PermisoCentro();
                    permisoCentro.CentroId = CentroId;
                    permisoCentro.PermisoId = PermisoId;
                    permisoCentro.InstitucionId = InstitucionId;
                    _context.Add(permisoCentro);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Details", new { id = CentroId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemovePermiso(int CentroId, int PermisoCentroId)
        {
            if (!PermisoCentroId.Equals(0))
            {
                var permisoCentro = _context.PermisoCentros.Find(PermisoCentroId);
                if (permisoCentro != null)
                {
                    _context.Remove(permisoCentro);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Details", new { id = CentroId });
        }



    }
}