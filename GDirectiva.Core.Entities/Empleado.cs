
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
    
public partial class Empleado
{

    public Empleado()
    {

        this.ActividadPlanAsignatura = new HashSet<ActividadPlanAsignatura>();

        this.ActividadPlanProyectoPedagogico = new HashSet<ActividadPlanProyectoPedagogico>();

        this.PlanAsignatura = new HashSet<PlanAsignatura>();

        this.Curso = new HashSet<Curso>();

    }


    public int Id_Empleado { get; set; }

    public string ApePaterno { get; set; }

    public string ApeMaterno { get; set; }

    public string Nombres { get; set; }

    public string Rol { get; set; }

    public string Area { get; set; }



    public virtual ICollection<ActividadPlanAsignatura> ActividadPlanAsignatura { get; set; }

    public virtual ICollection<ActividadPlanProyectoPedagogico> ActividadPlanProyectoPedagogico { get; set; }

    public virtual ICollection<PlanAsignatura> PlanAsignatura { get; set; }

    public virtual ICollection<Curso> Curso { get; set; }

}

}
