using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sirep.Data;
using Sirep.Models;

namespace Sirep.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IServiceProvider _serviceProvider;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider,
                                ApplicationDbContext context)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InfoCentros()
        {
            var centros = _context.Centros.Include("Departamento").ToList();
            return View(centros);
        }

        public IActionResult InfoEspecies()
        {
            var especies = _context.Especies.ToList().OrderBy(x=>x.NombreComun);
            return View(especies);
        }

        public IActionResult Agradecimientos()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CrearRoles()
        {
            await CreateRolesAsync(_serviceProvider);
            return Content("Roles Creados");
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Admin", "Centro" };
            foreach (var item in rolesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }

    }
}
