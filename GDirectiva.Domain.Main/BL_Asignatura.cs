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
    public class BL_Asignatura
    {
        public ProcessResult<List<PA_ASIGNATURA_PLANAREA_LISTA_Result>> ListarAsignaturaPlanArea(int planAreaId)
        {
            ProcessResult<List<PA_ASIGNATURA_PLANAREA_LISTA_Result>> resultado = new ProcessResult<List<PA_ASIGNATURA_PLANAREA_LISTA_Result>>();
            try
            {
                DA_PlanAsignatura objeto = new DA_PlanAsignatura();
                resultado.Result = objeto.ListarAsignaturaPlanArea(planAreaId);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }
    }
}
