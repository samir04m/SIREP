using Sirep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Library
{
    public class ReproductorSimulador
    {
        public int Id;
        public string ChipId;
        public string Sexo;
        public Dictionary<int, ValoresLocus> Locus;
        public bool Estado;
    }

    public class ValoresLocus
    {
        public int ValorA;
        public int ValorB;
    }

    public class EspecieReproductores
    {
        public Centro Centro;
        public Especie Especie;
        public List<int> NumerosLocuses;
        public List<ReproductorSimulador> Reproductores;
        public int NumeroReproductoresValidos;
    }
}
