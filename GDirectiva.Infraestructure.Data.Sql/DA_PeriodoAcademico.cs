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
        public List<Core.Entities.GD_Periodo_Academico> listar()
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {   
                List<GD_Periodo_Academico> objeto = (from x in contexto.GD_Periodo_Academico
                                                     where x.Fecha_Inicio.Value.Year==DateTime.Now.Year
                                                   select x).ToList();
                if (objeto.Count>0) { 
                    return objeto;
                }else{
                    return null;
                }
                
            }
        }

        public List<GD_SP_PERIODO_ACADEMICO_LISTA_Result> listarVigentes()
        {
            using (GESTION_DIRECTIVAEntities contexto = new GESTION_DIRECTIVAEntities())
            {
                List<GD_SP_PERIODO_ACADEMICO_LISTA_Result> objeto = new List<GD_SP_PERIODO_ACADEMICO_LISTA_Result>();
                objeto = contexto.GD_SP_PERIODO_ACADEMICO_LISTA().ToList();
                return objeto;
            }
        }
    }
}
