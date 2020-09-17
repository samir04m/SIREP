using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Areas.Simulador.Library
{
    public class ParejasAgrupadas
    {
        public Individuo Agrupador;
        public Pareja[] Parejas;

        public ParejasAgrupadas(IEnumerable<Pareja> parejas, Individuo agrupador)
        {
            Parejas = parejas.OrderByDescending(p => p.Ho).ToArray();
            Agrupador = agrupador;
        }

        public int NumCruces
        {
            get { return Parejas.Length; }
        }

        public int NumCrucesAceptables
        {
            get { return Parejas.Where(p => p.Ho > 0.5).Count(); }
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
    }
}
