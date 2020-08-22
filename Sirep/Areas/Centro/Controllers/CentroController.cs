using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sirep.Areas.Centro.Controllers
{
    [Area("Centro")]
    public class CentroController : Controller
    {
        [Route("/Centro/Inicio")]
        [Authorize(Roles = "Centro")]
        public IActionResult CentroIndex()
        {
            return View();
        }
    }
}