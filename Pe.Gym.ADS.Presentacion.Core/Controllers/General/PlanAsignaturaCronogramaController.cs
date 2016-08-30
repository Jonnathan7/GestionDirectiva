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
            return View(model);
        }
        #endregion

        #region Vistas parciales
        public ActionResult FormularioRegistro(int pId_Plan_Asignatura)
        {
            var model = new PlanAsignaturaCronogramaRegistroModel();
            return PartialView(model);
        }

        public ActionResult FormularioRegistroActividad(int pId_Plan_Asignatura, int pId_Actividad)
        {
            var model = new PlanAsignaturaCronogramaActividadRegistroModel();
            
            return PartialView(model);
        }
        #endregion

    }
}
