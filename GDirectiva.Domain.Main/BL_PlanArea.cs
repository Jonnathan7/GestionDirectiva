using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;

namespace GDirectiva.Domain.Main
{
    public class BL_PlanArea
    {
        /// <summary>
        /// Método que devuelve una lista de planes de Area
        /// </summary>
        public List<GD_Plan_Area> listar(GD_Plan_Area planArea) {
            DA_PlanArea objeto = new DA_PlanArea();
            return objeto.listar(planArea);
        }

        public GD_Plan_Area obtener(GD_Plan_Area planArea)
        {
            DA_PlanArea objeto = new DA_PlanArea();
            return objeto.obtener(planArea);
        }
    }
}
