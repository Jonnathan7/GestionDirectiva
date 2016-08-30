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
        /// <summary>
        /// Método que devuelve un plan de asignatura existente a través de un Id de Asignatura enviado como parámetro en el Plan de Asignatura 
        /// </summary>
        /// <param name="planAsignatura de tipo GD_Plan_Asignatura"></param>
        /// <returns></returns>
        public ProcessResult<EN_PlanAsignatura> obtener(int pId_Plan_Asignatura)
        {
            ProcessResult<EN_PlanAsignatura> resultado = new ProcessResult<EN_PlanAsignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                resultado.Result = PlanAsignaturaAdapter.ObtenerPlanAsignaturaEntity(objDA.obtener(pId_Plan_Asignatura));
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }
        /// <summary>
        /// Método que devuelve una lista de planes de Asignatura incluyendo el nombre del periodo y el nombre del docente encargado
        /// </summary>
        /// <param name="idPeriodo"></param>
        /// <param name="planAsignatura"></param>
        /// <returns></returns>
        public ProcessResult<List<GD_SP_PLAN_ASIGNATURA_LISTA_Result>> listar(int idPeriodo, GD_Plan_Asignatura planAsignatura, int pagina, int tamanio)
        {
            ProcessResult<List<GD_SP_PLAN_ASIGNATURA_LISTA_Result>> resultado = new ProcessResult<List<GD_SP_PLAN_ASIGNATURA_LISTA_Result>>();
            try
            {
                var objeto = new DA_PlanAsignatura();
                resultado.Result = objeto.listar(idPeriodo, planAsignatura, pagina, tamanio);
            }
            catch (Exception e)
            {
                resultado.Message = e.Message;
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura>(e);
            }
            return resultado;
        }

        /// <summary>
        /// Método que permite crear un nuevo Plan de Asignatura
        /// </summary>
        /// <param name="planAsignatura"></param>
        /// <returns></returns>
        public ProcessResult<GD_Plan_Asignatura> registrar(GD_Plan_Asignatura planAsignatura)
        {
            ProcessResult<GD_Plan_Asignatura> resultado = new ProcessResult<GD_Plan_Asignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                validarReglasInsertUpdate(planAsignatura);
                planAsignatura.Estado = "CREADO";
                objDA.registrar(planAsignatura);

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

        /// <summary>
        /// Método que Permite actualizar un plan de Asignatura Existente
        /// </summary>
        /// <param name="planAsignatura"></param>
        /// <returns></returns>
        public ProcessResult<GD_Plan_Asignatura> actualizar(GD_Plan_Asignatura planAsignatura)
        {
            ProcessResult<GD_Plan_Asignatura> resultado = new ProcessResult<GD_Plan_Asignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();

                validarReglasInsertUpdate(planAsignatura);

                //Reglas de negocio Adicionales para Actualización
                //GD-RN017
                GD_SP_PLAN_ASIGNATURA_SEL_Result obj = objDA.obtener(planAsignatura.Id_Plan_Asignatura);
                GD_Periodo_Academico objPer = objDA.obtenerPeriodo(planAsignatura.Id_Plan_Asignatura);

                DateTime FechaInicio = objPer.Fecha_Inicio.Value;
                DateTime FechaFin = objPer.Fecha_Fin.Value;
                if (DateTime.Now.Date > FechaFin.Date)
                {
                    resultado.Message = "No se puede modificar el Plan de Asignatura porque ya finalizó el Periodo";
                }
                // Si el Plan ya está en ejecución solo se debe poder modificar el nombre del docente
                if (DateTime.Now.Date > FechaInicio)
                {
                    if (obj.META != planAsignatura.Meta)
                    {
                        resultado.Message = "No se pueden modificar las metas porque el plan de Asignatura ya está en curso";
                    }
                    if (obj.METODOLOGIA != planAsignatura.Metodologia)
                    {
                        resultado.Message = "No se pueden modificar las metodologías porque el plan de Asignatura ya está en curso";
                    }
                    if (obj.DOCUMENTO != planAsignatura.Documento)
                    {
                        resultado.Message = "No se puede modificar el documento porque el plan de Asignatura ya está en curso";
                    }
                    if (obj.ID_PLAN_AREA != planAsignatura.GD_Plan_Area_Id_Plan_Area)
                    {
                        resultado.Message = "No se pueden modificar el plan de área porque el plan de Asignatura ya está en curso";
                    }
                    if (obj.ID_ASIGNATURA != planAsignatura.GD_Asignatura_Id_Asignatura)
                    {
                        resultado.Message = "No se pueden modificar la asignatura porque el plan de Asignatura ya está en curso";
                    }
                }
                //GD-RN056
                if (planAsignatura.Estado != "CREADO")
                {
                    resultado.Message = "Solo se puede modificar un Plan de Asignatura que se encuentre en estado Creado";
                }
                resultado.IsProcess = false;
                if (string.IsNullOrEmpty(resultado.Message))
                {
                    objDA.actualizar(planAsignatura);
                    resultado.IsProcess = true;
                }
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

        /// <summary>
        /// Método que permite eliminar un plan de Asignatura
        /// </summary>
        /// <param name="planAsignatura"></param>
        /// <returns></returns>
        public ProcessResult<GD_Plan_Asignatura> eliminar(GD_Plan_Asignatura planAsignatura)
        {
            ProcessResult<GD_Plan_Asignatura> resultado = new ProcessResult<GD_Plan_Asignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                GD_SP_PLAN_ASIGNATURA_SEL_Result obj = objDA.obtener(planAsignatura.Id_Plan_Asignatura);
                GD_Periodo_Academico objPer = objDA.obtenerPeriodo(planAsignatura.Id_Plan_Asignatura);

                //RN018
                DateTime FechaInicio = objPer.Fecha_Inicio.Value;
                DateTime FechaFin = objPer.Fecha_Fin.Value;
                if (DateTime.Now.Date > FechaInicio.Date)
                {
                    resultado.Message = "No se puede eliminar el Plan de Asignatura porque está en curso";
                }
                //RN057
                if (obj.ESTADO != "CREADO")
                {
                    resultado.Message = "En estos momentos no es posible eliminar el plan de Asignatura porque no se encuentra en estado CREADO";
                }
                resultado.IsProcess = false;
                if (string.IsNullOrEmpty(resultado.Message))
                {
                    objDA.eliminar(planAsignatura);
                    resultado.IsProcess = true;
                }
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
        /// <summary>
        /// Método que permite Eliminar las actividades del plan de Asignatura
        /// </summary>
        public ProcessResult<GD_Plan_Asignatura> eliminarActividades(GD_Plan_Asignatura planAsignatura)
        {
            ProcessResult<GD_Plan_Asignatura> resultado = new ProcessResult<GD_Plan_Asignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                objDA.eliminarCronograma(planAsignatura);
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
        /// <summary>
        /// Método que valida Reglas de Negocio comunes para los métodos de Inserción y Actualización
        /// </summary>
        /// <param name="planAsignatura"></param>
        private void validarReglasInsertUpdate(GD_Plan_Asignatura planAsignatura)
        {
            //Reglas de Negocio comunes para Inserción y Modificación
            //GD-RN012,GD-RN014
            if (planAsignatura.GD_Plan_Area_Id_Plan_Area == 0)
            {
                throw new Exception("Todo plan de Asignatura debe pertenecer a un plan de Area");
            }
            //GD-RN013
            if (planAsignatura.GD_Asignatura_Id_Asignatura == 0)
            {
                throw new Exception("Todo plan de Asignatura debe tener una Asignatura");
            }
            //GD-RN015
            if (planAsignatura.Meta == null)
            {
                throw new Exception("Todo plan de Asignatura debe al menos una meta");
            }
            //GD-RN016
            if (planAsignatura.Metodologia == null)
            {
                throw new Exception("Todo plan de Asignatura debe al menos una Metodología");
            }
            //GD-RN049
            if (planAsignatura.GRMS_Empleado_Id_Empleado == 0)
            {
                throw new Exception("Todo plan de Asignatura debe tener un docente Encargado");
            }
            //GD-RN054
            if (planAsignatura.Documento == null)
            {
                throw new Exception("Todo plan de Asignatura debe contener un Documento");
            }
        }
        public int prueba(int a1, int a2)
        {
            int a3 = a1 + a2;
            return a3;
        }

        /// <summary>
        /// Método que permite validar si un cronograma de Plan de Asignatura aún puede ser Editado
        /// </summary>
        public bool editarCronograma(GD_Plan_Asignatura planAsignatura)
        {
            DA_PlanAsignatura objDA = new DA_PlanAsignatura();
            GD_Periodo_Academico objPer = objDA.obtenerPeriodo(planAsignatura.Id_Plan_Asignatura);

            DateTime FechaInicioPer = objPer.Fecha_Inicio.Value;
            DateTime FechaFinPer = objPer.Fecha_Fin.Value;

            //GD-RN021
            if (DateTime.Now.Date >= FechaInicioPer.Date && DateTime.Now.Date <= FechaFinPer.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que permite Eliminar el cronograma de actividades del plan de Asignatura
        /// </summary>
        public ProcessResult<GD_Plan_Asignatura> eliminarCronograma(GD_Plan_Asignatura planAsignatura)
        {
            ProcessResult<GD_Plan_Asignatura> resultado = new ProcessResult<GD_Plan_Asignatura>();
            try
            {
                DA_PlanAsignatura objDA = new DA_PlanAsignatura();
                //GD-RN058
                if (editarCronograma(planAsignatura) == false)
                {
                    resultado.Message = "No se puede eliminar el Cronograma de Plan de Asignatura, porque la fecha actual está fuera del rango de inicio y fin del periodo académico";
                }
                //GD-RN023
                if (objDA.listaConPorcentajeAvance(planAsignatura).Count > 0)
                {
                    resultado.Message = "No se puede eliminar el Cronograma de Plan de Asignatura, porque ya tiene actividades con porcentaje de Avance";
                }
                resultado.IsProcess = false;
                if (string.IsNullOrEmpty(resultado.Message))
                {
                    objDA.eliminarCronograma(planAsignatura);
                    resultado.IsProcess = true;
                }
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
