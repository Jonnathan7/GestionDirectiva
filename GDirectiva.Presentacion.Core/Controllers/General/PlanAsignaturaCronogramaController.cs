using GDirectiva.Core.Entities;
using GDirectiva.Core.Entities.Adapter;
using GDirectiva.Domain.Main;
using GDirectiva.Presentacion.Core.Controllers.Base;
using GDirectiva.Presentacion.Core.ViewModel.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GDirectiva.Presentacion.Core.Controllers.General
{
    public class PlanAsignaturaCronogramaController : GenericController
    {
        #region Vistas
        /// <summary>
        /// Muestra la vista principal
        /// </summary>
        /// <returns>Vista principal de la opción</returns>
        public ActionResult Index()
        {
            var model = new PlanAsignaturaCronogramaBusquedaModel();
            var bl_PeriodoAcademico = new BL_PeriodoAcademico();
            model.ListaPeriodoAcademico = bl_PeriodoAcademico.ListarPeriodosAcademicos();
            return View(model);
        }
        #endregion

        #region Vistas parciales
        public ActionResult FormularioRegistro(int pId_Plan_Asignatura)
        {
            var model = new PlanAsignaturaCronogramaRegistroModel();
            var bl_PlanAsignatura = new BL_PlanAsignatura();
            if (pId_Plan_Asignatura > 0)
            {
                model.planAsignatura = bl_PlanAsignatura.ObtenerPlanAsignatura(pId_Plan_Asignatura).Result;
                var bl_PeriodoAcademico = new BL_PeriodoAcademico();
                var periodoAcademico = bl_PeriodoAcademico.ListarPeriodosAcademicos().Where(item => item.ID_PERIODOACADEMICO == model.planAsignatura.ID_PERIODOACADEMICO).FirstOrDefault();
                model.fechaInicioPeriodo = periodoAcademico.FECHA_INICIO.Value.ToString("dd/MM/yyyy");
                model.fechaFinPeriodo = periodoAcademico.FECHA_FIN.Value.ToString("dd/MM/yyyy");
            }
            return PartialView(model);
        }

        public ActionResult FormularioRegistroActividad(int pId_Plan_Asignatura, int pId_Actividad)
        {
            var model = new PlanAsignaturaCronogramaActividadRegistroModel();
            
            return PartialView(model);
        }
        #endregion


        #region JsonResult
        public JsonResult Buscar(int pId_Periodo, int pGD_Plan_Area_Id_Plan_Area, int pGD_Asignatura_Id_Asignatura)
        {
            var bl_PlanAsignatura = new BL_PlanAsignatura();
            var proceso = bl_PlanAsignatura.ListarPlanAsignaturaVigente(pId_Periodo, pGD_Plan_Area_Id_Plan_Area, pGD_Asignatura_Id_Asignatura, 0, 100);
            return Json(proceso);
        }

        public JsonResult BuscarActividades(int pId_Plan_Asignatura)
        {
            var bl_PlanAsignatura_Actividad = new BL_PlanAsignatura_Actividad();
            var proceso = bl_PlanAsignatura_Actividad.ListarActividadPlanAsignatura(pId_Plan_Asignatura, 0, 100);
            return Json(proceso);
        }

        public JsonResult BuscarPlanArea(int pId_Periodo)
        {
            ProcessResult<List<SelectListItem>> resultado = new ProcessResult<List<SelectListItem>>();
            resultado.Result = new List<SelectListItem>();
            var bl_PlanArea = new BL_PlanArea();
            var proceso = bl_PlanArea.ListarPlanAreaVigente(pId_Periodo);
            if (proceso != null)
            {
                proceso.Result.ForEach(delegate(PA_PLAN_AREA_LISTA_VIGENTE_Result plan)
                {
                    resultado.Result.Add(new SelectListItem() { Value = plan.ID_PLANAREA.ToString(), Text = plan.NOMBRE });
                });
            }
            else
            {
                resultado.Result = null;
            }
            return Json(resultado);
        }

        public JsonResult BuscarAsignatura(int pId_PlanArea)
        {
            ProcessResult<List<SelectListItem>> resultado = new ProcessResult<List<SelectListItem>>();
            resultado.Result = new List<SelectListItem>();
            var bl_Asignatura = new BL_Asignatura();
            var proceso = bl_Asignatura.ListarAsignaturaPlanArea(pId_PlanArea);
            if (proceso != null)
            {
                proceso.Result.ForEach(delegate(PA_ASIGNATURA_PLANAREA_LISTA_Result asignatura)
                {
                    resultado.Result.Add(new SelectListItem() { Value = asignatura.ID_ASIGNATURA.ToString(), Text = asignatura.NOMBRE_ASIGNATURA });
                });
            }
            else
            {
                resultado.Result = null;
            }
            return Json(resultado);
        }

        public JsonResult Registrar(int Id_Plan_Asignatura, List<PA_ACTIVIDAD_PLAN_ASIGNATURA_LISTA_Result> request)
        {
            var bl_PlanAsignatura = new BL_PlanAsignatura();
            var proceso = bl_PlanAsignatura.RegistrarPlanAsignaturaVigente(Id_Plan_Asignatura, request);
            return Json(proceso);
        }

        public JsonResult ListarEmpleado(int idCurso)
        {
            var bl_PlanAsignatura = new BL_PlanAsignatura();
            var resultado = bl_PlanAsignatura.CursoDocente(idCurso);
            return Json(resultado);
        }

        public JsonResult ListarMeta(int pId_PlanAsignatura)
        {
            var bl_PlanAsignatura = new BL_PlanAsignatura();
            var resultado = bl_PlanAsignatura.ListarPlanAsignaturaMeta(pId_PlanAsignatura);
            return Json(resultado);
        }
        #endregion
    }
}
