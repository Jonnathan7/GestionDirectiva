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
        public List<Core.Entities.PlanArea> listar(PlanArea planArea)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PlanArea> objeto = (from x in contexto.PlanArea
                                             where x.Id_PeriodoAcademico.Equals(planArea.Id_PeriodoAcademico)
                                                   select x).ToList();
                if (objeto.Count>0)
                {
                    return objeto;
                }
                else {
                    return null;
                }
                
            }
        }

        public GD_Plan_Area obtener(GD_Plan_Area planArea)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                GD_Plan_Area objeto = (from x in contexto.GD_Plan_Area
                                       where x.Id_Plan_Area.Equals(planArea.Id_Plan_Area)
                                       select x).FirstOrDefault();
                return objeto;
            }
        } 
    }
}
