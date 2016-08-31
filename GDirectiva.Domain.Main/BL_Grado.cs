using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Domain.Main
{
    public class BL_Grado
    {
        public List<PA_GRADO_LISTA_Result> ListarGrados()
        {
            DA_Grado objeto = new DA_Grado();
            return objeto.ListarGrados();
        }
    }
}
