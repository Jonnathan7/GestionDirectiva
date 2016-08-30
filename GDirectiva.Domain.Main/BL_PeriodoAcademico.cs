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
        /// <summary>
        ///  Método que devulve una lista de Periodos Académicos del año en curso
        /// </summary>
        public List<GD_Periodo_Academico> listar() {
            DA_PeriodoAcademico objeto = new DA_PeriodoAcademico();
            return objeto.listar();
        }

        public List<GD_SP_PERIODO_ACADEMICO_LISTA_Result> listarVigentes()
        {
            DA_PeriodoAcademico objeto = new DA_PeriodoAcademico();
            return objeto.listarVigentes();
        }
    }
}
