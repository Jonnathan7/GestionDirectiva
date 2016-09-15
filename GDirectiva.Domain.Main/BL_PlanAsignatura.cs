using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;
using GDirectiva.Cross.Core.Exception;
using GDirectiva.Core.Entities.Adapter;
using GDirectiva.Core.Entities.General;

namespace GDirectiva.Domain.Main
{
    public class BL_PlanAsignatura
    {
        public ProcessResult<List<PA_PLAN_ASIGNATURA_LISTA_Result>> ListarPlanAsignatura(int periodoacademicoId, int planAreaId, int asignaturaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            ProcessResult<List<PA_PLAN_ASIGNATURA_LISTA_Result>> resultado = new ProcessResult<List<PA_PLAN_ASIGNATURA_LISTA_Result>>();
            try
            {
                DA_PlanAsignatura objeto = new DA_PlanAsignatura();
                resultado.Result = objeto.ListarPlanAsignatura(periodoacademicoId, planAreaId, asignaturaId, pAGINA_INICIO, tAMANIO_PAGINA);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<List<PA_PLAN_ASIGNATURA_LISTA_VIGENTE_Result>> ListarPlanAsignaturaVigente(int periodoacademicoId, int planAreaId, int asignaturaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            ProcessResult<List<PA_PLAN_ASIGNATURA_LISTA_VIGENTE_Result>> resultado = new ProcessResult<List<PA_PLAN_ASIGNATURA_LISTA_VIGENTE_Result>>();
            try
            {
                DA_PlanAsignatura objeto = new DA_PlanAsignatura();
                resultado.Result = objeto.ListarPlanAsignaturaVigente(periodoacademicoId, planAreaId, asignaturaId, pAGINA_INICIO, tAMANIO_PAGINA);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<string> RegistrarPlanAsignaturaVigente(int id_planasignatura, List<PA_ACTIVIDAD_PLAN_ASIGNATURA_LISTA_Result> request)
        {
            ProcessResult<string> resultado = new ProcessResult<string>();
            try
            {

                DA_PlanAsignatura objeto = new DA_PlanAsignatura();
                objeto.EliminarPlanAsignaturaVigente(id_planasignatura);
                foreach (var item in request)
                {
                    resultado.Result = objeto.RegistrarPlanAsignaturaVigente(
                        item.ID_ACTIVIDADPLANASIGNATURA,
                        (int)item.ID_EMPLEADO,
                        item.ACTIVIDAD,
                        item.FECHAINICIO,
                        item.FECHAFIN,
                        item.PORCENTAJE,
                        (int)item.ID_PLANASIGNATURAMETA).ToString();
                }
                resultado.IsProcess = true;
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<PA_PLAN_ASIGNATURA_SEL_Result> ObtenerPlanAsignatura(int planAsignaturaId)
        {
            ProcessResult<PA_PLAN_ASIGNATURA_SEL_Result> resultado = new ProcessResult<PA_PLAN_ASIGNATURA_SEL_Result>();
            try
            {
                DA_PlanAsignatura objeto = new DA_PlanAsignatura();
                resultado.Result = objeto.ObtenerPlanAsignatura(planAsignaturaId);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanAsignatura> InsertarPlanAsignatura(PlanAsignatura planAsignatura)
        {
            ProcessResult<PlanAsignatura> resultado = new ProcessResult<PlanAsignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                planAsignatura.Estado = "REGISTRADO";
                planAsignatura.FechaCreacion = DateTime.Now;
                objDA.InsertarPlanAsignatura(planAsignatura);

                resultado.IsProcess = true;
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanAsignatura> ActualizarPlanAsignatura(PlanAsignatura planAsignatura)
        {
            ProcessResult<PlanAsignatura> resultado = new ProcessResult<PlanAsignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                planAsignatura.Estado = "REGISTRADO";
                planAsignatura.FechaModificacion = DateTime.Now;
                objDA.ActualizarPlanAsignatura(planAsignatura);

                resultado.IsProcess = true;
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanAsignatura> EliminarPlanAsignatura(int pId_PlanAsignatura)
        {
            ProcessResult<PlanAsignatura> resultado = new ProcessResult<PlanAsignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                PlanAsignatura planAsignatura = new PlanAsignatura()
                {
                    Id_PlanAsignatura = pId_PlanAsignatura,
                    Estado = "ELIMINADO",
                    FechaModificacion = DateTime.Now
                };
                objDA.EliminarPlanAsignatura(planAsignatura);

                resultado.IsProcess = true;
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<List<PA_PLAN_ASIGNATURA_META_LISTA_Result>> ListarPlanAsignaturaMeta(int pId_PlanAsignatura)
        {
            ProcessResult<List<PA_PLAN_ASIGNATURA_META_LISTA_Result>> resultado = new ProcessResult<List<PA_PLAN_ASIGNATURA_META_LISTA_Result>>();
            try
            {
                DA_PlanAsignatura objeto = new DA_PlanAsignatura();
                resultado.Result = objeto.ListarPlanAsignaturaMeta(pId_PlanAsignatura);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanAsignaturaMeta> InsertarPlanAsignaturaMeta(int pId_PlanAsignatura, List<string> meta)
        {
            ProcessResult<PlanAsignaturaMeta> resultado = new ProcessResult<PlanAsignaturaMeta>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                resultado.IsProcess = objDA.InsertarPlanAsignaturaMeta(pId_PlanAsignatura, meta);
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<PlanAsignaturaMeta> EliminarPlanAsignaturaMeta(int pId_PlanAsignatura)
        {
            ProcessResult<PlanAsignaturaMeta> resultado = new ProcessResult<PlanAsignaturaMeta>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();

                resultado.IsProcess = objDA.EliminarPlanAsignaturaMeta(pId_PlanAsignatura); ;
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        public ProcessResult<List<PA_CURSO_DOCENTE_Result>> CursoDocente(int idCurso)
        {
            ProcessResult <List<PA_CURSO_DOCENTE_Result>> resultado = new ProcessResult<List<PA_CURSO_DOCENTE_Result>>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();

                resultado.Result = objDA.ListarCursoDocente(idCurso);
                resultado.IsSuccess = true;
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }
    }
}
