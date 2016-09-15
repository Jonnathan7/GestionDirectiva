using GDirectiva.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_PlanEstudio
    {
        public PA_PLAN_ESTUDIO_SEL_Result ObtenerPlanEstudioVigente()
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                PA_PLAN_ESTUDIO_SEL_Result objeto = new PA_PLAN_ESTUDIO_SEL_Result();
                objeto = contexto.PA_PLAN_ESTUDIO_SEL().ToList().FirstOrDefault();
                return objeto;
            }
        }
    }
}
