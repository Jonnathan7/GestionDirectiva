//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GDirectiva.Core.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActividadPlanProyectoPedagogico
    {
        public int Id_ActividadProyectoPedagogico { get; set; }
        public string Actividad { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public int PorcentajeAvance { get; set; }
        public Nullable<int> Id_ProyectoPedagogico { get; set; }
        public Nullable<int> Id_Empleado { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual PlanProyectoPedagogico PlanProyectoPedagogico { get; set; }
    }
}
