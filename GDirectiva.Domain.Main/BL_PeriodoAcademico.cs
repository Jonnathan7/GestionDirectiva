using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;

namespace GDirectiva.Domain.Main
{
    public class BL_PeriodoAcademico
    {
        public List<PA_PERIODO_ACADEMICO_LISTA_VIGENTE_Result> ListarPeriodosAcademicosVigentes()
        {
            DA_PeriodoAcademico objeto = new DA_PeriodoAcademico();
            return objeto.ListarPeriodosAcademicosVigentes();
        }
        public List<PA_PERIODO_ACADEMICO_LISTA_Result> ListarPeriodosAcademicos()
        {
            DA_PeriodoAcademico objeto = new DA_PeriodoAcademico();
            return objeto.ListarPeriodosAcademicos();
        }
    }
}
