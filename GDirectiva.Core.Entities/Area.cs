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
    
    public partial class Area
    {
        public Area()
        {
            this.Asignatura = new HashSet<Asignatura>();
            this.PlanArea = new HashSet<PlanArea>();
            this.PlanProyectoPedagogico = new HashSet<PlanProyectoPedagogico>();
        }
    
        public int Id_Area { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<Asignatura> Asignatura { get; set; }
        public virtual ICollection<PlanArea> PlanArea { get; set; }
        public virtual ICollection<PlanProyectoPedagogico> PlanProyectoPedagogico { get; set; }
    }
}
