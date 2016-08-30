using GDirectiva.Core.Entities;
using GDirectiva.Presentacion.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GDirectiva.Presentacion.Core.ViewModel.General
{
    public class PlanAsignaturaBusquedaModel : GenericViewModel
    {
        public PlanAsignaturaBusquedaModel()
        {

        }

        public List<GD_Periodo_Academico> ListaPeriodoAcademico { get; set; }
    }
}
