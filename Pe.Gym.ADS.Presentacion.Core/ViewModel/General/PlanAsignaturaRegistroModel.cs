using GDirectiva.Core.Entities;
using GDirectiva.Core.Entities.General;
using GDirectiva.Presentacion.Core.ViewModel.Base;
using System.Collections.Generic;

namespace GDirectiva.Presentacion.Core.ViewModel.General
{
    public class PlanAsignaturaRegistroModel : GenericViewModel
    {
        public PlanAsignaturaRegistroModel()
        {
            this.planAsignatura = new EN_PlanAsignatura();
        }

        public EN_PlanAsignatura planAsignatura { get; set; }
        public List<GD_SP_PERIODO_ACADEMICO_LISTA_Result> ListaPeriodoAcademico { get; set; }
    }
}
