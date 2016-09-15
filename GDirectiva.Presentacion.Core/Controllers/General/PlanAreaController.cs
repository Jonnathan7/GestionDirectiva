﻿using GDirectiva.Core.Entities;
using GDirectiva.Domain.Main;
using GDirectiva.Presentacion.Core.Controllers.Base;
using GDirectiva.Presentacion.Core.ViewModel.Base;
using GDirectiva.Presentacion.Core.ViewModel.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
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
                model.planArea = bl_PlanArea.ObtenerPlanArea(pId_Plan_Area).Result;
                model.planEstudio = new PA_PLAN_ESTUDIO_SEL_Result()
                {
                    ID_PLANESTUDIO = (int)model.planArea.ID_PLANESTUDIO,
                    ANIO = model.planArea.ANIO_PLANESTUDIO,
                    DOCUMENTO = model.planArea.DOCUMENTO_PLANESTUDIO,
                    DESCRIPCION = model.planArea.DESCRIPCION_PLANESTUDIO
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
            var proceso = bl_PlanArea.ListarPlanArea(periodoacademicoId, gradoId, areaId,0, 10);
            return Json(proceso);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planArea"></param>
        /// <returns></returns>
        public JsonResult Registrar(PlanArea planArea, string contenidoArchivo)
        {
            ProcessResult<PlanArea> resultado = new ProcessResult<PlanArea>();

            var bl_PlanArea = new BL_PlanArea();

            if (!string.IsNullOrEmpty(contenidoArchivo))
            {
                var extencion = planArea.Documento.Split('.').LastOrDefault();
                planArea.Documento = WebConfigurationManager.AppSettings["DirectorioPlanArea"] + Guid.NewGuid().ToString() + "." + extencion;
                contenidoArchivo = contenidoArchivo.Split(new string[] { "base64," }, StringSplitOptions.None).LastOrDefault();

            }

            if (planArea.Id_PlanArea == 0)
            {
                resultado = bl_PlanArea.InsertarPlanArea(planArea);

                if (resultado.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(contenidoArchivo))
                    {
                        System.IO.File.WriteAllBytes(planArea.Documento, Convert.FromBase64String(contenidoArchivo));
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(planArea.Documento))
                {
                    var planAreaActual = bl_PlanArea.ObtenerPlanArea(planArea.Id_PlanArea);
                    if (!string.IsNullOrEmpty(planAreaActual.Result.DOCUMENTO_PLANAREA) && !string.IsNullOrEmpty(contenidoArchivo))
                    {
                        System.IO.File.Delete(planAreaActual.Result.DOCUMENTO_PLANAREA);
                    }
                }

                resultado = bl_PlanArea.ActualizarPlanArea(planArea);

                if (resultado.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(contenidoArchivo))
                    {
                        System.IO.File.WriteAllBytes(planArea.Documento, Convert.FromBase64String(contenidoArchivo));
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
        public JsonResult Eliminar(int pId_PlanArea)
        {
            ProcessResult<PlanArea> resultado = new ProcessResult<PlanArea>();
            var bl_PlanArea = new BL_PlanArea();
            resultado = bl_PlanArea.EliminarPlanArea(pId_PlanArea);
            return Json(resultado);
        }

        [HttpPost]
        public ActionResult SubirArchivo(PlanArea planArea)
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

        public void UploadDocument()
        {
            CargarArchivoViewModel ObjCargarArchivo = new CargarArchivoViewModel();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase item = Request.Files[i];

                ObjCargarArchivo.NombreArchivo = System.IO.Path.GetFileName(item.FileName);
                ObjCargarArchivo.Extension = System.IO.Path.GetExtension(item.FileName);
                ObjCargarArchivo.Tamaño = item.ContentLength;
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(Request.Files[i].InputStream))
                {
                    fileData = binaryReader.ReadBytes(Request.Files[i].ContentLength);
                }
                ObjCargarArchivo.ArchivoBase64 = Convert.ToBase64String(fileData);
            }
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(ObjCargarArchivo));
        }
    }
}
