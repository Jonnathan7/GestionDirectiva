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
    
    public partial class PlanEstudio
    {
        public PlanEstudio()
        {
            this.PlanArea = new HashSet<PlanArea>();
            this.PlanProyectoPedagogico = new HashSet<PlanProyectoPedagogico>();
        }
    
        public int Id_PlanEstudio { get; set; }
        public string Descripción { get; set; }
        public int Anio { get; set; }
        public string Estado { get; set; }
        public string Documento { get; set; }
    
        public virtual ICollection<PlanArea> PlanArea { get; set; }
        public virtual ICollection<PlanProyectoPedagogico> PlanProyectoPedagogico { get; set; }
    }
}
