using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirep.Data;
using Sirep.Models;

namespace Sirep.Areas.CentroAdmin.Controllers
{
    [Area("CentroAdmin")]
    [Authorize(Roles = "Centro")]
    public class ImagenReproductorController : Controller
    {
        private ApplicationDbContext _context;

        public ImagenReproductorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public int ReproductorId { get; set; }
            public int? Numero { get; set; }
            [Required]
            [DataType(DataType.Upload)]
            [MaxFileSize(ErrorMessage = "La imagen enviada supera los 5 MB.")]
            public IFormFile Imagen { get; set; }

            private class MaxFileSizeAttribute : ValidationAttribute
            {
                public override bool IsValid(object value)
                {
                    var file = value as IFormFile;
                    if (file.Length <= 1048576 * 5) return true;
                    return false;
                }
            }
        }

        [HttpGet]
        [Route("/ImagenReproductor/Create/{ReproductorId}")]
        public IActionResult Create(int ReproductorId)
        {
            ViewBag.Reproductor = _context.Reproductores.Find(ReproductorId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/ImagenReproductor/Create/{ReproductorId}")]
        public async Task<ActionResult> CreateAsync()
        {
            if (ModelState.IsValid)
            {
                var _imagen = new ImagenReproductor
                {
                    ReproductorId = Input.ReproductorId,
                    Numero = Input.Numero,
                    Imagen = await ByteImageAsync(Input.Imagen)
                };
                _context.ImagenReproductores.Add(_imagen);
                _context.SaveChanges();

                return Redirect("/Reproductor/Details/" + _imagen.ReproductorId);
            }
            ViewBag.Reproductor = _context.Reproductores.Find(Input.ReproductorId);
            return View();
        }

        public async Task<byte[]> ByteImageAsync(IFormFile FileImagen)
        {
            if (FileImagen != null)
            {
                using (var memoryStream = new MemoryStream(1048576 * 5))
                {
                    await FileImagen.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null;
        }

        [Route("/ImagenReproductor/List/{ReproductorId}")]
        public ActionResult List(int ReproductorId)
        {
            var imagenes = _context.ImagenReproductores.Where(x => x.ReproductorId == ReproductorId).ToList();
            ViewBag.Reproductor = _context.Reproductores.Find(ReproductorId);
            return View(imagenes);
        }

        [HttpGet]
        [Route("/ImagenReproductor/Edit")]
        public ActionResult Edit(int Id, int? Numero)
        {
            if (!Id.Equals(0) && !Numero.Equals(0))
            {
                var imagenReproductor = _context.ImagenReproductores.Find(Id);
                if (imagenReproductor != null)
                {
                    imagenReproductor.Numero = Numero;
                    _context.Entry(imagenReproductor).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Redirect("/ImagenReproductor/List/" + imagenReproductor.ReproductorId);
                }
            }
            return BadRequest();
        }

        [Route("/ImagenReproductor/Delete/{Id}")]
        public ActionResult Delete(int Id)
        {
            if (!Id.Equals(0))
            {
                var imagenReproductor = _context.ImagenReproductores.Find(Id);
                if (imagenReproductor != null)
                {
                    int ReproductorId = imagenReproductor.ReproductorId;
                    _context.ImagenReproductores.Remove(imagenReproductor);
                    _context.SaveChanges();
                    return Redirect("/ImagenReproductor/List/" + ReproductorId);
                }
            }
            return BadRequest();
        }
    }

}