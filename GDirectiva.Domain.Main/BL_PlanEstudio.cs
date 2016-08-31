using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Domain.Main
{
    public class BL_PlanEstudio
    {
        public PA_PLAN_ESTUDIO_SEL_Result ObtenerPlanEstudioVigente()
        {
            DA_PlanEstudio objeto = new DA_PlanEstudio();
            return objeto.ObtenerPlanEstudioVigente();
        }
    }
}
