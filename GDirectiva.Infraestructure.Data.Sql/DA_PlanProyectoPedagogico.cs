using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_PlanProyectoPedagogico
    {
        public List<PA_PLAN_PROYECTO_PEDAGOGICO_LISTA_Result> ListarPlanProyectoPedagogico(int periodoacademicoId, int gradoId, int areaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_PLAN_PROYECTO_PEDAGOGICO_LISTA_Result> objeto = new List<PA_PLAN_PROYECTO_PEDAGOGICO_LISTA_Result>();
                objeto = contexto.PA_PLAN_PROYECTO_PEDAGOGICO_LISTA(periodoacademicoId, gradoId, areaId, pAGINA_INICIO, tAMANIO_PAGINA).ToList();
                return objeto;
            }
        }
        /*
        public List<PA_PLAN_AREA_LISTA_VIGENTE_Result> ListarPlanAreaVigente(int pID_PERIODOACADEMICO)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_PLAN_AREA_LISTA_VIGENTE_Result> objeto = new List<PA_PLAN_AREA_LISTA_VIGENTE_Result>();
                objeto = contexto.PA_PLAN_AREA_LISTA_VIGENTE(pID_PERIODOACADEMICO).ToList();
                return objeto;
            }
        }*/

        public PA_PLAN_PROYECTO_PEDAGOGICO_SEL_Result ObtenerPlanProyectoPedagogico(int planProyectoPedagogicoId)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PA_PLAN_PROYECTO_PEDAGOGICO_SEL_Result objeto = new PA_PLAN_PROYECTO_PEDAGOGICO_SEL_Result();
                objeto = contexto.PA_PLAN_PROYECTO_PEDAGOGICO_SEL(planProyectoPedagogicoId).ToList().FirstOrDefault();
                return objeto;
            }
        }

        public int ObtenerPlanProyectoPedagogicoExiste(int periodoacademicoId, int areaId, int planEstudioId, int gradoId)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                var objeto = contexto.PA_PLAN_PROYECTO_PEDAGOGICO_EXISTE(periodoacademicoId, areaId, planEstudioId, gradoId).FirstOrDefault();
                return Convert.ToInt32(objeto);
            }
        }

        public PlanProyectoPedagogico InsertarPlanProyectoPedagogico(PlanProyectoPedagogico planProyectoPedagogico)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanProyectoPedagogico objeto = contexto.PlanProyectoPedagogico.Add(planProyectoPedagogico);
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

        public bool ActualizarPlanProyectoPedagogico(PlanProyectoPedagogico planProyectoPedagogico)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanProyectoPedagogico objeto = (from x in contexto.PlanProyectoPedagogico
                                                 where x.Id_ProyectoPedagogico == planProyectoPedagogico.Id_ProyectoPedagogico
                                   select x).FirstOrDefault();

                objeto.Nombre = planProyectoPedagogico.Nombre;
                objeto.Objetivos = planProyectoPedagogico.Objetivos;
                objeto.Descripcion = planProyectoPedagogico.Descripcion;
                objeto.Estado = planProyectoPedagogico.Estado;
                objeto.Documento = planProyectoPedagogico.Documento;
                objeto.FechaModificacion = planProyectoPedagogico.FechaModificacion;

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

        public bool EliminarPlanProyectoPedagogico(PlanProyectoPedagogico planProyectoPedagogico)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanProyectoPedagogico objeto = (from x in contexto.PlanProyectoPedagogico
                                   where x.Id_ProyectoPedagogico == planProyectoPedagogico.Id_ProyectoPedagogico
                                   select x).FirstOrDefault();

                objeto.Id_ProyectoPedagogico = planProyectoPedagogico.Id_ProyectoPedagogico;
                objeto.Estado = planProyectoPedagogico.Estado;

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
