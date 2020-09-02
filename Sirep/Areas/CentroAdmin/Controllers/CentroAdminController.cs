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
        //private SignInManager<IdentityUser> _signInManager;
        private ApplicationDbContext _context;

        public CentroAdminController(
             UserManager<IdentityUser> userManager,
            //SignInManager<IdentityUser> signInManager
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            //_signInManager = signInManager;
        }

        [Route("/Centro/Inicio")]
        [Authorize(Roles = "Centro")]
        public IActionResult CentroIndex()
        {
            var userEmail = User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Name)).Value;
            var user_logged = _userManager.FindByEmailAsync(userEmail).Result;
            var usuario = _context.Usuarios.Include("CentroUsuarios").Where(x => x.IdUser == user_logged.Id).FirstOrDefault();
            
            List<Centro> centros = new List<Centro>();
            foreach (var cu in usuario.CentroUsuarios.ToList())
            {
                var centro = _context.Centros.Find(cu.CentroId);
                centros.Add(centro);
            }
            return View(centros);
        }
    }
}