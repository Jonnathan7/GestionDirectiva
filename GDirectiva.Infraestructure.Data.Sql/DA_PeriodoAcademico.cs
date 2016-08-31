using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_PeriodoAcademico
    {
        public List<PA_PERIODO_ACADEMICO_LISTA_VIGENTE_Result> ListarPeriodosAcademicosVigentes()
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_PERIODO_ACADEMICO_LISTA_VIGENTE_Result> objeto = new List<PA_PERIODO_ACADEMICO_LISTA_VIGENTE_Result>();
                objeto = contexto.PA_PERIODO_ACADEMICO_LISTA_VIGENTE().ToList();
                return objeto;
            }
        }

        public List<PA_PERIODO_ACADEMICO_LISTA_Result> ListarPeriodosAcademicos()
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_PERIODO_ACADEMICO_LISTA_Result> objeto = new List<PA_PERIODO_ACADEMICO_LISTA_Result>();
                objeto = contexto.PA_PERIODO_ACADEMICO_LISTA().ToList();
                return objeto;
            }
        }
    }
}
