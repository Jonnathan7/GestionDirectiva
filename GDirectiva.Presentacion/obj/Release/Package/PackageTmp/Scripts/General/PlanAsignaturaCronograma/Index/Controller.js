/// <summary>
/// Script de controlador de Cronograma de Plan de Asignatura.
/// </summary>
/// <remarks>
/// Creacion: 	JPC 07/08/2016
/// </remarks>
ns('GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Index');


GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Index.Controller = function () {
    var base = this;

    base.Ini = function () {
        base.Control.FormularioRegistro = new GDirectiva.Presentacion.General.PlanAsignaturaCronograma.FormularioRegistro.Controller({
            GrabarSuccess: function () {
                base.Control.GrdResultado.Load(base.Configurations.search.parameters);
            }
        });
        base.Control.SlcPeriodoAcademico().change(base.Event.SlcPeriodoAcademicoChange);
        base.Control.SlcPlanArea().change(base.Event.SlcPlanAreaChange);
        base.Control.SlcAsignatura().change(base.Event.SlcAsignaturaChange);
        base.Control.BtnAgregar().on('click', base.Event.BtnAgregarClick);
        base.Control.BtnBuscar().on('click', base.Event.BtnBuscarClick);

        base.Function.CrearGrid();
        base.Control.BtnBuscar().click();
    };


    base.Control = {
        Mensaje: new GDirectiva.Presentacion.Web.Components.Message(),
        GrdResultado: null,
        ValBusqueda: null,
        FrmBusqueda: function () { return $('#frmPlanAsignaturaBusqueda'); },
        SlcPeriodoAcademico: function () { return $('#slcPeriodoAcademico'); },
        SlcPlanArea: function () { return $('#slcPlanArea'); },
        SlcAsignatura: function () { return $('#slcAsignatura'); },
        BtnAgregar: function () { return $('#btnAgregar'); },
        BtnEliminar: function () { return $('#btnEliminar'); },
        BtnBuscar: function () { return $('#btnBuscar'); }
    };

    base.Configurations = {
        search: {
            parameters: null
        }
    };

    base.Event = {

        BtnBuscarClick: function () {
            base.Configurations.search.parameters = {
                pId_Periodo: base.Control.SlcPeriodoAcademico().val(),
                pGD_Plan_Area_Id_Plan_Area: base.Control.SlcPlanArea().val(),
                pGD_Asignatura_Id_Asignatura: base.Control.SlcAsignatura().val(),
            };
            base.Control.GrdResultado.Load(base.Configurations.search.parameters);
        },
        BtnAgregarClick: function () {
            base.Control.FormularioRegistro.Mostrar();
        },
        BtnGridEditarClick: function (row, data) {
            base.Control.FormularioRegistro.Mostrar(data.ID_PLANASIGNATURA);
        },
        BtnGridEliminarClick: function (row, data) {
            base.Control.Mensaje.Confirmation({
                title: GDirectiva.Presentacion.Base.MensajeResource.ConfirmacionEliminacion,
                message: GDirectiva.Presentacion.Base.MensajeResource.TextoEliminacion,
                onAccept: function () {
                    base.Ajax.AjaxEliminar.send({ pId_Plan_Asignatura: data.ID_PLANASIGNATURA })
                }
            });
        },
        AjaxEliminarSuccess: function (data) {
            if (data.IsProcess == true) {
                base.Control.Mensaje.Information({ message: GDirectiva.Presentacion.Base.MensajeResource.SeEliminoRegistro });
                base.Control.GrdResultado.Load(base.Configurations.search.parameters);
            } else {
                base.Control.Mensaje.Error({ message: data.Message });
            }
        },
        SlcPeriodoAcademicoChange: function () {
            vId_Periodo = base.Control.SlcPeriodoAcademico().val() == "" ? 0 : base.Control.SlcPeriodoAcademico().val();
            base.Ajax.AjaxBuscarPlanArea.data = {
                pId_Periodo: vId_Periodo
            };
            base.Ajax.AjaxBuscarPlanArea.submit();
        },
        AjaxBuscarPlanAreaSuccess: function (data) {
            base.Control.SlcPlanArea().empty();
            base.Control.SlcAsignatura().empty();
            base.Control.SlcPlanArea().append($('<option>', {
                value: '0',
                text: '--SELECCIONE--'
            }));
            base.Control.SlcAsignatura().append($('<option>', {
                value: '0',
                text: '--SELECCIONE--'
            }));
            if (data.Result != null) {
                $.each(data.Result, function (i, item) {
                    base.Control.SlcPlanArea().append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            }
        },
        SlcPlanAreaChange: function () {
            vId_PlanArea = base.Control.SlcPlanArea().val() == "" ? 0 : base.Control.SlcPlanArea().val();
            base.Ajax.AjaxBuscarAsignatura.data = {
                pId_PlanArea: vId_PlanArea
            };
            base.Ajax.AjaxBuscarAsignatura.submit();
        },
        AjaxBuscarAsignaturaSuccess: function (data) {
            base.Control.SlcAsignatura().empty();
            base.Control.SlcAsignatura().append($('<option>', {
                value: '0',
                text: '--SELECCIONE--'
            }));
            if (data.Result != null) {
                $.each(data.Result, function (i, item) {
                    base.Control.SlcAsignatura().append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            }
        }
    };

    base.Ajax = {
        AjaxEliminar: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.Eliminar,
            autoSubmit: false,
            onSuccess: base.Event.AjaxEliminarSuccess
        }),
        AjaxBuscarPlanArea: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.BuscarPlanArea,
            autoSubmit: false,
            onSuccess: base.Event.AjaxBuscarPlanAreaSuccess
        }),
        AjaxBuscarAsignatura: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.BuscarAsignatura,
            autoSubmit: false,
            onSuccess: base.Event.AjaxBuscarAsignaturaSuccess
        })
    };

    base.Function = {
        CrearGrid: function () {

            var columns = new Array();
            columns.push({ data: 'NOMBRE_PERIODO', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaPeriodoAcademico });
            columns.push({ data: 'NOMBRE_PLANAREA', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaPlanArea });
            columns.push({ data: 'NOMBRE_ASIGNATURA', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaAsignatura });
            
            var listaOpciones = new Array();
            listaOpciones.push({ type: GDirectiva.Presentacion.Web.Components.GridAction.Calendario, event: { on: 'click', callBack: base.Event.BtnGridEditarClick } });
            listaOpciones.push({ type: GDirectiva.Presentacion.Web.Components.GridAction.DeleteActividades, event: { on: 'click', callBack: base.Event.BtnGridEliminarClick } });

            columns.push({
                data: null,
                title: GDirectiva.Presentacion.Base.GenericResource.GridAcciones,
                'sClass': "text-center controls",
                actionButtons: listaOpciones
            });

            base.Control.GrdResultado = new GDirectiva.Presentacion.Web.Components.Grid({
                renderTo: 'divGrdResult',
                columns: columns,
                proxy: {
                    url: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.Buscar,
                    source: 'Result'
                },
                hasSelectionRows: false
            });
        }
    };
};