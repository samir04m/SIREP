using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Areas.Simulador.Library
{
    public class Individuo
    {
        private ushort[] m_Alelos;

        public ushort[] Alelos
        {
            get { return m_Alelos; }
        }
        private string m_Codigo;

        public string Codigo
        {
            get { return m_Codigo; }
            set { m_Codigo = value; }
        }

        public Individuo(CoreEstacion core, int alelos)
        {
            m_Alelos = new ushort[alelos];
            m_core = core;
        }

        public ushort GetAlelo(int indexGameto, int indexLocus)
        {
            return m_Alelos[indexLocus * 2
                + ((m_core.Potencias[indexLocus] & indexGameto) == 0 ? 0 : 1)];
        }

        CoreEstacion m_core;

        bool[] invalidezGametos;

        internal bool EsLocusVacio(int indexLocus)
        {
            return m_Alelos[indexLocus * 2] == 0;
        }

        public double Ho
        {
            get
            {
                return (double)Enumerable.Range(0, m_core.NumLoci).Sum(i => m_Alelos[i * 2] != m_Alelos[i * 2 + 1] ? 1 : 0)
                / m_core.NumLoci;
            }
        }

        private bool _EsGametoInvalido(int indexGameto)
        {
            for (int i = 0; i < m_core.NumLoci; i++)
            {
                if (EsLocusVacio(i) && (m_core.Potencias[i] & indexGameto) != 0)
                    return true;
            }

            return false;
        }

        public bool EsGametoInvalido(int indexGameto)
        {
            return invalidezGametos[indexGameto];
        }

        internal void Preparar()
        {
            invalidezGametos = new bool[m_core.NumGametos];
            for (int i = 0; i < m_core.NumGametos; i++)
                invalidezGametos[i] = _EsGametoInvalido(i);
        }

        int? numAlelosRaros;
        public int NumAlelosRaros
        {
            get
            {
                if (numAlelosRaros == null)
                    numAlelosRaros = m_AlelosRaros.SelectMany(a => a.Alelos).Count();
                return numAlelosRaros.Value;
            }
        }

        AlelosByLocus[] m_AlelosRaros;
        internal void TengoAlelosUnicos(IEnumerable<AlelosByLocus> alelosUnicos)
        {
            m_AlelosRaros = alelosUnicos.Select(ar => new AlelosByLocus()
            {
                Locus = ar.Locus,
                Alelos = ar.Alelos
                .Where(al => al == Alelos[ar.Locus * 2] || al == Alelos[ar.Locus * 2 + 1])
                .ToArray()
            }).ToArray();
        }
    }
}
