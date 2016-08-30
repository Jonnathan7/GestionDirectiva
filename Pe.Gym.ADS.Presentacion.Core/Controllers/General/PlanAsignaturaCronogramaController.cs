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

            model.ListaPeriodoAcademico = bl_PeriodoAcademico.listar();

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
                model.planAsignatura = bl_PlanAsignatura.obtener(pId_Plan_Asignatura).Result;
            }
            return PartialView(model);
        }

        public ActionResult FormularioRegistroActividad(int pId_Plan_Asignatura, int pId_Actividad)
        {
            var model = new PlanAsignaturaCronogramaActividadRegistroModel();
            var bl_PlanAsignatura_Actividad = new BL_PlanAsignatura_Actividad();
            var bl_Empleado = new BL_Empleado();
            if (pId_Actividad > 0)
            {
                model.actividad = bl_PlanAsignatura_Actividad.obtener(new GD_Plan_Asignatura_Actividades() { Id_Actividad = pId_Actividad }).Result;
            }
            model.actividad.GD_Plan_Asignatura_Id_Plan_Asignatura = pId_Plan_Asignatura;
            model.ListaResponsables = bl_Empleado.listarResponsables(pId_Plan_Asignatura);
            return PartialView(model);
        }
        #endregion

        public JsonResult Buscar(int pId_Periodo, int pGD_Plan_Area_Id_Plan_Area, int pGD_Asignatura_Id_Asignatura, int pGRMS_Empleado_Id_Empleado)
        {
            var bl_PlanAsignatura = new BL_PlanAsignatura();
            var proceso = bl_PlanAsignatura.listar(pId_Periodo, new GD_Plan_Asignatura()
            {
                GD_Plan_Area_Id_Plan_Area = pGD_Plan_Area_Id_Plan_Area,
                GD_Asignatura_Id_Asignatura = pGD_Asignatura_Id_Asignatura,
                GRMS_Empleado_Id_Empleado = pGRMS_Empleado_Id_Empleado
            }, 0, 100);
            return Json(proceso);
        }

        public JsonResult BuscarActividades(int pId_Plan_Asignatura)
        {
            var bl_PlanAsignatura_Actividad = new BL_PlanAsignatura_Actividad();
            var proceso = bl_PlanAsignatura_Actividad.listar(new GD_Plan_Asignatura()
            {
                Id_Plan_Asignatura = pId_Plan_Asignatura,
            }, 0, 100);
            return Json(proceso);
        }

        public JsonResult BuscarPlanArea(int pId_Periodo)
        {
            ProcessResult<List<SelectListItem>> resultado = new ProcessResult<List<SelectListItem>>();
            resultado.Result = new List<SelectListItem>();
            var bl_PlanArea = new BL_PlanArea();
            List<GD_Plan_Area> proceso = bl_PlanArea.listar(new GD_Plan_Area() { GD_Periodo_Academico_Id_Periodo = pId_Periodo });
            if (proceso != null)
            {
                proceso.ForEach(delegate(GD_Plan_Area plan)
                {
                    resultado.Result.Add(new SelectListItem() { Value = plan.Id_Plan_Area.ToString(), Text = plan.Nombre });
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
            BL_PlanArea bl_PlanArea = new BL_PlanArea();
            var planArea = bl_PlanArea.obtener(new GD_Plan_Area() { Id_Plan_Area = pId_PlanArea });

            ProcessResult<List<SelectListItem>> resultado = new ProcessResult<List<SelectListItem>>();
            resultado.Result = new List<SelectListItem>();
            var bl_Asignatura = new BL_Asignatura();
            List<GD_Asignatura> proceso = bl_Asignatura.listar(new GD_Asignatura() { GD_Area_Id_Area = planArea.GD_Area_Id_Area });
            if (proceso != null)
            {
                proceso.ForEach(delegate(GD_Asignatura asignatura)
                {
                    resultado.Result.Add(new SelectListItem() { Value = asignatura.Id_Asignatura.ToString(), Text = asignatura.Curso });
                });
            }
            else
            {
                resultado.Result = null;
            }
            return Json(resultado);
        }

        public JsonResult BuscarDocente(int pId_Asignatura)
        {
            ProcessResult<List<SelectListItem>> resultado = new ProcessResult<List<SelectListItem>>();
            resultado.Result = new List<SelectListItem>();
            var bl_Empleado = new BL_Empleado();
            List<GRMS_Empleado> proceso = bl_Empleado.listar(new GD_Asignatura() { Id_Asignatura = pId_Asignatura });
            if (proceso != null)
            {
                proceso.ForEach(delegate(GRMS_Empleado empleado)
                {
                    resultado.Result.Add(new SelectListItem() { Value = empleado.Id_Empleado.ToString(), Text = empleado.Ape_Paterno + " " + empleado.Ape_Materno + ", " + empleado.Nombres });
                });
            }
            else
            {
                resultado.Result = null;
            }
            return Json(resultado);
        }

        /*public JsonResult Registrar(GD_Plan_Asignatura planAsignatura)
        {
            ProcessResult<GD_Plan_Asignatura> resultado = new ProcessResult<GD_Plan_Asignatura>();
            var bl_PlanAsignatura = new BL_PlanAsignatura();
            if (planAsignatura.Id_Plan_Asignatura == 0)
            {
                planAsignatura.Documento = "";
                //resultado = bl_PlanAsignatura.registrar(planAsignatura);
            }
            else
            {
                planAsignatura.Documento = "";
                resultado = bl_PlanAsignatura.actualizar(planAsignatura);
            }
            return Json(resultado);
        }*/

        public JsonResult Eliminar(int pId_Plan_Asignatura)
        {
            ProcessResult<GD_Plan_Asignatura> resultado = new ProcessResult<GD_Plan_Asignatura>();
            var bl_PlanAsignatura = new BL_PlanAsignatura();
            resultado = bl_PlanAsignatura.eliminarActividades(new GD_Plan_Asignatura() { Id_Plan_Asignatura = pId_Plan_Asignatura });
            return Json(resultado);
        }

        public JsonResult RegistrarActividad(GD_Plan_Asignatura_Actividades pActividad)
        {
            ProcessResult<GD_Plan_Asignatura_Actividades> resultado = new ProcessResult<GD_Plan_Asignatura_Actividades>();
            var bl_PlanAsignatura_Actividad = new BL_PlanAsignatura_Actividad();
            if (pActividad.Id_Actividad == 0)
            {
                resultado = bl_PlanAsignatura_Actividad.registrar(pActividad);
            }
            else
            {
                resultado = bl_PlanAsignatura_Actividad.actualizar(pActividad);
            }
            return Json(resultado);
        }
        public JsonResult EliminarActividad(int pId_Actividad, int pIdPlan_Asignatura)
        {
            ProcessResult<GD_Plan_Asignatura_Actividades> resultado = new ProcessResult<GD_Plan_Asignatura_Actividades>();
            var bl_PlanAsignatura = new BL_PlanAsignatura_Actividad();
            resultado = bl_PlanAsignatura.eliminar(new GD_Plan_Asignatura_Actividades() { Id_Actividad = pId_Actividad, GD_Plan_Asignatura_Id_Plan_Asignatura = pIdPlan_Asignatura });
            return Json(resultado);
        }
    }
}
