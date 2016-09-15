using GDirectiva.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_Grado
    {
        public List<PA_GRADO_LISTA_Result> ListarGrados()
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_GRADO_LISTA_Result> objeto = new List<PA_GRADO_LISTA_Result>();
                objeto = contexto.PA_GRADO_LISTA().ToList();
                return objeto;
            }
        }
    }
}
