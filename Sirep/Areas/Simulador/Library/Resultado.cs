using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Areas.Simulador.Library
{
    public class Resultado : IEnumerable<ResultadoParaLoci>
    {
        internal ResultadoParaLoci[] ResultadosParaLoci;
        internal int NumLociTotal;
        internal List<string> NombresLoci;

        internal CoreEstacion Estacion;

        internal Resultado(CoreEstacion estacion, Pareja[] parejasProcesadas, int minLoci, int MaxLoci, List<string> NombresLoci)
        {
            Estacion = estacion;

            ResultadosParaLoci = new ResultadoParaLoci[MaxLoci - minLoci + 1];
            var parejasOrdenadas = parejasProcesadas.OrderByDescending(pp => pp.Ho);
            this.NombresLoci = NombresLoci;
            NumLociTotal = MaxLoci;

            for (int loci = MaxLoci; loci >= minLoci; loci--)
            {
                var parejas = parejasOrdenadas.Where(p => p.NumLoci == loci).ToArray();
                var resultado = new ResultadoParaLoci()
                {
                    Restultado = this,
                    NumLociComun = loci,
                    Parejas = parejas,
                    ParejasAgrupadasPorHembra = parejas.GroupBy(p => p.Hembra).Select(gp => new ParejasAgrupadas(gp, gp.Key))
                    .OrderByDescending(pa => pa.HoPromedio).ToArray(),
                    ParejasAgrupadasPorMacho = parejas.GroupBy(p => p.Macho).Select(gp => new ParejasAgrupadas(gp, gp.Key))
                    .OrderByDescending(pa => pa.HoPromedio).ToArray()
                };
                ResultadosParaLoci[MaxLoci - loci] = resultado;
            }
        }

        public string GetNombreLocus(int index)
        {
            return NombresLoci[index];
        }

        public IEnumerator<ResultadoParaLoci> GetEnumerator()
        {
            return ((IEnumerable<ResultadoParaLoci>)ResultadosParaLoci).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ResultadosParaLoci.GetEnumerator();
        }

        public Core Core
        {
            get { return Estacion.Core; }
        }
    }
}
