using GDirectiva.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Core.Entities.Adapter
{
    public class PlanAsignaturaAdapter
    {
        public static EN_PlanAsignatura ObtenerPlanAsignaturaEntity(GD_SP_PLAN_ASIGNATURA_SEL_Result planAsignatura)
        {
            var planAsignaturaEntity = new EN_PlanAsignatura();

            planAsignaturaEntity.ID_PERIODO = planAsignatura.ID_PERIODO;
            planAsignaturaEntity.PERIODO = planAsignatura.PERIODO;
            planAsignaturaEntity.ID_PLAN_ASIGNATURA = planAsignatura.ID_PLAN_ASIGNATURA;
            planAsignaturaEntity.META = planAsignatura.META;
            planAsignaturaEntity.METODOLOGIA = planAsignatura.METODOLOGIA;
            planAsignaturaEntity.DOCUMENTO = planAsignatura.DOCUMENTO;
            planAsignaturaEntity.ESTADO = planAsignatura.ESTADO;
            planAsignaturaEntity.ID_PLAN_AREA = planAsignatura.ID_PLAN_AREA;
            planAsignaturaEntity.NOMBRE_PLAN_AREA = planAsignatura.NOMBRE_PLAN_AREA;
            planAsignaturaEntity.ID_EMPLEADO = planAsignatura.ID_EMPLEADO;
            planAsignaturaEntity.NOMBRE_EMPLEADO = planAsignatura.NOMBRE_EMPLEADO;
            planAsignaturaEntity.ID_ASIGNATURA = planAsignatura.ID_ASIGNATURA;
            planAsignaturaEntity.NOMBRE_CURSO = planAsignatura.NOMBRE_CURSO;

            return planAsignaturaEntity;
        }
    }
}
