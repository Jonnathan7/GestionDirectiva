/// <summar4y>
/// Script de controlador de formulario de registro.
/// </summary>
/// <remarks>
/// Creacion: 	JPC 07/08/2016
/// </remarks>
ns('GDirectiva.Presentacion.General.PlanAsignatura.FormularioRegistro');
GDirectiva.Presentacion.General.PlanAsignatura.FormularioRegistro.Controller = function (opts) {
    var base = this;

    base.Ini = function () {
        base.Event.GrabarSuccess = (opts.GrabarSuccess != null && opts.GrabarSuccess) ? opts.GrabarSuccess : null;
        base.Control.BtnGrabar().click(base.Event.BtnGrabarClick);
        base.Control.BtnCancelar().click(base.Event.BtnCancelarClick);
        base.Control.SlcPeriodoAcademicoForm().change(base.Event.SlcPeriodoAcademicoFormChange);
        base.Control.SlcPlanAreaForm().change(base.Event.SlcPlanAreaFormChange);
        base.Control.SlcAsignaturaForm().change(base.Event.SlcAsignaturaFormChange);
        base.Control.ValRegistro = new GDirectiva.Presentacion.Web.Components.Validator({
            form: base.Control.FormRegistro(),
            messages: GDirectiva.Presentacion.General.PlanAsignatura.Resource
        });
    };
    base.Mostrar = function (pIdPlanAsignatura) {
        pIdPlanAsignatura = (pIdPlanAsignatura == undefined) ? 0 : pIdPlanAsignatura;
        base.Control.DlgFormulario.getAjaxContent(
            {
                action: GDirectiva.Presentacion.General.PlanAsignatura.Actions.FormularioRegistro,
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
            title: GDirectiva.Presentacion.General.PlanAsignatura.Resource.EtiquetaTituloFormulario,
            width: '50%',
            maxHeight: 650
        }),

        FormRegistro: function () { return $('#frmRegistrarPlanAsignatura'); },
        ValRegistro: null,
        BtnGrabar: function () { return $('#btnFrmGrabar'); },
        BtnCancelar: function () { return $('#btnFrmCancelar'); },
        SlcPeriodoAcademicoForm: function () { return $('#slcPeriodoAcademicoForm'); },
        SlcPlanAreaForm: function () { return $('#slcPlanAreaForm'); },
        SlcAsignaturaForm: function () { return $('#slcAsignaturaForm'); },
        SlcDocenteForm: function () { return $('#slcDocenteForm'); },
        TxtMetas: function () { return $('#txtMetasForm'); },
        TxtMetodologias: function () { return $('#txtMetodologiasForm'); },
        HdnCodigo: function () { return $('#hdnFormularioRegistroCodigo'); },
        HdnEstado: function () { return $('#hdnFormularioRegistroEstado'); }
    };

    base.Event = {
        BtnGrabarClick: function () {
            if (base.Control.ValRegistro.isValid()) {
                base.Ajax.AjaxRegistrar.data = {
                    Id_Plan_Asignatura: base.Control.HdnCodigo().val(),
                    Id_Periodo: base.Control.SlcPeriodoAcademicoForm().val(),
                    GD_Plan_Area_Id_Plan_Area: base.Control.SlcPlanAreaForm().val(),
                    GRMS_Empleado_Id_Empleado: base.Control.SlcDocenteForm().val(),
                    GD_Asignatura_Id_Asignatura: base.Control.SlcAsignaturaForm().val(),
                    Meta: base.Control.TxtMetas().val(),
                    Metodologia: base.Control.TxtMetodologias().val(),
                    Estado: base.Control.HdnEstado().val()
                };
                base.Ajax.AjaxRegistrar.submit();
            }
        },
        BtnCancelarClick: function () {
            base.Control.DlgFormulario.close();
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
        SlcPeriodoAcademicoFormChange: function () {
            vId_Periodo = base.Control.SlcPeriodoAcademicoForm().val() == "" ? 0 : base.Control.SlcPeriodoAcademicoForm().val();
            base.Ajax.AjaxBuscarPlanArea.data = {
                pId_Periodo: vId_Periodo
            };
            base.Ajax.AjaxBuscarPlanArea.submit();
        },
        AjaxBuscarPlanAreaSuccess: function (data) {
            base.Control.SlcPlanAreaForm().empty();
            base.Control.SlcAsignaturaForm().empty();
            base.Control.SlcDocenteForm().empty();
            base.Control.SlcPlanAreaForm().append($('<option>', {
                value: '',
                text: '--SELECCIONE--'
            }));
            base.Control.SlcAsignaturaForm().append($('<option>', {
                value: '',
                text: '--SELECCIONE--'
            }));
            base.Control.SlcDocenteForm().append($('<option>', {
                value: '',
                text: '--SELECCIONE--'
            }));
            if (data.Result != null) {
                $.each(data.Result, function (i, item) {
                    base.Control.SlcPlanAreaForm().append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            }
        },
        SlcPlanAreaFormChange: function () {
            vId_PlanArea = base.Control.SlcPlanAreaForm().val() == "" ? 0 : base.Control.SlcPlanAreaForm().val();
            base.Ajax.AjaxBuscarAsignatura.data = {
                pId_PlanArea: vId_PlanArea
            };
            base.Ajax.AjaxBuscarAsignatura.submit();
        },
        AjaxBuscarAsignaturaSuccess: function (data) {
            base.Control.SlcAsignaturaForm().empty();
            base.Control.SlcDocenteForm().empty();
            base.Control.SlcAsignaturaForm().append($('<option>', {
                value: '',
                text: '--SELECCIONE--'
            }));
            base.Control.SlcDocenteForm().append($('<option>', {
                value: '',
                text: '--SELECCIONE--'
            }));
            if (data.Result != null) {
                $.each(data.Result, function (i, item) {
                    base.Control.SlcAsignaturaForm().append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            }
        },
        SlcAsignaturaFormChange: function () {
            vId_Asignatura = base.Control.SlcAsignaturaForm().val() == "" ? 0 : base.Control.SlcAsignaturaForm().val();
            base.Ajax.AjaxBuscarDocente.data = {
                pId_Asignatura: vId_Asignatura
            };
            base.Ajax.AjaxBuscarDocente.submit();
        },
        AjaxBuscarDocenteSuccess: function (data) {
            base.Control.SlcDocenteForm().empty();
            base.Control.SlcDocenteForm().append($('<option>', {
                value: '',
                text: '--SELECCIONE--'
            }));
            if (data.Result != null) {
                $.each(data.Result, function (i, item) {
                    base.Control.SlcDocenteForm().append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            }
        }
    };
    base.Ajax = {
        AjaxRegistrar: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignatura.Actions.Registrar,
            autoSubmit: false,
            onSuccess: base.Event.AjaxRegistrarSuccess
        }),
        AjaxBuscarPlanArea: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignatura.Actions.BuscarPlanArea,
            autoSubmit: false,
            onSuccess: base.Event.AjaxBuscarPlanAreaSuccess
        }),
        AjaxBuscarAsignatura: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignatura.Actions.BuscarAsignatura,
            autoSubmit: false,
            onSuccess: base.Event.AjaxBuscarAsignaturaSuccess
        }),
        AjaxBuscarDocente: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignatura.Actions.BuscarDocente,
            autoSubmit: false,
            onSuccess: base.Event.AjaxBuscarDocenteSuccess
        })
    };
};