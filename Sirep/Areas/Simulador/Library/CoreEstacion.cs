using Sirep.Data;
using Sirep.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sirep.Areas.Simulador.Library
{
    public class CoreEstacion
    {
        List<Individuo> m_listaM;
        List<Individuo> m_listaH;
        List<string> m_NombreLocuses;
        int m_numReproductores;
        int m_numLocuses;
        int m_numAlelos;
        public int[] Potencias;
        private int m_numGametos;
        Core m_core;

        EspecieReproductores _especieReproductor;
        private DatosSimulador _datosSimulador;

        public Core Core
        {
            get { return m_core; }
        }

        public CoreEstacion(IReceptorAvance receptor, Core core, DatosSimulador datosSimulador)
        {
            m_receptor = receptor;
            m_core = core;
            _datosSimulador = datosSimulador;
        }

        public string GetNombreLocus(int index)
        {
            return m_NombreLocuses[index];
        }

        public void CargarDatosReproductores(EspecieReproductores especieReproductor)
        {
            _especieReproductor = especieReproductor;

            m_listaH = new List<Individuo>();
            m_listaM = new List<Individuo>();

            m_NombreLocuses = new List<string>(); 
            foreach (var numeroLocus in especieReproductor.NumerosLocuses)
                m_NombreLocuses.Add("PL "+numeroLocus);

            m_numLocuses = m_NombreLocuses.Count();
            m_numAlelos = m_numLocuses * 2;
            m_numGametos = (int)Math.Pow(2, m_numLocuses);
            Potencias = Enumerable.Range(0, m_numLocuses).Select(num => (int)Math.Pow(2, num)).ToArray();

            foreach (var rep in especieReproductor.Reproductores)
            {
                if (rep.Estado)
                {
                    var individuo = new Individuo(this, m_numAlelos)
                    {
                        Codigo = rep.ChipId
                    };

                    var i = 0;
                    foreach (var locus in rep.Locus)
                    {
                        individuo.Alelos[i] = Convert.ToUInt16(locus.Value.ValorA);
                        i += 1;
                        individuo.Alelos[i] = Convert.ToUInt16(locus.Value.ValorB);
                        i += 1;
                    }

                    individuo.Preparar();
                    if (rep.Sexo.Equals("H"))
                        m_listaH.Add(individuo);
                    else
                        m_listaM.Add(individuo);

                }
            }
            m_numReproductores = m_listaH.Count + m_listaM.Count;

            listarAlelos();
        }

        internal AlelosByLocus[] Alelos;
        private void listarAlelos()
        {
            var individuos = getIndividuos();
            Alelos = Enumerable.Range(0, m_numLocuses).SelectMany(locus =>
               new[]{
                    new
                    {
                        Alelo=individuos.Select(i=>i.Alelos[locus*2]),
                        Locus=locus
                    },
                    new
                    {
                        Alelo=individuos.Select(i=>i.Alelos[locus*2+1]),
                        Locus=locus
                    }
                }
               ).GroupBy(a => a.Locus, a => a.Alelo, (locus, alelos) =>
                   new AlelosByLocus
                   {
                       Locus = locus,
                       Alelos = alelos.SelectMany(al => al).Distinct().ToArray()
                   }).ToArray();
        }

        internal void BuscarAlelosUnicosPara(IEnumerable<AlelosByLocus> bancoAlelos)
        {
            var alelosUnicos = Alelos.Select(als => new AlelosByLocus
            {
                Locus = als.Locus,
                Alelos = als.Alelos.Where(a1 =>
                    !bancoAlelos.Where(als2 => als2.Locus == als.Locus)
                    .SelectMany(al => al.Alelos).Contains(a1)).ToArray()
            }
                ).ToArray();

            foreach (var individuo in getIndividuos())
            {
                individuo.TengoAlelosUnicos(alelosUnicos);
            }
        }

        private IReceptorAvance m_receptor;
        private int m_tamañoRangoLoci; ///Numero de loci que contiene el rango de loci de analizados
        int _minimoLoci;

        Pareja[] parejasCalculadas;

        public void Notificar(double porcentaje)
        {
            if ((DateTime.Now - fechaInicial).TotalMilliseconds < 100)
                return;

            fechaInicial = DateTime.Now;
            m_receptor.SiguientePaso(porcentaje);
        }

        DateTime fechaInicial;
        public void CalcularYGuardar()
        {
            //m_receptor.EmpezadoCalculoEstacion(m_filename);
            fechaInicial = DateTime.Now;
            //Consulta para calcular los datos sobre la descendencia
            _minimoLoci = NumLoci / 2 + 1;
            m_tamañoRangoLoci = NumLoci - _minimoLoci + 1;
            double totalParejas = m_listaH.Count * m_listaM.Count;
            int conteoParejas = 0;

            var parejasPreCalculadas = m_listaM.AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .SelectMany(m => m_listaH, (m, h) =>
                {
                    var pareja = procesarPareja(m, h, _minimoLoci);
                    Interlocked.Add(ref conteoParejas, 1);
                    Notificar(conteoParejas / totalParejas);
                    return pareja;
                });

            parejasCalculadas = parejasPreCalculadas.Where(p => p != null).ToArray();

            //m_receptor.FinalizadoCalculoEstacion(m_filename);
            GenerarInforme();
        }

        

        public void GenerarInforme()
        {
            //m_receptor.EmpezadoInforme(m_filename);

            Informe informe = new Informe();
            informe.NumHembras = m_listaH.Count;
            informe.NumMachos = m_listaM.Count;
            informe.TiempoInicial = fechaInicial;
            informe.Model = new Resultado(this, parejasCalculadas, _minimoLoci, NumLoci, m_NombreLocuses);

            var carpeta = "wwwroot/Content/Informes/";
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            var nombreInforme = _datosSimulador.GuardarInforme(_especieReproductor, fechaInicial);               

            StreamWriter writer = new StreamWriter(carpeta + nombreInforme + ".html");
            var informeStr = informe.TransformText();
            writer.Write(informeStr);
            writer.Flush();
            writer.Close();

            //m_receptor.FinalizadoInforme(m_filename);
        }

        private IEnumerable<Individuo> getIndividuos()
        {
            foreach (var m in m_listaM)
                yield return m;

            foreach (var h in m_listaH)
                yield return h;
        }

        public int NumGametos
        {
            get { return m_numGametos; }
        }

        public int NumLoci
        {
            get
            {
                return m_numLocuses;
            }
        }

        private Pareja procesarPareja(Individuo macho, Individuo hembra, int minLociValidos)
        {
            //Cuenta el numero de loci que tienen en comun
            int numLociValidos = 0;
            for (int k = 0; k < m_numLocuses; k++)
            {
                if (!(macho.EsLocusVacio(k) || hembra.EsLocusVacio(k)))
                    numLociValidos++;
            }

            if (numLociValidos < minLociValidos)
                return null;

            Dictionary<ushort, ushort>[] ConteoAlelosByLocusIndex = new Dictionary<ushort, ushort>[m_numLocuses];
            Dictionary<ushort, ushort> conteoAlelos = new Dictionary<ushort, ushort>();
            double numeradorHe = 0;
            double numeradorHo = 0;
            double ho, he;

            for (int k = 0; k < m_numLocuses; k++)
            {
                if (macho.EsLocusVacio(k) || hembra.EsLocusVacio(k))
                    continue;

                int individuosHeteC = 0;
                int totalIndividuos = 0;
                ushort tamañoClase = 1;

                conteoAlelos = new Dictionary<ushort, ushort>();

                for (int i = 0; i < m_numGametos; i++)
                {
                    var aleloMacho = macho.GetAlelo(i, k);
                    var marcaClaseMacho = (ushort)(aleloMacho / tamañoClase);

                    if (macho.EsGametoInvalido(i) || hembra.EsGametoInvalido(i))
                        continue;

                    for (int j = 0; j < m_numGametos; j++)
                    {
                        //Se cuentan la cantidad de individuos y la cantidad de individuos heterocigotos

                        if (macho.EsGametoInvalido(j) || hembra.EsGametoInvalido(j))
                            continue;

                        var aleloHembra = hembra.GetAlelo(j, k);
                        var marcaClaseHembra = (ushort)(aleloHembra / tamañoClase);

                        totalIndividuos++;

                        if (!conteoAlelos.ContainsKey(marcaClaseMacho))
                            conteoAlelos[marcaClaseMacho] = 1;
                        else
                            conteoAlelos[marcaClaseMacho]++;

                        if (!conteoAlelos.ContainsKey(marcaClaseHembra))
                            conteoAlelos[marcaClaseHembra] = 1;
                        else
                            conteoAlelos[marcaClaseHembra]++;

                        if (marcaClaseMacho != marcaClaseHembra)
                            individuosHeteC++;
                    }
                }
                ho = (double)individuosHeteC / totalIndividuos;
                numeradorHo += ho;

                //Se calcula el valor de he para el locus actual
                double sumPi2 = 0;

                foreach (var freq in conteoAlelos.Values)
                {
                    sumPi2 += Math.Pow(freq / (totalIndividuos * 2.0), 2);
                }

                he = totalIndividuos * (1 - sumPi2) / (totalIndividuos - 1);
                numeradorHe += he;

                ConteoAlelosByLocusIndex[k] = conteoAlelos;
            }

            ho = numeradorHo / numLociValidos;
            he = numeradorHe / numLociValidos;

            var result = new Pareja(macho, hembra, ho, he, numLociValidos);
            result.ConteoAlelosByLocus = ConteoAlelosByLocusIndex;

            return result;
        }
    }
}
