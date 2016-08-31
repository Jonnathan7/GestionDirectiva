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
    public class BL_PlanArea
    {
        public ProcessResult<List<usp_gd_PlanArea_Listar_Result>> ListarPlanArea(int periodoacademicoId, int gradoId, int areaId)
        {
            ProcessResult<List<usp_gd_PlanArea_Listar_Result>> resultado = new ProcessResult<List<usp_gd_PlanArea_Listar_Result>>();
            try
            {
                DA_PlanArea objeto = new DA_PlanArea();
                resultado.Result = objeto.ListarPlanArea(periodoacademicoId, gradoId, areaId);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanArea>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanArea> InsertarPlanArea(PlanArea planArea)
        {
            ProcessResult<PlanArea> resultado = new ProcessResult<PlanArea>();
            try
            {
                DA_PlanArea objDA = new DA_PlanArea();
                planArea.Estado = "REGISTRADO";
                planArea.FechaCreacion = DateTime.Now;
                objDA.InsertarPlanArea(planArea);

                resultado.IsProcess = true;
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanArea>(e);
            }
            return resultado;
        }
    }
}
