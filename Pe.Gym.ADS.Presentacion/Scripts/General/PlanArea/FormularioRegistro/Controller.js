/// <summar4y>
/// Script de controlador de formulario de registro.
/// </summary>
/// <remarks>
/// Creacion: 	JPC 30/08/2016
/// </remarks>
ns('GDirectiva.Presentacion.General.PlanArea.FormularioRegistro');
GDirectiva.Presentacion.General.PlanArea.FormularioRegistro.Controller = function (opts) {
    var base = this;

    base.Ini = function () {
        base.Event.GrabarSuccess = (opts.GrabarSuccess != null && opts.GrabarSuccess) ? opts.GrabarSuccess : null;
        base.Control.BtnGrabar().click(base.Event.BtnGrabarClick);
        base.Control.BtnCancelar().click(base.Event.BtnCancelarClick);
        base.Control.ValRegistro = new GDirectiva.Presentacion.Web.Components.Validator({
            form: base.Control.FormRegistro(),
            messages: GDirectiva.Presentacion.General.PlanArea.Resource
        });
    };
    base.Mostrar = function (pIdPlanArea) {
        pIdPlanArea = (pIdPlanArea == undefined) ? 0 : pIdPlanArea;
        base.Control.DlgFormulario.getAjaxContent(
            {
                action: GDirectiva.Presentacion.General.PlanArea.Actions.FormularioRegistro,
                data: { pId_Plan_Area: pIdPlanArea },
                onSuccess: function () {
                    base.Ini();
                }
            }
        );
    };
    base.Control = {
        Mensaje: new GDirectiva.Presentacion.Web.Components.Message(),
        DlgFormulario: new GDirectiva.Presentacion.Web.Components.Dialog({
            title: GDirectiva.Presentacion.General.PlanArea.Resource.EtiquetaTituloFormulario,
            width: '50%',
            maxHeight: 800
        }),
        FormRegistro: function () { return $('#frmRegistrarPlanArea'); },
        ValRegistro: null,
        BtnGrabar: function () { return $('#btnFrmGrabar'); },
        BtnCancelar: function () { return $('#btnFrmCancelar'); },
        SlcPeriodoAcademicoForm: function () { return $('#slcPeriodoAcademicoForm'); },
        SlcGradoForm: function () { return $('#slcGradoForm'); },
        SlcAreaForm: function () { return $('#slcAreaForm'); },
        TxtNombreForm: function () { return $('#txtNombreForm'); },
        TxtObjetivosForm: function () { return $('#txtObjetivosForm'); },
        TxtCriteriosForm: function () { return $('#txtCriteriosForm'); },
        TxtRequisitosForm: function () { return $('#txtRequisitosForm'); },
        TxtAnioPlanEstudioForm: function () { return $('#txtAnioPlanEstudioForm'); },
        TxtDocumentoPlanEstudioForm: function () { return $('#txtDocumentoPlanEstudioForm'); },
        HdnFormularioCodigoPlanEstudio: function () { return $('#hdnFormularioCodigoPlanEstudio'); },
        HdnCodigo: function () { return $('#hdnFormularioRegistroCodigo'); }
    };

    base.Event = {
        BtnGrabarClick: function () {
            if (base.Control.ValRegistro.isValid()) {
                base.Ajax.AjaxRegistrar.data = {
                    Id_PlanArea: base.Control.HdnCodigo().val(),
                    Nombre: base.Control.TxtNombreForm().val(),
                    Objetivos: base.Control.TxtObjetivosForm().val(),
                    Criterios: base.Control.TxtCriteriosForm().val(),
                    Requisitos: base.Control.TxtRequisitosForm().val(),
                    Id_PeriodoAcademico: base.Control.SlcPeriodoAcademicoForm().val(),
                    Id_PlanEstudio: base.Control.HdnFormularioCodigoPlanEstudio().val(),
                    Id_Grado: base.Control.SlcGradoForm().val(),
                    Id_Area: base.Control.SlcAreaForm().val()
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
        }
    };
    base.Ajax = {
        AjaxRegistrar: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanArea.Actions.Registrar,
            autoSubmit: false,
            onSuccess: base.Event.AjaxRegistrarSuccess
        })
    };
};