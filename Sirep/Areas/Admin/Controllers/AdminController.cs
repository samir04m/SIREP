using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sirep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        [Route("/Admin/Inicio")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            return View();
        }
    }
}