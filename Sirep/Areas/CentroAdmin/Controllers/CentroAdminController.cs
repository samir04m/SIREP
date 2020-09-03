using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirep.Data;
using Sirep.Models;

namespace Sirep.Areas.CentroAdmin.Controllers
{
    [Area("CentroAdmin")]
    public class CentroAdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;

        public CentroAdminController(
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("/Centro/Inicio")]
        [Authorize(Roles = "Centro")]
        public IActionResult CentroIndex()
        {
            var userEmail = User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Name)).Value;
            var user_logged = _userManager.FindByEmailAsync(userEmail).Result;
            var usuario = _context.Usuarios.Include("CentroUsuarios").Where(x => x.IdUser == user_logged.Id).FirstOrDefault();
            
            List<Centro> centros = new List<Centro>();
            Dictionary<int, int> cantidadReproductores = new Dictionary<int, int>();
            foreach (var cu in usuario.CentroUsuarios.ToList())
            {
                var centro = _context.Centros.Include("Lotes").Include("Departamento").Where(x => x.Id == cu.CentroId).FirstOrDefault();
                var nReproductores = 0;
                foreach (var lote in centro.Lotes.ToList()){
                    lote.Reproductores = _context.Reproductores.Where(r => r.LoteId == lote.Id).ToList();
                    nReproductores += lote.Reproductores.Count();
                }
                centros.Add(centro);
                cantidadReproductores.Add(centro.Id, nReproductores);
            }
            ViewBag.cantidadReproductores = cantidadReproductores;
            return View(centros);
        }
    }
}