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
        public ProcessResult<List<PA_PLAN_AREA_LISTA_Result>> ListarPlanArea(int periodoacademicoId, int gradoId, int areaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            ProcessResult<List<PA_PLAN_AREA_LISTA_Result>> resultado = new ProcessResult<List<PA_PLAN_AREA_LISTA_Result>>();
            try
            {
                DA_PlanArea objeto = new DA_PlanArea();
                resultado.Result = objeto.ListarPlanArea(periodoacademicoId, gradoId, areaId, pAGINA_INICIO, tAMANIO_PAGINA);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanArea>(e);
            }
            return resultado;
        }

        public ProcessResult<PA_PLAN_AREA_SEL_Result> ObtenerPlanArea(int pIdPlanArea)
        {
            ProcessResult<PA_PLAN_AREA_SEL_Result> resultado = new ProcessResult<PA_PLAN_AREA_SEL_Result>();
            try
            {
                DA_PlanArea objeto = new DA_PlanArea();
                resultado.Result = objeto.ObtenerPlanArea(pIdPlanArea);
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

        public ProcessResult<PlanArea> ActualizarPlanArea(PlanArea planArea)
        {
            ProcessResult<PlanArea> resultado = new ProcessResult<PlanArea>();
            try
            {
                DA_PlanArea objDA = new DA_PlanArea();
                planArea.Estado = "REGISTRADO";
                planArea.FechaModificacion = DateTime.Now;
                objDA.ActualizarPlanArea(planArea);

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

        public ProcessResult<PlanArea> EliminarPlanArea(int pId_PlanArea)
        {
            ProcessResult<PlanArea> resultado = new ProcessResult<PlanArea>();
            try
            {
                DA_PlanArea objDA = new DA_PlanArea();
                PlanArea planArea = new PlanArea()
                {
                    Id_PlanArea = pId_PlanArea,
                    Estado = "ELIMINADO",
                    FechaModificacion = DateTime.Now
                };
                objDA.EliminarPlanArea(planArea);

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
