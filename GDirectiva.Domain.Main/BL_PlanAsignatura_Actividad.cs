using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;
using GDirectiva.Cross.Core.Exception;

namespace GDirectiva.Domain.Main
{
    public class BL_PlanAsignatura_Actividad
    {

        /// <summary>
        /// Método que devuelve una lista de Actividades de planes de asignatura de acuerdo al código del plan de asignatura ingresado
        /// </summary>
        public ProcessResult<List<GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA_Result>> listar(GD_Plan_Asignatura planAsignatura, int pagina, int tamanio)
        {
            ProcessResult<List<GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA_Result>> resultado = new ProcessResult<List<GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA_Result>>();
            try
            {
                var objeto = new DA_PlanAsignatura_Actividad();
                resultado.Result = objeto.listar(planAsignatura, pagina, tamanio);
            }
            catch (Exception e)
            {
                resultado.Message = e.Message;
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura_Actividad>(e);
            }
            return resultado;
        }

        public ProcessResult<GD_Plan_Asignatura_Actividades> obtener(GD_Plan_Asignatura_Actividades actividad)
        {
            ProcessResult<GD_Plan_Asignatura_Actividades> resultado = new ProcessResult<GD_Plan_Asignatura_Actividades>();
            try
            {
                var objeto = new DA_PlanAsignatura_Actividad();
                resultado.Result = objeto.obtener(actividad);
            }
            catch (Exception e)
            {
                resultado.Message = e.Message;
                resultado.IsSuccess = false;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura_Actividad>(e);
            }
            return resultado;
        }
        
