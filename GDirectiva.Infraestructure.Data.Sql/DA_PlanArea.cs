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
        public List<usp_gd_PlanArea_Listar_Result> ListarPlanArea(int periodoacademicoId, int gradoId, int areaId)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<usp_gd_PlanArea_Listar_Result> objeto = new List<usp_gd_PlanArea_Listar_Result>();
                objeto = contexto.usp_gd_PlanArea_Listar(periodoacademicoId, gradoId, areaId).ToList();
                return objeto;
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
