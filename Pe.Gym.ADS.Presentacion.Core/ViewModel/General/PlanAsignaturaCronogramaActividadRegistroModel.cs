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
    public class PlanAsignaturaCronogramaActividadRegistroModel : GenericViewModel
    {
        public PlanAsignaturaCronogramaActividadRegistroModel()
        {
            this.actividad = new GD_Plan_Asignatura_Actividades();
        }
        public GD_Plan_Asignatura_Actividades actividad { get; set; }
        public List<GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA_Result> ListaResponsables { get; set; }
    }
}
