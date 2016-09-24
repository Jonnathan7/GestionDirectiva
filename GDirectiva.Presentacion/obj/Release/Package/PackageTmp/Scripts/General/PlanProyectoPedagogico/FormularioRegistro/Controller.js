/// <summar4y>
/// Script de controlador de formulario de registro.
/// </summary>
/// <remarks>
/// Creacion: 	JPC 30/08/2016
/// </remarks>
ns('GDirectiva.Presentacion.General.PlanProyectoPedagogico.FormularioRegistro');
GDirectiva.Presentacion.General.PlanProyectoPedagogico.FormularioRegistro.Controller = function (opts) {
    var base = this;

    base.Ini = function () {
        
        base.Event.GrabarSuccess = (opts.GrabarSuccess != null && opts.GrabarSuccess) ? opts.GrabarSuccess : null;
        base.Control.BtnGrabar().click(base.Event.BtnGrabarClick);
        base.Control.BtnCancelar().click(base.Event.BtnCancelarClick);
        base.Control.ValRegistro = new GDirectiva.Presentacion.Web.Components.Validator({
            form: base.Control.FormRegistro(),
            messages: GDirectiva.Presentacion.General.PlanProyectoPedagogico.Resource
        });
        base.Control.TxtFile().on('change', function () {
            base.Function.beforeSubmit();
        });
    };
    base.Mostrar = function (pId_Plan_Proyecto_Pedagogico) {
        pId_Plan_Proyecto_Pedagogico = (pId_Plan_Proyecto_Pedagogico == undefined) ? 0 : pId_Plan_Proyecto_Pedagogico;
        base.Control.DlgFormulario.getAjaxContent(
            {
                action: GDirectiva.Presentacion.General.PlanProyectoPedagogico.Actions.FormularioRegistro,
                data: { pId_Plan_Proyecto_Pedagogico: pId_Plan_Proyecto_Pedagogico },
                onSuccess: function () {
                    base.Ini();
                }
            }
        );
    };
    base.Function = {
        beforeSubmit: function () {
            var $ufile = base.Control.TxtFile();
            var value = $ufile.val();
            if (value != "") {
                if (!(/\.(doc|docx)$/i.test(value))) {
                    base.Control.NombreArchivo = null;
                    base.Control.ContenidoArchivo = null;

                    alert(GDirectiva.Presentacion.General.PlanProyectoPedagogico.Resource.ErrorDeExtension);
                    base.Control.TxtFile().val('');
                    
                    return false;
                } else {
                    var txtFile = document.getElementById("txtFile");
                    var txt = "";
                    if ('files' in txtFile) {
                        var file = txtFile.files[0];
                        base.Control.NombreArchivo = file.name;
                    }

                    if (txtFile.files && txtFile.files[0]) {
                        var fileReader = new FileReader();
                        fileReader.onload = function (e) {
                            base.Control.ContenidoArchivo = e.target.result;
                        };
                        fileReader.readAsDataURL(txtFile.files[0]);
                    }
                }
            } else {
                base.Control.NombreArchivo = null;
                base.Control.ContenidoArchivo = null;
                alert(GDirectiva.Presentacion.General.PlanProyectoPedagogico.Resource.MensajeDebeSeleccionarArchivoWord);
                return false;
            }
        }
    };
    base.Control = {
        Mensaje: new GDirectiva.Presentacion.Web.Components.Message(),
        DlgFormulario: new GDirectiva.Presentacion.Web.Components.Dialog({
            title: GDirectiva.Presentacion.General.PlanProyectoPedagogico.Resource.EtiquetaTituloFormulario,
            width: '50%',
            maxHeight: 800
        }),
        FormRegistro: function () { return $('#frmRegistrarPlanProyectoPedagogico'); },
        ValRegistro: null,
        BtnGrabar: function () { return $('#btnFrmGrabar'); },
        BtnCancelar: function () { return $('#btnFrmCancelar'); },
        SlcPeriodoAcademicoForm: function () { return $('#slcPeriodoAcademicoForm'); },
        SlcGradoForm: function () { return $('#slcGradoForm'); },
        SlcAreaForm: function () { return $('#slcAreaForm'); },
        TxtNombreForm: function () { return $('#txtNombreForm'); },
        TxtObjetivosForm: function () { return $('#txtObjetivosForm'); },
        TxtDescripcionForm: function () { return $('#txtDescripcionForm'); },
        TxtAnioPlanEstudioForm: function () { return $('#txtAnioPlanEstudioForm'); },
        TxtDocumentoPlanEstudioForm: function () { return $('#txtDocumentoPlanEstudioForm'); },
        HdnFormularioCodigoPlanEstudio: function () { return $('#hdnFormularioCodigoPlanEstudio'); },
        HdnCodigo: function () { return $('#hdnFormularioRegistroCodigo'); },
        TxtFile: function () { return $('#txtFile'); },
        NombreArchivo: null,
        ContenidoArchivo: null,
        ajaxform: function () { return $('#ajaxform_Archivo'); }
    };

    base.Event = {
        BtnGrabarClick: function () {
            if (base.Control.ValRegistro.isValid()) {
                
                base.Ajax.AjaxRegistrar.data = {
                    planProyectoPedagogico: {
                        Id_ProyectoPedagogico: base.Control.HdnCodigo().val(),
                        Nombre: base.Control.TxtNombreForm().val(),
                        Objetivos: base.Control.TxtObjetivosForm().val(),
                        Descripcion: base.Control.TxtDescripcionForm().val(),
                        Id_PeriodoAcademico: base.Control.SlcPeriodoAcademicoForm().val(),
                        Id_PlanEstudio: base.Control.HdnFormularioCodigoPlanEstudio().val(),
                        Id_Grado: base.Control.SlcGradoForm().val(),
                        Id_Area: base.Control.SlcAreaForm().val(),
                        Documento: base.Control.NombreArchivo
                    },
                    contenidoArchivo: base.Control.ContenidoArchivo
                };
                base.Ajax.AjaxRegistrar.submit();
            }
            /*if (base.Control.TxtFile().val() != "") {
                base.Control.ajaxform().submit();
            } else {
                alert(GDirectiva.Presentacion.General.PlanArea.Resource.MensajeDebeSeleccionarArchivoWord);
            }*/
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
            action: GDirectiva.Presentacion.General.PlanProyectoPedagogico.Actions.Registrar,
            autoSubmit: false,
            onSuccess: base.Event.AjaxRegistrarSuccess
        })
    };
};