using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sirep.Areas.Admin.Models;
using Sirep.Data;
using Sirep.Library;

namespace Sirep.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private ApplicationDbContext _context;
        private LUsuario usuarios;

        public AdminController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            usuarios = new LUsuario(userManager, signInManager, roleManager, context);
        }

        [Route("/Admin/Inicio")]
        public IActionResult AdminIndex()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("Usuarios", usuarios.getUsuariosAsync(null, 0).Result.Count);
            dict.Add("Centros", _context.Centros.Count());
            dict.Add("Especies", _context.Especies.Count());
            dict.Add("Representantes", _context.RepresentantesLegales.Count());
            dict.Add("Permisos", _context.Permisos.Count());
            dict.Add("Instituciones", _context.Instituciones.Count());
            dict.Add("Cuencas", _context.Cuencas.Count());

            return View(dict);
        }

        [Route("/Admin/Usuarios")]
        public IActionResult Usuarios()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var data = usuarios.getUsuariosAsync(null, 0);
                if (0 < data.Result.Count)
                {
                    return View(data.Result);
                }
                return View(new List<UsuarioInputModel>());
            }
            else
            {
                return Redirect("/");
            }
        }

    }
}