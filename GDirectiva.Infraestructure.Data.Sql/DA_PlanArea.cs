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
        public List<Core.Entities.GD_Plan_Area> listar(GD_Plan_Area planArea)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                List<GD_Plan_Area> objeto = (from x in contexto.GD_Plan_Area
                                             where x.GD_Periodo_Academico_Id_Periodo.Equals(planArea.GD_Periodo_Academico_Id_Periodo)
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
