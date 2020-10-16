using Microsoft.EntityFrameworkCore;
using Sirep.Data;
using Sirep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Library
{
    public class DatosSimulador
    {
        private ApplicationDbContext db;

        public DatosSimulador(ApplicationDbContext context)
        {
            db = context;
        }

        public List<EspecieReproductores> GetReproductores(Centro centro)
        {
            List<EspecieReproductores> list = new List<EspecieReproductores>();

            foreach (var especie in db.Especies.ToList())
            {
                var reproductores = db.Reproductores.Include("Datos").Include("Locus")
                                                    .Where(x => x.Lote.CentroId == centro.Id && x.EspecieId == especie.Id).ToList();

                if (reproductores.Count() > 0)
                {
                    List<ReproductorSimulador> listRS = new List<ReproductorSimulador>();
                    int numeroReproductoresValidos = 0;

                    var numerosLocus = GetNumerosLocus(reproductores);
                    if (numerosLocus.Count() > 0)
                    {
                        foreach (var rep in reproductores)
                        {
                            Dictionary<int, ValoresLocus> dictLocus = new Dictionary<int, ValoresLocus>();
                            bool estado = true;
                            foreach (var numero in numerosLocus)
                            {
                                var locus = rep.Locus.Where(z => z.Numero == numero).FirstOrDefault();
                                var parLocus = new ValoresLocus();
                                if (locus != null)
                                {
                                     parLocus = new ValoresLocus {
                                        ValorA = locus.ValorA,
                                        ValorB = locus.ValorB 
                                     };
                                }
                                else
                                {
                                    parLocus = new ValoresLocus {
                                        ValorA = 0,
                                        ValorB = 0
                                    };
                                    estado = false;
                                }
                                dictLocus.Add(numero, parLocus);
                            }

                            if (rep.Datos == null) estado = false;

                            var reproductorSimulador = new ReproductorSimulador
                            {
                                Id = rep.Id,
                                ChipId = rep.ChipId,
                                Sexo = (rep.Datos != null) ? rep.Datos.Sexo : "?",
                                Locus = dictLocus,
                                Estado = estado
                            };
                            if (estado) numeroReproductoresValidos += 1;
                            listRS.Add(reproductorSimulador);
                        }
                    }
                    var especieReproductores = new EspecieReproductores
                    {
                        Centro = centro,
                        Especie = especie,
                        NumerosLocus = numerosLocus,
                        Reproductores = listRS,
                        NumeroReproductoresValidos = numeroReproductoresValidos
                    };
                    list.Add(especieReproductores);

                }
            }

            return list;
        }

        public List<int> GetNumerosLocus(List<Reproductor> reproductores)
        {
            List<int> list = new List<int>();

            foreach (var rep in reproductores)
                foreach (var locus in rep.Locus)
                    if (list.IndexOf(locus.Numero) == -1) list.Add(locus.Numero);

            return list;
        }

        public string GuardarInforme(EspecieReproductores especieReproductor, DateTime fecha)
        {
            var informe = new Informe
            {
                Fecha = fecha,
                CentroId = especieReproductor.Centro.Id,
                EspecieId = especieReproductor.Especie.Id
            };
            db.Informes.Add(informe);
            db.SaveChanges();
            return "Informe"+informe.Id;
        }

    }
}
