using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;


namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_PlanAsignatura_Actividad
    {
        public GD_Plan_Asignatura_Actividades obtener(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura_Actividades objeto = (from x in contexto.GD_Plan_Asignatura_Actividades
                                                             where x.Id_Actividad.Equals(planAsignaturaActiv.Id_Actividad)
                                                             select x).FirstOrDefault();

                    return objeto;
            }
        }
        public List<GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA_Result> listar(GD_Plan_Asignatura planAsignatura, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                List<GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA_Result> objeto = new List<GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA_Result>();
                objeto = contexto.GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA(planAsignatura.Id_Plan_Asignatura, pAGINA_INICIO, tAMANIO_PAGINA).ToList();
                return objeto;
            }
        }
        public GD_Plan_Asignatura_Actividades registrar(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura_Actividades objeto = contexto.GD_Plan_Asignatura_Actividades.Add(planAsignaturaActiv);
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

        public bool actualizar(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura_Actividades objeto = (from x in contexto.GD_Plan_Asignatura_Actividades
                                             where x.Id_Actividad == planAsignaturaActiv.Id_Actividad
                                             select x).FirstOrDefault();

                objeto.Actividad = planAsignaturaActiv.Actividad;
                objeto.Fecha_Inicio = planAsignaturaActiv.Fecha_Inicio;
                objeto.Fecha_Fin = planAsignaturaActiv.Fecha_Fin;
                //objeto.Porcentaje_Avance = planAsignaturaActiv.Porcentaje_Avance;
                objeto.GRMS_Empleado_Id_Empleado = planAsignaturaActiv.GRMS_Empleado_Id_Empleado;

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
        public bool actualizarPorcentajeAvance(GD_Plan_Asignatura_Actividades planAsignaturaActiv) {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura_Actividades objeto = (from x in contexto.GD_Plan_Asignatura_Actividades
                                                         where x.Id_Actividad == planAsignaturaActiv.Id_Actividad
                                                         select x).FirstOrDefault();

                objeto.Porcentaje_Avance = planAsignaturaActiv.Porcentaje_Avance;

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
        public bool eliminar(GD_Plan_Asignatura_Actividades planAsignaturaActiv)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura_Actividades objeto = (from x in contexto.GD_Plan_Asignatura_Actividades
                                             where x.Id_Actividad == planAsignaturaActiv.Id_Actividad
                                             select x).FirstOrDefault();

                objeto.Estado = "ELIMINADO";

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
