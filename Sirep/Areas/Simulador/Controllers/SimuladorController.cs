using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        [Route("/Simulador/Procesar")]
        public IActionResult Procesar()
        {
            if (datos == null) return BadRequest();
            if (datos.Count() == 0) return BadRequest();

            DateTime start = DateTime.Now;
            Core core = new Core(new Notificador(start));

            foreach (var item in datos)
            {
                if (item.NumeroReproductoresValidos >= 4)
                {
                    core.AddEstacion(item, datosSimulador);
                }
            }
            core.Procesar();

            return Redirect("/Simulador/Resultados");
        }

        [Authorize(Roles = "Centro")]
        [Route("/Simulador/Informes/{CentroId}")]
        public IActionResult Informes(int CentroId)
        {
            var informes = db.Informes.Include("Centro").Include("Especie").Where(x => x.CentroId == CentroId)
                                       .OrderByDescending(x => x.Fecha).ToList();

            return View(informes);
        }

        [Authorize(Roles = "Centro")]
        [Route("/Simulador/VerInforme/{id}")]
        public IActionResult VerInforme(int id)
        {
            var ruta = "Content/Informes/Informe"+id+".html";
            if (Directory.Exists(ruta))
            {

            }
            return View();
        }

        class Notificador : IReceptorAvance
        {
            private DateTime start;

            public Notificador(DateTime start)
            {
                // TODO: Complete member initialization
                this.start = start;
            }
            public void SiguientePaso(double factoCompletitud)
            {
                //Console.Write("\r{0:N2}%      ", factoCompletitud * 100);
            }

            public void EmpezadoCalculoEstacion(string nombre)
            {
                //Console.WriteLine("{0} - Se empezo el calculo de {1}...", DateTime.Now - start, nombre);
            }

            public void FinalizadoCalculoEstacion(string nombre)
            {
                //Console.WriteLine("\r{0} - Se finalizo el calculo de {1}", DateTime.Now - start, nombre);
            }

            public void EmpezadoInforme(string nombre)
            {
                //Console.WriteLine("{0} - Se empezo el informe de {1}...", DateTime.Now - start, nombre);
            }

            public void FinalizadoInforme(string nombre)
            {
                //Console.WriteLine("{0} - Se finalizo el informe de {1}", DateTime.Now - start, nombre);
            }
        }
    }
}