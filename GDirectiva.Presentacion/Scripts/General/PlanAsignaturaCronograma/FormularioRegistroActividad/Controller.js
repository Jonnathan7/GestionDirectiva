/// <summar4y>
/// Script de controlador de formulario de registro.
/// </summary>
/// <remarks>
/// Creacion: 	JPC 07/08/2016
/// </remarks>
ns('GDirectiva.Presentacion.General.PlanAsignaturaCronograma.FormularioRegistroActividad');
GDirectiva.Presentacion.General.PlanAsignaturaCronograma.FormularioRegistroActividad.Controller = function (opts) {
    var base = this;

    base.Ini = function () {
        base.Event.GrabarSuccess = (opts.GrabarSuccess != null && opts.GrabarSuccess) ? opts.GrabarSuccess : null;
        base.Control.BtnGrabarActividad().click(base.Event.BtnGrabarActividadClick);
        base.Control.BtnCancelarActividad().click(base.Event.BtnCancelarActividadClick);
        base.Control.ValRegistro = new GDirectiva.Presentacion.Web.Components.Validator({
            form: base.Control.FormRegistro(),
            messages: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource
        });
        base.Control.DatePickerFechaInicio = new GDirectiva.Presentacion.Web.Components.Calendar({
            inputFrom: base.Control.TxtFechaInicio(),
            is12HourFormat: false
        });
        base.Control.DatePickerFechaFin = new GDirectiva.Presentacion.Web.Components.Calendar({
            inputFrom: base.Control.TxtFechaFin(),
            is12HourFormat: false
        });
    };
    base.MostrarActividades = function (pIdPlanAsignatura, pIdActividad) {
        pIdPlanAsignatura = (pIdPlanAsignatura == undefined) ? 0 : pIdPlanAsignatura;
        pIdActividad = (pIdActividad == undefined) ? 0 : pIdActividad;
        base.Control.DlgFormulario.getAjaxContent(
            {
                action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.FormularioRegistroActividad,
                data: { pId_Plan_Asignatura: pIdPlanAsignatura, pId_Actividad: pIdActividad },
                onSuccess: function () {
                    base.Ini();
                }
            }
        );
    };
    base.Control = {
        Mensaje: new GDirectiva.Presentacion.Web.Components.Message(),
        DlgFormulario: new GDirectiva.Presentacion.Web.Components.Dialog({
            title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaTituloFormularioActividad,
            width: '50%',
            maxHeight: 650
        }),

        FormRegistro: function () { return $('#frmPlanAsignaturaCronogramaRegistro'); },
        ValRegistro: null,
        BtnGrabarActividad: function () { return $('#btnFrmGrabarActividad'); },
        BtnCancelarActividad: function () { return $('#btnFrmCancelarActividad'); },
        TxtFechaInicio: function () { return $('#txtFechaInicio'); },
        TxtFechaFin: function () { return $('#txtFechaFin'); },
        TxtNombreActividad: function () { return $('#txtNombreActividad'); },
        TxtPorcentaje: function () { return $('#txtPorcentaje'); },
        SlcResponsables: function () { return $('#slcResponsables'); },
        HdnFormularioRegistroId_Actividad: function () { return $('#hdnFormularioRegistroId_Actividad'); },
        HdnFormularioRegistroPlanAsignatura: function () { return $('#hdnFormularioRegistroPlanAsignatura'); },
        DatePickerFechaInicio: null,
        DatePickerFechaFin: null
    };

    base.Event = {
        BtnGrabarActividadClick: function () {
            if (base.Control.ValRegistro.isValid()) {
                base.Ajax.AjaxRegistrarActividad.data = {
                    Id_Actividad: base.Control.HdnFormularioRegistroId_Actividad().val(),
                    GD_Plan_Asignatura_Id_Plan_Asignatura: base.Control.HdnFormularioRegistroPlanAsignatura().val(),
                    Actividad: base.Control.TxtNombreActividad().val(),
                    Fecha_Inicio: base.Control.TxtFechaInicio().val(),
                    Fecha_Fin: base.Control.TxtFechaFin().val(),
                    Porcentaje_Avance: (base.Control.TxtPorcentaje().val() == '' ? 0 : base.Control.TxtPorcentaje().val()),
                    GRMS_Empleado_Id_Empleado: base.Control.SlcResponsables().val()
                };
                base.Ajax.AjaxRegistrarActividad.submit();
            }
        },
        BtnCancelarActividadClick: function () {
            base.Control.DlgFormulario.close();
        },
        AjaxRegistrarActividadSuccess: function (data) {
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
        }
    };
    base.Ajax = {
        AjaxRegistrarActividad: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.RegistrarActividad,
            autoSubmit: false,
            onSuccess: base.Event.AjaxRegistrarActividadSuccess
        })
    };

    base.Function = {
        MostrarMensaje: function () {

        }
    };
};