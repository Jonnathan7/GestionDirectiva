/// <summar4y>
/// Script de controlador de formulario de registro.
/// </summary>
/// <remarks>
/// Creacion: 	APC 07/08/2016
/// </remarks>
ns('GDirectiva.Presentacion.General.PlanAsignaturaCronograma.FormularioRegistro');
GDirectiva.Presentacion.General.PlanAsignaturaCronograma.FormularioRegistro.Controller = function (opts) {
    var base = this;

    base.Ini = function () {
        base.Control.FormularioRegistroActividad = new GDirectiva.Presentacion.General.PlanAsignaturaCronograma.FormularioRegistroActividad.Controller({
            
        });
        base.Event.GrabarSuccess = (opts.GrabarSuccess != null && opts.GrabarSuccess) ? opts.GrabarSuccess : null;
        base.Control.btnGrabarCronograma().click(base.Event.BtnGrabarClick);
        base.Control.btnCancelarCronograma().click(base.Event.BtnCancelarClick);
        base.Control.ValRegistro = new GDirectiva.Presentacion.Web.Components.Validator({
            form: base.Control.FormRegistro(),
            messages: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource
        });
        base.Control.btnAgregarCronograma().on('click', base.Event.BtnAgregarCronogramaClick);
        base.Function.CrearGrid();
        base.Event.BtnBuscarClick();
    };
    base.Mostrar = function (pIdPlanAsignatura) {
        pIdPlanAsignatura = (pIdPlanAsignatura == undefined) ? 0 : pIdPlanAsignatura;
        base.Control.DlgFormulario.getAjaxContent(
            {
                action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.FormularioRegistro,
                data: { pId_Plan_Asignatura: pIdPlanAsignatura },
                onSuccess: function () {
                    base.Ini();
                }
            }
        );
    };
    base.Control = {
        Mensaje: new GDirectiva.Presentacion.Web.Components.Message(),
        DlgFormulario: new GDirectiva.Presentacion.Web.Components.Dialog({
            title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaTituloFormulario,
            width: '80%',
            maxHeight: 750
        }),

        FormRegistro: function () { return $('#frmRegistrarPlanAsignatura'); },
        ValRegistro: null,
        btnGrabarCronograma: function () { return $('#btnFrmGrabarCronograma'); },
        btnCancelarCronograma: function () { return $('#btnFrmCancelarCronograma'); },
        btnAgregarCronograma: function () { return $('#btnAgregarCronograma'); },
        HdnCodigo: function () { return $('#hdnFormularioRegistroCodigo'); }
    };
    base.Configurations = {
        search: {
            parameters: null
        }
    };
    base.Event = {
        BtnBuscarClick: function () {
            base.Configurations.search.parameters = {
                pId_Plan_Asignatura: base.Control.HdnCodigo().val()
            };
            base.Control.GrdResultado.Load(base.Configurations.search.parameters);
        },
        BtnGrabarClick: function () {
            if (base.Control.ValRegistro.isValid()) {
                base.Ajax.AjaxRegistrar.data = {
                    Id_Plan_Asignatura: base.Control.HdnCodigo().val()                    
                };
                base.Ajax.AjaxRegistrar.submit();
            }
        },
        BtnCancelarClick: function () {
            base.Control.DlgFormulario.close();
        },
        BtnGridEditarActividadClick: function (row, data) {
            base.Control.FormularioRegistroActividad.MostrarActividades(base.Control.HdnCodigo().val(), data.ID_ACTIVIDAD);
        },
        BtnGridEliminarActividadClick: function (row, data) {
            base.Control.Mensaje.Confirmation({
                title: GDirectiva.Presentacion.Base.MensajeResource.ConfirmacionEliminacion,
                message: GDirectiva.Presentacion.Base.MensajeResource.TextoEliminacion,
                onAccept: function () {
                    base.Ajax.AjaxEliminarActividad.send({ pId_Actividad: data.ID_ACTIVIDAD, pIdPlan_Asignatura: base.Control.HdnCodigo().val() })
                }
            });
        },
        AjaxRegistrarSuccess: function (data) {
            if (data.IsProcess == true) {
                base.Control.Mensaje.Information({ message: GDirectiva.Presentacion.Base.MensajeResource.SeGuardoInformacionExito });
                if (base.Event.GrabarSuccess != null) {
                    base.Event.GrabarSuccess();
                }
                base.Control.DlgFormulario.close();
            } else {
                base.Control.DlgFormulario.close();
                base.Control.Mensaje.Error({ message: data.Message });
            }
        },
        AjaxEliminarActividadSuccess: function (data) {
            if (data.IsProcess == true) {
                base.Control.Mensaje.Information({ message: GDirectiva.Presentacion.Base.MensajeResource.SeEliminoRegistro });
                base.Control.GrdResultado.Load(base.Configurations.search.parameters);
            } else {
                base.Control.Mensaje.Error({ message: data.Message });
            }
        },
        BtnAgregarCronogramaClick: function () {
            base.Control.FormularioRegistroActividad.MostrarActividades(base.Control.HdnCodigo().val(), 0);
        }
    };
    base.Ajax = {
        AjaxRegistrar: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.Registrar,
            autoSubmit: false,
            onSuccess: base.Event.AjaxRegistrarSuccess
        }),
        AjaxEliminarActividad: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.EliminarActividad,
            autoSubmit: false,
            onSuccess: base.Event.AjaxEliminarActividadSuccess
        }),
    };
    base.Function = {
        CrearGrid: function () {
            var columns = new Array();
            columns.push({ data: 'NOMBRE_ACTIVIDAD', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaNombreActividad });
            columns.push({ data: 'FECHA_INICIO', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaFechaInicio });
            columns.push({ data: 'FECHA_FIN', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaFechaFin });
            columns.push({ data: 'PORCENTAJE', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaPorcentaje });
            columns.push({ data: 'NOMBRE_EMPLEADO', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaResponsables });

            var listaOpciones = new Array();
            listaOpciones.push({ type: GDirectiva.Presentacion.Web.Components.GridAction.Edit, event: { on: 'click', callBack: base.Event.BtnGridEditarActividadClick } });
            listaOpciones.push({ type: GDirectiva.Presentacion.Web.Components.GridAction.Delete, event: { on: 'click', callBack: base.Event.BtnGridEliminarActividadClick } });

            columns.push({
                data: null,
                title: GDirectiva.Presentacion.Base.GenericResource.GridAcciones,
                'sClass': "text-center controls",
                actionButtons: listaOpciones
            });

            base.Control.GrdResultado = new GDirectiva.Presentacion.Web.Components.Grid({
                renderTo: 'divGrdResultActividades',
                columns: columns,
                proxy: {
                    url: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.BuscarActividades,
                    source: 'Result'
                },
                hasSelectionRows: false
            });
        }
    };
};