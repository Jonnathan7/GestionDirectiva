/// <summary>
/// Script de controlador de Plan de Asignatura.
/// </summary>
/// <remarks>
/// Creacion: 	JPC 30/08/2016
/// </remarks>
ns('GDirectiva.Presentacion.General.PlanArea.Index');


GDirectiva.Presentacion.General.PlanArea.Index.Controller = function () {
    var base = this;

    base.Ini = function () {
        base.Control.FormularioRegistro = new GDirectiva.Presentacion.General.PlanArea.FormularioRegistro.Controller({
            GrabarSuccess: function () {
                base.Control.GrdResultado.Load(base.Configurations.search.parameters);
            }
        });
        base.Control.BtnAgregar().on('click', base.Event.BtnAgregarClick);
        base.Control.BtnBuscar().on('click', base.Event.BtnBuscarClick);

        base.Function.CrearGrid();
        base.Control.BtnBuscar().click();
    };


    base.Control = {
        Mensaje: new GDirectiva.Presentacion.Web.Components.Message(),
        GrdResultado: null,
        ValBusqueda: null,
        FrmBusqueda: function () { return $('#frmPlanAreaBusqueda'); },
        SlcPeriodoAcademico: function () { return $('#slcPeriodoAcademico'); },
        SlcGrado: function () { return $('#slcGrado'); },
        SlcArea: function () { return $('#slcArea'); },
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
                periodoacademicoId: base.Control.SlcPeriodoAcademico().val(),
                gradoId: base.Control.SlcGrado().val(),
                areaId: base.Control.SlcArea().val()
            };
            base.Control.GrdResultado.Load(base.Configurations.search.parameters);
        },
        BtnAgregarClick: function () {
            base.Control.FormularioRegistro.Mostrar();
        },
        BtnGridEditarClick: function (row, data) {
            base.Control.FormularioRegistro.Mostrar(data.ID_PLANAREA);
        },
        BtnGridEliminarClick: function (row, data) {
            base.Control.Mensaje.Confirmation({
                title: GDirectiva.Presentacion.Base.MensajeResource.ConfirmacionEliminacion,
                message: GDirectiva.Presentacion.Base.MensajeResource.TextoEliminacion,
                onAccept: function () {
                    base.Ajax.AjaxEliminar.send({ pId_PlanArea: data.ID_PLANAREA })
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
        }
    };

    base.Ajax = {
        AjaxEliminar: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanArea.Actions.Eliminar,
            autoSubmit: false,
            onSuccess: base.Event.AjaxEliminarSuccess
        })
    };

    base.Function = {
        
        ActivarEliminar: function (data, type, full) {
            return full.ID_ESTADO;
        },
        CrearGrid: function () {

            var columns = new Array();
            columns.push({ data: 'PERIODO', title: GDirectiva.Presentacion.General.PlanArea.Resource.EtiquetaPeriodoAcademico });
            columns.push({ data: 'NOMBRE_GRADO', title: GDirectiva.Presentacion.General.PlanArea.Resource.EtiquetaGrado });
            columns.push({ data: 'NOMBRE_AREA', title: GDirectiva.Presentacion.General.PlanArea.Resource.EtiquetaArea });
            columns.push({
                data: null, title: GDirectiva.Presentacion.General.PlanArea.Resource.EtiquetaNombrePlanArea, 'mRender': function (data, type, full) {
                    return full.NOMBRE_PLANAREA;
                }
            });
            columns.push({ data: 'ESTADO', title: GDirectiva.Presentacion.General.PlanArea.Resource.EtiquetaEstado });
            var listaOpciones = new Array();
            listaOpciones.push({ type: GDirectiva.Presentacion.Web.Components.GridAction.Edit, validateRender: base.Function.ActivarEditar, event: { on: 'click', callBack: base.Event.BtnGridEditarClick } });
            listaOpciones.push({ type: GDirectiva.Presentacion.Web.Components.GridAction.Delete, validateRender: base.Function.ActivarEliminar, event: { on: 'click', callBack: base.Event.BtnGridEliminarClick } });

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
                    url: GDirectiva.Presentacion.General.PlanArea.Actions.Buscar,
                    source: 'Result'
                },
                hasSelectionRows: false
            });
        }
    };
};