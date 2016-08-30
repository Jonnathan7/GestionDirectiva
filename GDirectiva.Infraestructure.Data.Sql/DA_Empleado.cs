using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_Empleado
    {
        public List<GRMS_Empleado> listar(GD_Asignatura asignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                List<GRMS_Empleado> objeto = (from emp in contexto.GRMS_Empleado join empAsig in contexto.GD_Empleado_Asignatura 
                                              on emp.Id_Empleado equals empAsig.GRMS_Empleado_Id_Empleado
                                             where empAsig.GD_Asignatura_Id_Asignatura.Equals(asignatura.Id_Asignatura)
                                             select emp).ToList();
                if (objeto.Count > 0)
                {
                    return objeto;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA_Result> listarResponsables(int id_Plan_Asignatura)
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                List<GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA_Result> objeto = new List<GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA_Result>();
                objeto = contexto.GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA(id_Plan_Asignatura).ToList();
                return objeto;
            }
        }
    }
}
