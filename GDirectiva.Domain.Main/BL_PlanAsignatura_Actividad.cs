using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;
using GDirectiva.Cross.Core.Exception;

namespace GDirectiva.Domain.Main
{
    public class BL_PlanAsignatura_Actividad
    {
        public ProcessResult<List<PA_ACTIVIDAD_PLAN_ASIGNATURA_LISTA_Result>> ListarActividadPlanAsignatura(int planAsignaturaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            ProcessResult<List<PA_ACTIVIDAD_PLAN_ASIGNATURA_LISTA_Result>> resultado = new ProcessResult<List<PA_ACTIVIDAD_PLAN_ASIGNATURA_LISTA_Result>>();
            try
            {
                DA_PlanAsignatura_Actividad objeto = new DA_PlanAsignatura_Actividad();
                resultado.Result = objeto.ListarActividadPlanAsignatura(planAsignaturaId, pAGINA_INICIO, tAMANIO_PAGINA);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanArea>(e);
            }
            return resultado;
        }
    }
}
