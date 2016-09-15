using GDirectiva.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Infraestructure.Data.Sql
{
    public class DA_Area
    {
        public List<PA_AREA_LISTA_Result> ListarArea()
        {
            using (DB_INNOVASCHOOLSEntities contexto = new DB_INNOVASCHOOLSEntities())
            {
                List<PA_AREA_LISTA_Result> objeto = new List<PA_AREA_LISTA_Result>();
                objeto = contexto.PA_AREA_LISTA().ToList();
                return objeto;
            }
        }
    }
}
