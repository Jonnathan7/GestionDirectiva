using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Core.Entities.General
{
    [Serializable]
    public class EN_PlanArea
    {
        public int Id_PlanArea { get; set; }
        public string Nombre { get; set; }
        public string Objetivos { get; set; }
        public string Criterios { get; set; }
        public string Requisitos { get; set; }
        public string Estado { get; set; }
        public string Documento { get; set; }
        public PeriodoAcademico PeriodoAcademicoBE { get; set; }
        public PlanEstudio PlanEstudioBE { get; set; }
        public Grado GradoBE { get; set; }
        public Area AreaBE { get; set; }

        public EN_PlanArea()
        {
            PeriodoAcademicoBE = new PeriodoAcademico();
            PlanEstudioBE = new PlanEstudio();
            GradoBE = new Grado();
            AreaBE = new Area();
        }

    }
}
