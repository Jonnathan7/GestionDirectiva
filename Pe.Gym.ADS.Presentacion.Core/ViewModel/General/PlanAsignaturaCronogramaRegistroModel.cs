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
            this.planAsignatura = new EN_PlanAsignatura();
        }
        public EN_PlanAsignatura planAsignatura { get; set; }
    }
}
