using GDirectiva.Core.Entities;
using GDirectiva.Core.Entities.General;
using GDirectiva.Presentacion.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Presentacion.Core.ViewModel.General
{
    public class PlanAsignaturaCronogramaRegistroModel : GenericViewModel
    {
        public PlanAsignaturaCronogramaRegistroModel()
        {
            this.planAsignatura = new PA_PLAN_ASIGNATURA_SEL_Result();
        }
        public PA_PLAN_ASIGNATURA_SEL_Result planAsignatura { get; set; }
    }
}
