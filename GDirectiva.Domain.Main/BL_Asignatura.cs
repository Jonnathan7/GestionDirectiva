using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;

namespace GDirectiva.Domain.Main
{
    public class BL_Asignatura
    {
        /// <summary>
        /// Método que devuelve una lista de Asignaturas de acuerdo al Código de Plan de Area enviado como parámetro
        /// </summary>
        public List<GD_Asignatura> listar(GD_Asignatura asignatura) {
            DA_Asignatura objeto = new DA_Asignatura();
            return objeto.listar(asignatura);
        }
    }
}
