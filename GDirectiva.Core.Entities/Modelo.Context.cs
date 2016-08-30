﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GESTION_DIRECTIVAEntities : DbContext
    {
        public GESTION_DIRECTIVAEntities()
            : base("name=GESTION_DIRECTIVAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GD_Area> GD_Area { get; set; }
        public virtual DbSet<GD_Asignatura> GD_Asignatura { get; set; }
        public virtual DbSet<GD_Empleado_Asignatura> GD_Empleado_Asignatura { get; set; }
        public virtual DbSet<GD_Grado> GD_Grado { get; set; }
        public virtual DbSet<GD_Observacion> GD_Observacion { get; set; }
        public virtual DbSet<GD_Periodo_Academico> GD_Periodo_Academico { get; set; }
        public virtual DbSet<GD_Plan_Area> GD_Plan_Area { get; set; }
        public virtual DbSet<GD_Plan_Asignatura> GD_Plan_Asignatura { get; set; }
        public virtual DbSet<GD_Plan_Asignatura_Actividades> GD_Plan_Asignatura_Actividades { get; set; }
        public virtual DbSet<GRMS_Empleado> GRMS_Empleado { get; set; }
    
        public virtual ObjectResult<GD_SP_PLAN_ASIGNATURA_LISTA_Result> GD_SP_PLAN_ASIGNATURA_LISTA(Nullable<int> id_Periodo, Nullable<int> id_Plan_Area, Nullable<int> id_Asignatura, Nullable<int> id_Empleado, Nullable<int> pAGINA_INICIO, Nullable<int> tAMANIO_PAGINA)
        {
            var id_PeriodoParameter = id_Periodo.HasValue ?
                new ObjectParameter("Id_Periodo", id_Periodo) :
                new ObjectParameter("Id_Periodo", typeof(int));
    
            var id_Plan_AreaParameter = id_Plan_Area.HasValue ?
                new ObjectParameter("Id_Plan_Area", id_Plan_Area) :
                new ObjectParameter("Id_Plan_Area", typeof(int));
    
            var id_AsignaturaParameter = id_Asignatura.HasValue ?
                new ObjectParameter("Id_Asignatura", id_Asignatura) :
                new ObjectParameter("Id_Asignatura", typeof(int));
    
            var id_EmpleadoParameter = id_Empleado.HasValue ?
                new ObjectParameter("Id_Empleado", id_Empleado) :
                new ObjectParameter("Id_Empleado", typeof(int));
    
            var pAGINA_INICIOParameter = pAGINA_INICIO.HasValue ?
                new ObjectParameter("PAGINA_INICIO", pAGINA_INICIO) :
                new ObjectParameter("PAGINA_INICIO", typeof(int));
    
            var tAMANIO_PAGINAParameter = tAMANIO_PAGINA.HasValue ?
                new ObjectParameter("TAMANIO_PAGINA", tAMANIO_PAGINA) :
                new ObjectParameter("TAMANIO_PAGINA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GD_SP_PLAN_ASIGNATURA_LISTA_Result>("GD_SP_PLAN_ASIGNATURA_LISTA", id_PeriodoParameter, id_Plan_AreaParameter, id_AsignaturaParameter, id_EmpleadoParameter, pAGINA_INICIOParameter, tAMANIO_PAGINAParameter);
        }
    
        public virtual int GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_ELIMINAR(Nullable<int> id_Plan_Asignatura)
        {
            var id_Plan_AsignaturaParameter = id_Plan_Asignatura.HasValue ?
                new ObjectParameter("Id_Plan_Asignatura", id_Plan_Asignatura) :
                new ObjectParameter("Id_Plan_Asignatura", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_ELIMINAR", id_Plan_AsignaturaParameter);
        }
    
        public virtual ObjectResult<GD_SP_PLAN_ASIGNATURA_SEL_Result> GD_SP_PLAN_ASIGNATURA_SEL(Nullable<int> iD_PLAN_ASIGNATURA)
        {
            var iD_PLAN_ASIGNATURAParameter = iD_PLAN_ASIGNATURA.HasValue ?
                new ObjectParameter("ID_PLAN_ASIGNATURA", iD_PLAN_ASIGNATURA) :
                new ObjectParameter("ID_PLAN_ASIGNATURA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GD_SP_PLAN_ASIGNATURA_SEL_Result>("GD_SP_PLAN_ASIGNATURA_SEL", iD_PLAN_ASIGNATURAParameter);
        }
    
        public virtual ObjectResult<GD_SP_PERIODO_ACADEMICO_LISTA_Result> GD_SP_PERIODO_ACADEMICO_LISTA()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GD_SP_PERIODO_ACADEMICO_LISTA_Result>("GD_SP_PERIODO_ACADEMICO_LISTA");
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA_Result> GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA(Nullable<int> id_Plan_Asignatura, Nullable<int> pAGINA_INICIO, Nullable<int> tAMANIO_PAGINA)
        {
            var id_Plan_AsignaturaParameter = id_Plan_Asignatura.HasValue ?
                new ObjectParameter("Id_Plan_Asignatura", id_Plan_Asignatura) :
                new ObjectParameter("Id_Plan_Asignatura", typeof(int));
    
            var pAGINA_INICIOParameter = pAGINA_INICIO.HasValue ?
                new ObjectParameter("PAGINA_INICIO", pAGINA_INICIO) :
                new ObjectParameter("PAGINA_INICIO", typeof(int));
    
            var tAMANIO_PAGINAParameter = tAMANIO_PAGINA.HasValue ?
                new ObjectParameter("TAMANIO_PAGINA", tAMANIO_PAGINA) :
                new ObjectParameter("TAMANIO_PAGINA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA_Result>("GD_SP_PLAN_ASIGNATURA_ACTIVIDADES_LISTA", id_Plan_AsignaturaParameter, pAGINA_INICIOParameter, tAMANIO_PAGINAParameter);
        }
    
        public virtual ObjectResult<GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA_Result> GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA(Nullable<int> id_Plan_Asignatura)
        {
            var id_Plan_AsignaturaParameter = id_Plan_Asignatura.HasValue ?
                new ObjectParameter("Id_Plan_Asignatura", id_Plan_Asignatura) :
                new ObjectParameter("Id_Plan_Asignatura", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA_Result>("GD_SP_PLAN_ASIGNATURA_EMPLEADO_LISTA", id_Plan_AsignaturaParameter);
        }
    }
}