        /// <summary>
        /// Método que permite registrar una actividad del Plan de Asignatura
        /// </summary>
        public ProcessResult<GD_Plan_Asignatura_Actividades> registrar(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            ProcessResult<GD_Plan_Asignatura_Actividades> resultado = new ProcessResult<GD_Plan_Asignatura_Actividades>();
            try
            {
                DA_PlanAsignatura_Actividad objDA = new DA_PlanAsignatura_Actividad();

                //GD-RN019,GD-RN024
                resultado.Message = validarReglasInsertUpdate(planAsignaturaActiv);
                //GD-RN020
                resultado.Message = validarFechaDentroDePeriodo(planAsignaturaActiv);
                resultado.IsProcess = false;
                if (string.IsNullOrEmpty(resultado.Message))
                {
                    planAsignaturaActiv.Estado = "CREADO";
                    objDA.registrar(planAsignaturaActiv);
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
        /// Método que permite actualizar una actividad del Plan de Asignatura
        /// </summary>
        public ProcessResult<GD_Plan_Asignatura_Actividades> actualizar(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            ProcessResult<GD_Plan_Asignatura_Actividades> resultado = new ProcessResult<GD_Plan_Asignatura_Actividades>();
            try
            {
                DA_PlanAsignatura_Actividad objDA = new DA_PlanAsignatura_Actividad();
                //GD-RN019
                resultado.Message = validarReglasInsertUpdate(planAsignaturaActiv);
                //GD-RN020
                resultado.Message = validarFechaDentroDePeriodo(planAsignaturaActiv);
                //GD-RN022
                if (validarFechaActualMenorInicioPeriodo(planAsignaturaActiv) == false)
                {
                    //valida si el % de complimiento es 0
                    GD_Plan_Asignatura_Actividades objActiv = objDA.obtener(planAsignaturaActiv);

                    if (objActiv.Porcentaje_Avance > 0)
                    {
                        resultado.Message = "No se puede modificar la actividad porque esta actividad ya se inició y ya tiene un porocentaje de avance";
                    }
                }
                resultado.IsProcess = false;
                if (string.IsNullOrEmpty(resultado.Message))
                {
                    objDA.actualizar(planAsignaturaActiv);
                    resultado.IsProcess = true;
                }
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura_Actividad>(e);
            }
            return resultado;
        }
        
        /// <summary>
        /// Método que permite actualizar el porcentaje de avance de un Plan de Asignatura
        /// </summary>
        public bool actualizarPorcentajeAvance(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            DA_PlanAsignatura_Actividad objDA = new DA_PlanAsignatura_Actividad();
            //GD-RN020
            validarFechaDentroDePeriodo(planAsignaturaActiv);

            return objDA.actualizarPorcentajeAvance(planAsignaturaActiv);
        }
        
        /// <summary>
        /// Método que permite eliminar una actividad del plan de Asignatura
        /// </summary>
        public ProcessResult<GD_Plan_Asignatura_Actividades> eliminar(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            ProcessResult<GD_Plan_Asignatura_Actividades> resultado = new ProcessResult<GD_Plan_Asignatura_Actividades>();
            try
            {
                DA_PlanAsignatura_Actividad objDA = new DA_PlanAsignatura_Actividad();

                //GD-RN023
                if (validarFechaActualMenorInicioPeriodo(planAsignaturaActiv) == false)
                {
                    //valida si el % de complimiento es 0
                    GD_Plan_Asignatura_Actividades objActiv = objDA.obtener(planAsignaturaActiv);

                    if (objActiv.Porcentaje_Avance > 0)
                    {
                        resultado.Message = "No se puede modificar la actividad porque esta actividad ya se inició y ya tiene un porocentaje de avance";
                    }
                }
                resultado.IsProcess = false;
                if (string.IsNullOrEmpty(resultado.Message))
                {
                    objDA.eliminar(planAsignaturaActiv);
                    resultado.IsProcess = true;
                }
            }
            catch (Exception e)
            {
                resultado.IsSuccess = false;
                resultado.IsProcess = false;
                resultado.Message = e.Message;
                resultado.Exception = new ApplicationLayerException<BL_PlanAsignatura_Actividad>(e);
            }
            return resultado;
        }
        
        /// <summary>
        /// Método que permite validar reglas de existencia de datos en los métodos de inserción y actualización
        /// </summary>
        private string validarReglasInsertUpdate(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            var mensaje = string.Empty;
            if (planAsignaturaActiv.Actividad == null)
            {
                mensaje = "El nombre de la actividad está vacía";
            }
            if (planAsignaturaActiv.Fecha_Inicio == null)
            {
                mensaje = "La fecha de inicio de la actividad porque está vacía";
            }
            if (planAsignaturaActiv.Fecha_Fin == null)
            {
                mensaje = "La fecha de finalización de la actividad está vacía";
            }
            if (planAsignaturaActiv.GRMS_Empleado_Id_Empleado == 0)
            {
                mensaje = "Toda actividad debe tener asignado un empleado";
            }
            if (planAsignaturaActiv.GD_Plan_Asignatura_Id_Plan_Asignatura == 0)
            {
                mensaje = "Toda actividad debe estar asignado a un Plan de Asignatura";
            }
            return mensaje;
        }
        
        /// <summary>
        /// Método que permite validad si las fecha de incio y fin de la actividad se encuentran dentro del rango de fechas de inicio y fin del periodo académico
        /// </summary>
        private string validarFechaDentroDePeriodo(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            var mensaje = string.Empty;
            DA_PlanAsignatura objPasDA = new DA_PlanAsignatura();
            GD_Plan_Asignatura objPas = new GD_Plan_Asignatura();
            
            objPas.Id_Plan_Asignatura = planAsignaturaActiv.GD_Plan_Asignatura_Id_Plan_Asignatura;
            GD_SP_PLAN_ASIGNATURA_SEL_Result objPasEsp = objPasDA.obtener(objPas.Id_Plan_Asignatura);
            
            GD_Periodo_Academico objPer = objPasDA.obtenerPeriodo(objPasEsp.ID_PLAN_ASIGNATURA);

            DateTime FechaInicioPer = objPer.Fecha_Inicio.Value;
            DateTime FechaFinPer = objPer.Fecha_Fin.Value;
            
            if (planAsignaturaActiv.Fecha_Inicio < FechaInicioPer.Date || planAsignaturaActiv.Fecha_Inicio>FechaFinPer)
            {
                mensaje = "La fecha de inicio de la actividad debe estar dentro de la fecha de inicio y fin del periodo";
            }
            if (planAsignaturaActiv.Fecha_Fin < FechaInicioPer.Date || planAsignaturaActiv.Fecha_Fin > FechaFinPer)
            {
                mensaje = "La fecha de finalización de la actividad debe estar dentro de la fecha de inicio y fin del periodo";
            }
            return mensaje;
        }

        /// <summary>
        /// Método que permite validar si la fecha actual es menor que la Fecha de inicio de periodo
        /// </summary>

        private bool validarFechaActualMenorInicioPeriodo(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            DA_PlanAsignatura objPasDA = new DA_PlanAsignatura();
            GD_Plan_Asignatura objPas = new GD_Plan_Asignatura();

            objPas.Id_Plan_Asignatura = planAsignaturaActiv.GD_Plan_Asignatura_Id_Plan_Asignatura;
            GD_Plan_Asignatura objPasEsp = objPasDA.obtener2(objPas);

            GD_Periodo_Academico objPer = objPasDA.obtenerPeriodo(objPasEsp.Id_Plan_Asignatura);

            DateTime FechaInicioPer = objPer.Fecha_Inicio.Value;
            DateTime FechaFinPer = objPer.Fecha_Fin.Value;

            if (DateTime.Now.Date < FechaInicioPer.Date)
            {
                return true;
            }
            else {
                return false;
            }

        }

        /// <summary>
        /// Método que permite obtener la lista de actividades con porcentaje de avances de un plan de asignatura
        /// </summary>
        public List<GD_Plan_Asignatura_Actividades> listaConPorcentajeAvance(GD_Plan_Asignatura planAsignatura) {
            DA_PlanAsignatura objDA = new DA_PlanAsignatura();
            return objDA.listaConPorcentajeAvance(planAsignatura);
        }
    }
}
