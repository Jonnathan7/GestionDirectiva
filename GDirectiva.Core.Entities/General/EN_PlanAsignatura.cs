using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Core.Entities.General
{
    public class EN_PlanAsignatura
    {
        public int ID_PERIODO { get; set; }
        public string PERIODO { get; set; }
        public int ID_PLAN_ASIGNATURA { get; set; }
        public string META { get; set; }
        public string METODOLOGIA { get; set; }
        public string ESTADO { get; set; }
        public string DOCUMENTO { get; set; }
        public int ID_PLAN_AREA { get; set; }
        public string NOMBRE_PLAN_AREA { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string NOMBRE_EMPLEADO { get; set; }
        public int ID_ASIGNATURA { get; set; }
        public string NOMBRE_CURSO { get; set; }
    }
}
