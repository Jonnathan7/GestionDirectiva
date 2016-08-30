﻿using GDirectiva.Core.Entities;
using GDirectiva.Presentacion.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Presentacion.Core.ViewModel.General
{
    public class PlanAsignaturaCronogramaBusquedaModel : GenericViewModel
    {
        public PlanAsignaturaCronogramaBusquedaModel()
        {

        }
        public List<GD_Periodo_Academico> ListaPeriodoAcademico { get; set; }
    }
}
