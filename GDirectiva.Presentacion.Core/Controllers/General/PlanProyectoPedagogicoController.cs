using GDirectiva.Core.Entities;
using GDirectiva.Domain.Main;
using GDirectiva.Presentacion.Core.Controllers.Base;
using GDirectiva.Presentacion.Core.ViewModel.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
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
        public ActionResult FormularioRegistro(int pId_Plan_Proyecto_Pedagogico)
        {
            var model = new PlanProyectoPedagogicoRegistroModel();
            var bl_PlanProyectoPedagogico = new BL_PlanProyectoPedagogico();
            var bl_PlanEstudio = new BL_PlanEstudio();
            if (pId_Plan_Proyecto_Pedagogico > 0)
            {
                model.planProyectoPedagogico = bl_PlanProyectoPedagogico.ObtenerProyectoPedagogico(pId_Plan_Proyecto_Pedagogico).Result;
                model.planEstudio = new PA_PLAN_ESTUDIO_SEL_Result()
                {
                    ID_PLANESTUDIO = (int)model.planProyectoPedagogico.ID_PLANESTUDIO,
                    ANIO = model.planProyectoPedagogico.ANIO_PLANESTUDIO,
                    DOCUMENTO = model.planProyectoPedagogico.DOCUMENTO_PLANESTUDIO,
                    DESCRIPCION = model.planProyectoPedagogico.DESCRIPCION_PLANESTUDIO
                };
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
        public JsonResult Buscar(int periodoacademicoId, int gradoId, int areaId)
        {
            var bl_PlanArea = new BL_PlanProyectoPedagogico();
            var proceso = bl_PlanArea.ListarPlanProyectoPedagogico(periodoacademicoId, gradoId, areaId, 0, 10);
            return Json(proceso);
        }

        public JsonResult Registrar(PlanProyectoPedagogico planProyectoPedagogico, string contenidoArchivo)
        {
            ProcessResult<PlanProyectoPedagogico> resultado = new ProcessResult<PlanProyectoPedagogico>();

            var bl_PlanProyectoPedagogico = new BL_PlanProyectoPedagogico();

            if (!string.IsNullOrEmpty(contenidoArchivo))
            {
                var extencion = planProyectoPedagogico.Documento.Split('.').LastOrDefault();
                planProyectoPedagogico.Documento = WebConfigurationManager.AppSettings["DirectorioPlanProyectoPedagogico"] + Guid.NewGuid().ToString() + "." + extencion;
                contenidoArchivo = contenidoArchivo.Split(new string[] { "base64," }, StringSplitOptions.None).LastOrDefault();

            }

            if (planProyectoPedagogico.Id_ProyectoPedagogico == 0)
            {
                resultado = bl_PlanProyectoPedagogico.InsertarPlanProyectoPedagogico(planProyectoPedagogico);

                if (resultado.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(contenidoArchivo))
                    {
                        System.IO.File.WriteAllBytes(planProyectoPedagogico.Documento, Convert.FromBase64String(contenidoArchivo));
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(planProyectoPedagogico.Documento))
                {
                    var planAreaActual = bl_PlanProyectoPedagogico.ObtenerProyectoPedagogico(planProyectoPedagogico.Id_ProyectoPedagogico);
                    if (!string.IsNullOrEmpty(planAreaActual.Result.DOCUMENTO_PLANESTUDIO) && !string.IsNullOrEmpty(contenidoArchivo))
                    {
                        System.IO.File.Delete(planAreaActual.Result.DOCUMENTO_PLANESTUDIO);
                    }
                }
                resultado = bl_PlanProyectoPedagogico.ActualizarPlanProyectoPedagogico(planProyectoPedagogico);

                if (resultado.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(contenidoArchivo))
                    {
                        System.IO.File.WriteAllBytes(planProyectoPedagogico.Documento, Convert.FromBase64String(contenidoArchivo));
                    }
                }
            }

            return Json(resultado);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planArea"></param>
        /// <returns></returns>
        public JsonResult Eliminar(int pId_PlanProyectoPedagogico)
        {
            ProcessResult<PlanProyectoPedagogico> resultado = new ProcessResult<PlanProyectoPedagogico>();
            var bl_PlanProyectoPedagogico = new BL_PlanProyectoPedagogico();
            resultado = bl_PlanProyectoPedagogico.EliminarPlanProyectoPedagogico(pId_PlanProyectoPedagogico);
            return Json(resultado);
        }

        [HttpPost]
        public ActionResult SubirArchivo(PlanProyectoPedagogico planProyectoPedagogico)
        {
            var model = new ResultadoDocumentoImportarModel();

            try
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName.ToString().Replace(' ', '_'));
            }
            catch (Exception e)
            {

            }
            return PartialView(model);
        }
        #endregion
    }
}
