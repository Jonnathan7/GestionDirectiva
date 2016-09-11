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

            return View(model);
        }
        #endregion

        #region Vistas parciales

        #endregion

        #region JsonResult

        #endregion
    }
}
