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
    public class PlanProyectoPedagogicoController : GenericController
    {
        #region Vistas
        public ActionResult Index()
        {
            var model = new PlanProyectoPedagogicoBusquedaModel();
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

        #endregion

        #region JsonResult
        public JsonResult Buscar(int periodoacademicoId, int gradoId, int areaId)
        {
            var bl_PlanArea = new BL_PlanPlanProyectoPedagogico();
            var proceso = bl_PlanArea.ListarPlanProyectoPedagogico(periodoacademicoId, gradoId, areaId, 0, 10);
            return Json(proceso);
        }
        #endregion
    }
}
