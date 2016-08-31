using GDirectiva.Core.Entities;
using GDirectiva.Infraestructure.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Domain.Main
{
    public class BL_Area
    {
        public List<PA_AREA_LISTA_Result> ListarArea()
        {
            DA_Area objeto = new DA_Area();
            return objeto.ListarArea();
        }
    }
}
