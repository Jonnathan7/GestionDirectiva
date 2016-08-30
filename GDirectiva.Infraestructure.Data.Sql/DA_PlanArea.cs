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

        public PlanArea obtener(PlanArea planArea)
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PlanArea objeto = (from x in contexto.PlanArea
                                       where x.Id_PlanArea.Equals(planArea.Id_PlanArea)
                                       select x).FirstOrDefault();
                return objeto;
            }
        } 
    }
}
