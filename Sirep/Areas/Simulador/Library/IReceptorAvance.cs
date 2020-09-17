using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sirep.Areas.Simulador.Library
{
    public interface IReceptorAvance
    {
        void EmpezadoCalculoEstacion(string nombre);
        void SiguientePaso(double factoCompletitud);
        void FinalizadoCalculoEstacion(string nombre);
        void EmpezadoInforme(string nombre);
        void FinalizadoInforme(string nombre);
    }
}
