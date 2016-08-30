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
        public GD_SP_PLAN_ASIGNATURA_SEL_Result obtener(int pId_Plan_Asignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_SP_PLAN_ASIGNATURA_SEL_Result objeto = new GD_SP_PLAN_ASIGNATURA_SEL_Result();
                objeto = contexto.GD_SP_PLAN_ASIGNATURA_SEL(pId_Plan_Asignatura).FirstOrDefault();
                return objeto;
            }
        }

        public GD_Plan_Asignatura obtener2(GD_Plan_Asignatura planAsignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura objeto = (from x in contexto.GD_Plan_Asignatura
                                             where x.Id_Plan_Asignatura.Equals(planAsignatura.Id_Plan_Asignatura)
                                             && x.Estado != "ELIMINADO"
                                             select x).FirstOrDefault();

                return objeto;
            }
        }

        public List<GD_SP_PLAN_ASIGNATURA_LISTA_Result> listar(int idPeriodo, GD_Plan_Asignatura planAsignatura, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                List<GD_SP_PLAN_ASIGNATURA_LISTA_Result> objeto = new List<GD_SP_PLAN_ASIGNATURA_LISTA_Result>();
                objeto = contexto.GD_SP_PLAN_ASIGNATURA_LISTA(idPeriodo, planAsignatura.GD_Plan_Area_Id_Plan_Area, planAsignatura.GD_Asignatura_Id_Asignatura, planAsignatura.GRMS_Empleado_Id_Empleado, pAGINA_INICIO, tAMANIO_PAGINA).ToList();
                return objeto;
            }
        }

        public GD_Plan_Asignatura registrar(GD_Plan_Asignatura planAsignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura objeto = contexto.GD_Plan_Asignatura.Add(planAsignatura);
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

        public bool actualizar(GD_Plan_Asignatura planAsignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura objeto = (from x in contexto.GD_Plan_Asignatura
                                             where x.Id_Plan_Asignatura == planAsignatura.Id_Plan_Asignatura
                                             select x).FirstOrDefault();

                objeto.Meta = planAsignatura.Meta;
                objeto.Metodologia = planAsignatura.Metodologia;
                objeto.Documento = planAsignatura.Documento;
                objeto.Estado = planAsignatura.Estado;
                objeto.GD_Plan_Area_Id_Plan_Area = planAsignatura.GD_Plan_Area_Id_Plan_Area;
                objeto.GRMS_Empleado_Id_Empleado = planAsignatura.GRMS_Empleado_Id_Empleado;
                objeto.GD_Asignatura_Id_Asignatura = planAsignatura.GD_Asignatura_Id_Asignatura;

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

        public bool eliminar(GD_Plan_Asignatura planAsignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura objeto = (from x in contexto.GD_Plan_Asignatura
                                             where x.Id_Plan_Asignatura == planAsignatura.Id_Plan_Asignatura
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

        public GD_Periodo_Academico obtenerPeriodo(int pId_Plan_Asignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Asignatura objeto = (from x in contexto.GD_Plan_Asignatura
                                             where x.Id_Plan_Asignatura.Equals(pId_Plan_Asignatura)
                                             select x).FirstOrDefault();
                GD_Periodo_Academico objPER = objeto.GD_Plan_Area.GD_Periodo_Academico;
                return objPER;
            }
        }
        public bool eliminarCronograma(GD_Plan_Asignatura planAsignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                int res = contexto.GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_ELIMINAR(planAsignatura.Id_Plan_Asignatura);

                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<GD_Plan_Asignatura_Actividades> listaConPorcentajeAvance(GD_Plan_Asignatura planAsignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                List<GD_Plan_Asignatura_Actividades> objeto = (from x in contexto.GD_Plan_Asignatura_Actividades
                                                               where x.GD_Plan_Asignatura_Id_Plan_Asignatura.Equals(planAsignatura.Id_Plan_Asignatura) &&
                                                               x.Porcentaje_Avance > 0 && x.Estado != "ELIMINADO"
                                                               select x).ToList();

                return objeto;

            }
        }
    }
}
