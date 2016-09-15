using GDirectiva.Core.Entities;
using GDirectiva.Presentacion.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Presentacion.Core.ViewModel.General
{
    public class PlanProyectoPedagogicoRegistroModel : GenericViewModel
    {
        public PlanProyectoPedagogicoRegistroModel()
        {
            this.planProyectoPedagogico = new PA_PLAN_PROYECTO_PEDAGOGICO_SEL_Result();
            this.planEstudio = new PA_PLAN_ESTUDIO_SEL_Result();
        }

        public PA_PLAN_PROYECTO_PEDAGOGICO_SEL_Result planProyectoPedagogico { get; set; }

        public PA_PLAN_ESTUDIO_SEL_Result planEstudio { get; set; }

        public List<PA_PERIODO_ACADEMICO_LISTA_VIGENTE_Result> ListaPeriodoAcademico = new List<PA_PERIODO_ACADEMICO_LISTA_VIGENTE_Result>();

        public List<PA_GRADO_LISTA_Result> ListaGrados = new List<PA_GRADO_LISTA_Result>();

        public List<PA_AREA_LISTA_Result> ListaAreas = new List<PA_AREA_LISTA_Result>();
    }
}
