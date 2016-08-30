using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;

namespace GDirectiva.Domain.Main
{
    public class BL_Empleado
    {
        /// <summary>
        /// Método que devuelve un listado de empleados que dictan Asignatura enviada
        /// </summary>
        public List<GRMS_Empleado> listar(GD_Asignatura asignatura) {
            DA_Empleado objeto = new DA_Empleado();
            return objeto.listar(asignatura);
        }

        public List<GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA_Result> listarResponsables(int id_Plan_Asignatura)
        {
            DA_Empleado objeto = new DA_Empleado();
            return objeto.listarResponsables(id_Plan_Asignatura);
        }
    }
}
