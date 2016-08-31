using GDirectiva.Core.Entities;
using GDirectiva.Presentacion.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Presentacion.Core.ViewModel.General
{
    public class PlanAreaBusquedaModel : GenericViewModel
    {
        public PlanAreaBusquedaModel()
        {

        }
        public List<PA_PERIODO_ACADEMICO_LISTA_Result> ListaPeriodoAcademico = new List<PA_PERIODO_ACADEMICO_LISTA_Result>();

        public List<PA_GRADO_LISTA_Result> ListaGrados = new List<PA_GRADO_LISTA_Result>();

        public List<PA_AREA_LISTA_Result> ListaAreas = new List<PA_AREA_LISTA_Result>();
    }
}
