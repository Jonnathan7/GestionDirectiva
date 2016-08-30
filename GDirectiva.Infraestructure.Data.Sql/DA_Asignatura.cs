using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_Asignatura
    {
        public List<Core.Entities.GD_Asignatura> listar(GD_Asignatura asignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                List<GD_Asignatura> objeto = (from x in contexto.GD_Asignatura
                                              where x.GD_Area_Id_Area.Equals(asignatura.GD_Area_Id_Area)
                                                   select x).ToList();
                if (objeto.Count > 0)
                {
                    return objeto;
                }
                else {
                    return null;
                }
                
            }
        }
    }
}
