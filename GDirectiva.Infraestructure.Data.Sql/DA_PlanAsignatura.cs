using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_PlanAsignatura
    {
        public List<PA_ASIGNATURA_PLANAREA_LISTA_Result> ListarAsignaturaPlanArea(int planAreaId)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_ASIGNATURA_PLANAREA_LISTA_Result> objeto = new List<PA_ASIGNATURA_PLANAREA_LISTA_Result>();
                objeto = contexto.PA_ASIGNATURA_PLANAREA_LISTA(planAreaId).ToList();
                return objeto;
            }
        }

        public List<PA_PLAN_ASIGNATURA_LISTA_Result> ListarPlanAsignatura(int periodoacademicoId, int planAreaId, int asignaturaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_PLAN_ASIGNATURA_LISTA_Result> objeto = new List<PA_PLAN_ASIGNATURA_LISTA_Result>();
                objeto = contexto.PA_PLAN_ASIGNATURA_LISTA(periodoacademicoId, planAreaId, asignaturaId, pAGINA_INICIO, tAMANIO_PAGINA).ToList();
                return objeto;
            }
        }

        public List<PA_PLAN_ASIGNATURA_LISTA_VIGENTE_Result> ListarPlanAsignaturaVigente(int periodoacademicoId, int planAreaId, int asignaturaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_PLAN_ASIGNATURA_LISTA_VIGENTE_Result> objeto = new List<PA_PLAN_ASIGNATURA_LISTA_VIGENTE_Result>();
                objeto = contexto.PA_PLAN_ASIGNATURA_LISTA_VIGENTE(periodoacademicoId, planAreaId, asignaturaId, pAGINA_INICIO, tAMANIO_PAGINA).ToList();
                return objeto;
            }
        }

        public int EliminarPlanAsignaturaVigente(
            int id_planasignatura)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                var objeto = contexto.PA_ACTIVIDAD_PLAN_ASIGNATURA_ELIMINAR(id_planasignatura);
                return objeto;
            }
        }

        public int RegistrarPlanAsignaturaVigente(
            int id_actividadplanasignatura,
            int id_empleado,
            string actividad,
            DateTime fechainicio,
            DateTime fechafin,
            int porcentaje)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                var objeto = contexto.PA_ACTIVIDAD_PLAN_ASIGNATURA_REGISTRAR(actividad, (DateTime)fechainicio, (DateTime)fechafin, (int)porcentaje, 2, (int)id_empleado);
                return objeto;
            }
        }

        public PA_PLAN_ASIGNATURA_SEL_Result ObtenerPlanAsignatura(int planAsignaturaId)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PA_PLAN_ASIGNATURA_SEL_Result objeto = new PA_PLAN_ASIGNATURA_SEL_Result();
                objeto = contexto.PA_PLAN_ASIGNATURA_SEL(planAsignaturaId).ToList().FirstOrDefault();
                return objeto;
            }
        }

        public PlanAsignatura InsertarPlanAsignatura(PlanAsignatura planAsignatura)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanAsignatura objeto = contexto.PlanAsignatura.Add(planAsignatura);
                if (contexto.SaveChanges() > 0)
                {
                    return objeto;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool ActualizarPlanAsignatura(PlanAsignatura planAsignatura)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanAsignatura objeto = (from x in contexto.PlanAsignatura
                                   where x.Id_PlanAsignatura == planAsignatura.Id_PlanAsignatura
                                   select x).FirstOrDefault();

                objeto.Meta = planAsignatura.Meta;
                objeto.Metodologia = planAsignatura.Metodologia;
                objeto.Id_PlanArea = planAsignatura.Id_PlanArea;
                objeto.Id_Asignatura = planAsignatura.Id_Asignatura;
                objeto.Estado = planAsignatura.Estado;
                objeto.Documento = planAsignatura.Documento;
                objeto.FechaModificacion = planAsignatura.FechaModificacion;

                if (contexto.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool EliminarPlanAsignatura(PlanAsignatura planAsignatura)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanAsignatura objeto = (from x in contexto.PlanAsignatura
                                         where x.Id_PlanAsignatura == planAsignatura.Id_PlanAsignatura
                                         select x).FirstOrDefault();

                objeto.Id_PlanAsignatura = planAsignatura.Id_PlanAsignatura;
                objeto.Estado = planAsignatura.Estado;

                if (contexto.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
