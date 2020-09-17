using Sirep.Data;
using Sirep.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Areas.Simulador.Library
{
    public class Core
    {
        List<CoreEstacion> estaciones;

        IReceptorAvance _receptor;
        public Core(IReceptorAvance receptor)
        {
            _receptor = receptor;
            estaciones = new List<CoreEstacion>();
        }

        public void AddEstacion(EspecieReproductores especieReproductor, DatosSimulador datosSimulador)
        {
            var estacion = new CoreEstacion(_receptor, this, datosSimulador);
            estacion.CargarDatosReproductores(especieReproductor);
            estaciones.Add(estacion);
        }

        public int NumEstaciones
        {
            get { return estaciones.Count; }
        }

        public void Procesar()
        {
            foreach (var estacion in estaciones)
            {
                if (NumEstaciones > 1)
                    estacion.BuscarAlelosUnicosPara(estaciones.Where(e => e != estacion).SelectMany(e => e.Alelos));
                estacion.CalcularYGuardar();
            }
        }
    }
}
