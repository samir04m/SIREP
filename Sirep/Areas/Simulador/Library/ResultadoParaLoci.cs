using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Areas.Simulador.Library
{
    public class ResultadoParaLoci
    {
        public ParejasAgrupadas[] ParejasAgrupadasPorMacho;
        public ParejasAgrupadas[] ParejasAgrupadasPorHembra;
        public Pareja[] Parejas;
        public Resultado Restultado;
        public int NumLociComun;

        public string GetNombreLocus(int index)
        {
            return Restultado.GetNombreLocus(index);
        }

        public int NumCruces
        {
            get { return Parejas.Length; }
        }

        public int NumMachos
        {
            get { return ParejasAgrupadasPorMacho.Length; }
        }

        public int NumHembras
        {
            get { return ParejasAgrupadasPorHembra.Length; }
        }

        public int NumGametosEsperados
        {
            get { return (int)Math.Pow(2, NumLociComun); }
        }

        public int NumCigotosEsperados
        {
            get { return NumGametosEsperados * NumGametosEsperados; }
        }

        double? _HoPromedio;
        public double HoPromedio
        {
            get
            {
                if (_HoPromedio == null)
                    _HoPromedio = Parejas.Sum(p => p.Ho) / Parejas.Length;
                return _HoPromedio.Value;
            }
        }

        public double HoDE
        {
            get { return Math.Sqrt(Parejas.Sum(p => Math.Pow(p.Ho - HoPromedio, 2)) / Parejas.Length); }
        }

        double? _HePromedio;
        public double HePromedio
        {
            get
            {
                if (_HePromedio == null)
                    _HePromedio = Parejas.Sum(p => p.He) / Parejas.Length;
                return _HePromedio.Value;
            }
        }

        /// <summary>
        /// Desviación estándar de He
        /// </summary>
        public double HeDE
        {
            get { return Math.Sqrt(Parejas.Sum(p => Math.Pow(p.He - HePromedio, 2)) / Parejas.Length); }
        }

        public double FisPromedio
        {
            get { return Parejas.Sum(p => p.Fis) / Parejas.Length; }
        }

        public class ConteoAlelo
        {
            public ConteoAlelo(ushort Alelo, int Conteo, int totalCigotos)
            {
                this.Conteo = Conteo;
                this.Alelo = Alelo;
                Porcentaje = (double)Conteo / totalCigotos * 100;
            }

            public ushort Alelo;
            public int Conteo;
            public double Porcentaje;
        }

        ConteoAlelo[][] _ConteoAlelosByLocus;
        public ConteoAlelo[][] ConteoAlelosByLocus
        {
            get
            {
                if (_ConteoAlelosByLocus == null)
                {
                    var totalCigotos = NumCigotosEsperados * NumCruces * 2;
                    var conteoAlelos = new Dictionary<ushort, int>[Restultado.NumLociTotal];
                    _ConteoAlelosByLocus = new ConteoAlelo[Restultado.NumLociTotal][];

                    for (int locusIndex = 0; locusIndex < Restultado.NumLociTotal; locusIndex++)
                    {
                        var conteo = new Dictionary<ushort, int>();

                        foreach (var pareja in Parejas)
                        {
                            var conteoByLocus = pareja.ConteoAlelosByLocus[locusIndex];
                            if (conteoByLocus == null)
                                continue;

                            foreach (var subConteo in conteoByLocus)
                            {
                                if (conteo.ContainsKey(subConteo.Key))
                                    conteo[subConteo.Key] += subConteo.Value;
                                else
                                    conteo[subConteo.Key] = subConteo.Value;
                            }
                        }

                        _ConteoAlelosByLocus[locusIndex] = conteo.OrderByDescending(kvp => kvp.Value)
                            .Select(kvp => new ConteoAlelo(kvp.Key, kvp.Value, totalCigotos)).ToArray();
                    }
                }

                return _ConteoAlelosByLocus;
            }
        }
    }
}
