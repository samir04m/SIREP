using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirep.Areas.Simulador.Library;
using Sirep.Data;
using Sirep.Library;
using Sirep.Models;

namespace Sirep.Areas.Simulador.Controllers
{
    [Area("Simulador")]
    public class SimuladorController : Controller
    {
        private ApplicationDbContext db;
        private DatosSimulador datosSimulador;
        private static List<EspecieReproductores> datos;

        public SimuladorController(ApplicationDbContext context)
        {
            db = context;
            datosSimulador = new DatosSimulador(context);
        }

        [Authorize(Roles = "Centro")]
        [Route("/Simulador/Centro/{id}")]
        public IActionResult SimuladorCentro(int id)
        {
            var centro = db.Centros.Find(id);
            if (centro == null) return NotFound();

            datos = datosSimulador.GetReproductores(centro);

            ViewBag.Centro = centro;

            return View(datos);
        }

        [Authorize(Roles = "Centro")]
        [Route("/Simulador/Procesar/{id}")]
        public IActionResult Procesar(int id)
        {
            if (datos == null) return BadRequest();
            if (datos.Count() == 0) return BadRequest();

            DateTime start = DateTime.Now;
            Core core = new Core(new Notificador(start));

            foreach (var item in datos)
            {
                if (item.Especie.Id == id)
                {
                    if (item.NumeroReproductoresValidos >= 4)
                    {
                        core.AddEstacion(item, datosSimulador);
                        core.Procesar();
                        return Redirect("/Simulador/Informes/"+item.Centro.Id);
                    }
                    break;
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "Centro")]
        [Route("/Simulador/Informes/{CentroId}")]
        public IActionResult Informes(int CentroId)
        {
            var informes = db.Informes.Include("Centro").Include("Especie").Where(x => x.CentroId == CentroId)
                                       .OrderByDescending(x => x.Fecha).ToList();

            ViewBag.CentroId = CentroId;
            return View(informes);
        }

        [Authorize(Roles = "Centro")]
        [Route("/Simulador/DeleteInforme/{id}")]
        public IActionResult DeleteInforme(int id)
        {
            var informe = db.Informes.Find(id);
            if (informe == null) return NotFound();

            var ruta = "wwwroot/Content/Informes/Informe"+id+".html";

            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }
            db.Informes.Remove(informe);
            db.SaveChanges();

            return Redirect("/Simulador/Informes/"+informe.CentroId);
        }


        class Notificador : IReceptorAvance
        {
            private DateTime start;

            public Notificador(DateTime start)
            {
                this.start = start;
            }
            public void SiguientePaso(double factoCompletitud) { }
            public void EmpezadoCalculoEstacion(string nombre) { }
            public void FinalizadoCalculoEstacion(string nombre) { }
            public void EmpezadoInforme(string nombre) { }
            public void FinalizadoInforme(string nombre) { }
        }

    }
}