using GDirectiva.Core.Entities;
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
    public class PlanAreaController : GenericController
    {
        #region Vistas
        /// <summary>
        /// Muestra la vista principal
        /// </summary>
        /// <returns>Vista principal de la opción</returns>
        public ActionResult Index()
        {
            var model = new PlanAreaBusquedaModel();
            var bl_PeriodoAcademico = new BL_PeriodoAcademico();
            var bl_Grado = new BL_Grado();
            var bl_Area = new BL_Area();

            model.ListaPeriodoAcademico = bl_PeriodoAcademico.ListarPeriodosAcademicos();
            model.ListaGrados = bl_Grado.ListarGrados();
            model.ListaAreas = bl_Area.ListarArea();

            return View(model);
        }
        #endregion

        #region Vistas parciales
        public ActionResult FormularioRegistro(int pId_Plan_Area)
        {
            var model = new PlanAreaRegistroModel();
            var bl_PlanArea = new BL_PlanArea();
            var bl_PlanEstudio = new BL_PlanEstudio();
            if (pId_Plan_Area > 0)
            {
                //model.planArea = bl_PlanArea.obtener(pId_Plan_Asignatura).Result;
                
            }
            else
            {
                model.planEstudio = bl_PlanEstudio.ObtenerPlanEstudioVigente();
            }
            var bl_PeriodoAcademico = new BL_PeriodoAcademico();
            var bl_Grado = new BL_Grado();
            var bl_Area = new BL_Area();

            model.ListaPeriodoAcademico = bl_PeriodoAcademico.ListarPeriodosAcademicosVigentes();
            model.ListaGrados = bl_Grado.ListarGrados();
            model.ListaAreas = bl_Area.ListarArea();

            return PartialView(model);
        }
        #endregion

        #region JsonResult
        /// <summary>
        /// 
        /// </summary>
        /// <param name="periodoacademicoId"></param>
        /// <param name="gradoId"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public JsonResult Buscar(int periodoacademicoId, int gradoId, int areaId)
        {
            var bl_PlanArea = new BL_PlanArea();
            var proceso = bl_PlanArea.ListarPlanArea(periodoacademicoId, gradoId, areaId);
            return Json(proceso);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planArea"></param>
        /// <returns></returns>
        public JsonResult Registrar(PlanArea planArea)
        {
            ProcessResult<PlanArea> resultado = new ProcessResult<PlanArea>();
            var bl_PlanArea = new BL_PlanArea();
            if (planArea.Id_PlanArea == 0)
            {
                resultado = bl_PlanArea.InsertarPlanArea(planArea);
            }
            else
            {
                planArea.FechaModificacion = DateTime.Now;
                //resultado = bl_PlanArea.actualizar(planAsignatura);
            }
            return Json(resultado);
        }
        #endregion
    }
}
