using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_PlanArea
    {
        public List<PA_PLAN_AREA_LISTA_Result> ListarPlanArea(int periodoacademicoId, int gradoId, int areaId, int pAGINA_INICIO, int tAMANIO_PAGINA)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_PLAN_AREA_LISTA_Result> objeto = new List<PA_PLAN_AREA_LISTA_Result>();
                objeto = contexto.PA_PLAN_AREA_LISTA(periodoacademicoId, gradoId, areaId, pAGINA_INICIO, tAMANIO_PAGINA).ToList();
                return objeto;
            }
        }

        public List<PA_PLAN_AREA_LISTA_VIGENTE_Result> ListarPlanAreaVigente(int pID_PERIODOACADEMICO)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_PLAN_AREA_LISTA_VIGENTE_Result> objeto = new List<PA_PLAN_AREA_LISTA_VIGENTE_Result>();
                objeto = contexto.PA_PLAN_AREA_LISTA_VIGENTE(pID_PERIODOACADEMICO).ToList();
                return objeto;
            }
        }

        public PA_PLAN_AREA_SEL_Result ObtenerPlanArea(int pIdPlanArea)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PA_PLAN_AREA_SEL_Result objeto = new PA_PLAN_AREA_SEL_Result();
                objeto = contexto.PA_PLAN_AREA_SEL(pIdPlanArea).ToList().FirstOrDefault();
                return objeto;
            }
        }

        public int ObtenerPlanAreaExiste(int periodoacademicoId, int areaId, int planEstudioId, int gradoId)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                var objeto = contexto.PA_PLAN_AREA_EXISTE(periodoacademicoId, areaId, planEstudioId, gradoId).FirstOrDefault();
                return Convert.ToInt32(objeto);
            }
        }

        public PlanArea InsertarPlanArea(PlanArea planArea)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanArea objeto = contexto.PlanArea.Add(planArea);
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

        public bool ActualizarPlanArea(PlanArea planArea)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanArea objeto = (from x in contexto.PlanArea
                                   where x.Id_PlanArea == planArea.Id_PlanArea
                                   select x).FirstOrDefault();

                objeto.Nombre = planArea.Nombre;
                objeto.Objetivos = planArea.Objetivos;
                objeto.Criterios = planArea.Criterios;
                objeto.Requisitos = planArea.Requisitos;
                objeto.Estado = planArea.Estado;
                objeto.Documento = planArea.Documento;
                objeto.FechaModificacion = planArea.FechaModificacion;

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

        public bool EliminarPlanArea(PlanArea planArea)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanArea objeto = (from x in contexto.PlanArea
                                   where x.Id_PlanArea == planArea.Id_PlanArea
                                   select x).FirstOrDefault();

                objeto.Id_PlanArea = planArea.Id_PlanArea;
                objeto.Estado = planArea.Estado;

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
