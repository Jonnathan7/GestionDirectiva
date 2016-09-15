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
    public class BL_PlanProyectoPedagogico
    {
        public ProcessResult<List<PA_PLAN_PROYECTO_PEDAGOGICO_LISTA_Result>> ListarPlanProyectoPedagogico(int periodoacademicoId, int gradoId, int areaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            ProcessResult<List<PA_PLAN_PROYECTO_PEDAGOGICO_LISTA_Result>> resultado = new ProcessResult<List<PA_PLAN_PROYECTO_PEDAGOGICO_LISTA_Result>>();
            try
            {
                DA_PlanProyectoPedagogico objeto = new DA_PlanProyectoPedagogico();
                resultado.Result = objeto.ListarPlanProyectoPedagogico(periodoacademicoId, gradoId, areaId, pAGINA_INICIO, tAMANIO_PAGINA);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanProyectoPedagogico>(e);
            }
            return resultado;
        }

        public ProcessResult<PA_PLAN_PROYECTO_PEDAGOGICO_SEL_Result> ObtenerProyectoPedagogico(int pIdPlanProyectoPedagogico)
        {
            ProcessResult<PA_PLAN_PROYECTO_PEDAGOGICO_SEL_Result> resultado = new ProcessResult<PA_PLAN_PROYECTO_PEDAGOGICO_SEL_Result>();
            try
            {
                DA_PlanProyectoPedagogico objeto = new DA_PlanProyectoPedagogico();
                resultado.Result = objeto.ObtenerPlanProyectoPedagogico(pIdPlanProyectoPedagogico);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanProyectoPedagogico>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanProyectoPedagogico> InsertarPlanProyectoPedagogico(PlanProyectoPedagogico planProyectoPedagogico)
        {
            ProcessResult<PlanProyectoPedagogico> resultado = new ProcessResult<PlanProyectoPedagogico>();
            try
            {
                DA_PlanProyectoPedagogico objDA = new DA_PlanProyectoPedagogico();
                if (objDA.ObtenerPlanProyectoPedagogicoExiste((int)planProyectoPedagogico.Id_PeriodoAcademico, (int)planProyectoPedagogico.Id_Area, (int)planProyectoPedagogico.Id_PlanEstudio, (int)planProyectoPedagogico.Id_Grado) == 0)
                {
                    planProyectoPedagogico.Estado = "REGISTRADO";
                    planProyectoPedagogico.FechaCreacion = DateTime.Now;
                    resultado.Result = objDA.InsertarPlanProyectoPedagogico(planProyectoPedagogico);
                    resultado.IsProcess = true;
                }
                else
                {
                    resultado.Message = "El Plan de Proyecto Pedagógico existe para el siguiente: Periodo Académico, Área Curricular o Grado";
                    resultado.IsProcess = false;
                }
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanProyectoPedagogico>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanProyectoPedagogico> ActualizarPlanProyectoPedagogico(PlanProyectoPedagogico planProyectoPedagogico)
        {
            ProcessResult<PlanProyectoPedagogico> resultado = new ProcessResult<PlanProyectoPedagogico>();
            try
            {
                DA_PlanProyectoPedagogico objDA = new DA_PlanProyectoPedagogico();
                planProyectoPedagogico.Estado = "REGISTRADO";
                planProyectoPedagogico.FechaModificacion = DateTime.Now;
                objDA.ActualizarPlanProyectoPedagogico(planProyectoPedagogico);
                resultado.Result = planProyectoPedagogico;

                resultado.IsProcess = true;
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanProyectoPedagogico>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanProyectoPedagogico> EliminarPlanProyectoPedagogico(int pId_PlanProyectoPedagogico)
        {
            ProcessResult<PlanProyectoPedagogico> resultado = new ProcessResult<PlanProyectoPedagogico>();
            try
            {
                DA_PlanProyectoPedagogico objDA = new DA_PlanProyectoPedagogico();
                PlanProyectoPedagogico planArea = new PlanProyectoPedagogico()
                {
                    Id_ProyectoPedagogico = pId_PlanProyectoPedagogico,
                    Estado = "ELIMINADO",
                    FechaModificacion = DateTime.Now
                };
                objDA.EliminarPlanProyectoPedagogico(planArea);

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
