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
    
    public partial class GD_Grado
    {
        public GD_Grado()
        {
            this.GD_Plan_Area = new HashSet<GD_Plan_Area>();
        }
    
        public int Id_Grado { get; set; }
        public string Grado { get; set; }
    
        public virtual ICollection<GD_Plan_Area> GD_Plan_Area { get; set; }
    }
}
