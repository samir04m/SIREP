using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Areas.Simulador.Library
{
    public class Pareja
    {
        public Individuo Macho;
        public Individuo Hembra;
        public int NumLoci; //Utilizado para agrupar
        public Dictionary<ushort, ushort>[] ConteoAlelosByLocus;

        double m_H0;
        public double Ho
        {
            get { return m_H0; }
        }
        double m_He;

        public double He
        {
            get { return m_He; }
        }
        double m_Fis;

        public double Fis
        {
            get { return m_Fis; }
        }

        public Pareja(Individuo macho, Individuo hembra, double h0, double he, int numLocusValidos)
        {
            Macho = macho;
            Hembra = hembra;
            m_H0 = h0;
            m_He = he;
            m_Fis = 1 - (h0 / he);
            NumLoci = numLocusValidos;
        }
    }
}
