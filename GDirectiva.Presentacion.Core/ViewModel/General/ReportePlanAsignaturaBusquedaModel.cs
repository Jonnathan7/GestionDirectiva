using GDirectiva.Core.Entities;
using GDirectiva.Presentacion.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Presentacion.Core.ViewModel.General
{
    public class ReportePlanAsignaturaBusquedaModel : GenericViewModel
    {
        public ReportePlanAsignaturaBusquedaModel()
        {

        }
        public List<PA_PERIODO_ACADEMICO_LISTA_Result> ListaPeriodoAcademico = new List<PA_PERIODO_ACADEMICO_LISTA_Result>();
    }
}
