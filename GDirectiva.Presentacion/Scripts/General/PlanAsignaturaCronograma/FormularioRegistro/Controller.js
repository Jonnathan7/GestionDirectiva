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

        base.Control.FechaInicio = GDirectiva.Presentacion.Web.Components.Util.ConvertirCadenaAFecha(base.Control.HdnFechaInicio().val());
        base.Control.FechaFin = GDirectiva.Presentacion.Web.Components.Util.ConvertirCadenaAFecha(base.Control.HdnFechaFin().val());

        base.Control.btnAgregarCronograma().on('click', base.Event.BtnAgregarCronogramaClick);
        base.Function.CrearGrid();

        base.Ajax.AjaxBuscarEmpleado.data = { idCurso: base.Control.HdnCodigoCurso().val() };
        base.Ajax.AjaxBuscarEmpleado.submit();
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
        ListaEmpleado: null,
        ListaMeta: null,
        UltimoRegistro: -1,

        FormRegistro: function () { return $('#frmRegistrarPlanAsignatura'); },
        ValRegistro: null,
        btnGrabarCronograma: function () { return $('#btnFrmGrabarCronograma'); },
        btnCancelarCronograma: function () { return $('#btnFrmCancelarCronograma'); },
        btnAgregarCronograma: function () { return $('#btnAgregarCronograma'); },
        HdnCodigo: function () { return $('#hdnFormularioRegistroCodigo'); },
        HdnCodigoCurso: function () { return $('#hdnFormularioRegistroCodigoCurso'); },
        HdnFechaInicio: function () { return $('#hdnFormularioRegistroFechaInicio'); },
        HdnFechaFin: function () { return $('#hdnFormularioRegistroFechaFin'); },
        FechaInicio: null,
        FechaFin: null,
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
            var listaActividad = $('input[name=Actividad]');
            var listaFechaInicio = $('input[name=FechaInicio]');
            var listaFechaFin = $('input[name=FechaFin]');
            var listaPorcentaje = $('input[name=Procentaje]');
            var listaEmpleado = $('select[name=Id_Empleado]');
            var listaMeta = $('select[name=Id_PlanAsignaturaMeta]');

            var listaGrabar = new Array();

            for (var i = 0; i < listaActividad.length ; i++){
                listaGrabar.push({
                    ID_ACTIVIDADPLANASIGNATURA: listaActividad[i].attributes[3].value,
                    ACTIVIDAD: listaActividad[i].value,
                    FECHAINICIO: listaFechaInicio[i].value,
                    FECHAFIN: listaFechaFin[i].value,
                    PORCENTAJE: listaPorcentaje[i].value,
                    ID_EMPLEADO: listaEmpleado[i].value,
                    ID_PLANASIGNATURAMETA: listaMeta[i].value
                });
            }

            base.Ajax.AjaxRegistrar.data = {
                Id_Plan_Asignatura: base.Control.HdnCodigo().val(),
                request: listaGrabar
            };
            base.Ajax.AjaxRegistrar.submit();
        },
        BtnCancelarClick: function () {
            base.Control.DlgFormulario.close();
        },
        BtnGridEliminarActividadClick: function (row, data) {
            base.Control.GrdResultado.core.row(row[0]).remove().draw( false )
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
        AjaxBuscarEmpleadoSuccess: function (data) {
            base.Control.ListaEmpleado = data.Result;
            base.Ajax.AjaxBuscarMeta.data = { pId_PlanAsignatura: base.Control.HdnCodigo().val() };
            base.Ajax.AjaxBuscarMeta.submit();
        },
        AjaxBuscarMetaSuccess: function (data) {
            base.Control.ListaMeta = data.Result;

            base.Event.BtnBuscarClick();
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
            var resultado = true;

            var listaActividad = $('input[name=Actividad]');
            var listaFechaInicio = $('input[name=FechaInicio]');
            var listaFechaFin = $('input[name=FechaFin]');
            var listaPorcentaje = $('input[name=Procentaje]');
            var listaEmpleado = $('select[name=Id_Empleado]');
            var listaMeta = $('select[name=Id_PlanAsignaturaMeta]');

            var listaGrabar = new Array();

            for (var i = 0; i < listaActividad.length ; i++) {
                if(listaActividad[i].value == null || listaActividad[i].value == ""
                || listaFechaInicio[i].value == null || listaFechaInicio[i].value == ""     
                || listaFechaFin[i].value == null || listaFechaFin[i].value == ""
                || listaPorcentaje[i].value == null || listaPorcentaje[i].value == ""
                || listaEmpleado[i].value == null || listaEmpleado[i].value == "0"
                || listaMeta[i].value == null || listaMeta[i].value == "0"
                ){
                    resultado = false;
                    return false;
                }
            }

            if (resultado == true) {
                base.Control.GrdResultado.core.row.add(
                    {
                        ID_ACTIVIDADPLANASIGNATURA: 0,
                        ACTIVIDAD: '',
                        FECHAINICIO: '',
                        FECHAFIN: '',
                        PORCENTAJE: '',
                        ID_EMPLEADO: 0
                    }
                ).draw(true);
                base.Function.FormatearInput();
            }
        }
    };
    base.Ajax = {
        AjaxBuscarEmpleado: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.ListarEmpleado,
            autoSubmit: false,
            onSuccess: base.Event.AjaxBuscarEmpleadoSuccess
        }),
        AjaxBuscarMeta: new GDirectiva.Presentacion.Web.Components.Ajax(
        {
            action: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Actions.ListarMeta,
            autoSubmit: false,
            onSuccess: base.Event.AjaxBuscarMetaSuccess
        }),
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
        FormatearInput: function () {
            GDirectiva.Presentacion.Web.Components.TextBox.Function.Configure("divGrdResultActividades");

            var listaFechaInicio = $('input[name=FechaInicio]');
            var listaFechaFin = $('input[name=FechaFin]');

            for (var i = 0; i < listaFechaInicio.length ; i++) {
                var configInicio = {
                    dateFormat: GDirectiva.Presentacion.Web.Formato.Fecha,
                    minDate: base.Control.FechaInicio,
                    maxDate: base.Control.FechaFin,
                    onClose: function (selected, e) {
                        var result = GDirectiva.Presentacion.Web.Components.Util.ValidateBlurOnlyDate(this);
                        var inputFin = $('#' + e.id.replace("Inicio", "Fin"));
                        var dateFin = GDirectiva.Presentacion.Web.Components.Util.ConvertirCadenaAFecha(inputFin.val());
                        var dateSeleccionado = GDirectiva.Presentacion.Web.Components.Util.ConvertirCadenaAFecha(selected);

                        if (inputFin && inputFin != null) {
                            if (result) {
                                inputFin.datepicker('option', 'minDate', selected);
                            }
                            else {
                                inputFin.datepicker('option', 'minDate', null);
                            }
                        }

                        if (dateSeleccionado >= dateFin) {
                            inputFin.val(selected);
                        }
                    }
                };

                $('#' + listaFechaInicio[i].id).datepicker(configInicio);

                var configFin = {
                    dateFormat: GDirectiva.Presentacion.Web.Formato.Fecha,
                    minDate: base.Control.FechaInicio,
                    maxDate: base.Control.FechaFin,
                    onClose: function (selected, e) {
                        var result = GDirectiva.Presentacion.Web.Components.Util.ValidateBlurOnlyDate(this);
                        var inputInicio = $('#' + e.id.replace("Fin", "Inicio"));
                        var dateInicio = GDirectiva.Presentacion.Web.Components.Util.ConvertirCadenaAFecha(inputInicio.val());
                        var dateSeleccionado = GDirectiva.Presentacion.Web.Components.Util.ConvertirCadenaAFecha(selected);

                        if (inputInicio && inputInicio != null) {
                            if (result) {
                                inputInicio.datepicker('option', 'maxDate', selected);
                            }
                            else {
                                inputInicio.datepicker('option', 'maxDate', null);
                            }
                        }

                        if (dateSeleccionado <= dateInicio) {
                            inputInicio.val(selected);
                        }
                    }
                };

                $('#' + listaFechaFin[i].id).datepicker(configFin);
            }
        },
        CrearGrid: function () {
            var columns = new Array();
            columns.push({
                data: 'ID_PLANASIGNATURAMETA', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaNombreActividad, 'mRender': function (data, type, full) {
                    base.Control.UltimoRegistro++;
                    
                    var opcion = '';

                    for (var i = 0; i < base.Control.ListaMeta.length; i++) {
                        var meta = base.Control.ListaMeta[i];
                        opcion += '<option value="' + meta.Id_PlanAsignaturaMeta + '"' + ((meta.Id_PlanAsignaturaMeta == full.ID_PLANASIGNATURAMETA) ? 'selected' : '') + '>' + meta.Meta + '</option>';
                    }

                    var resultado = '<select class="form-control" name="Id_PlanAsignaturaMeta" id_actividadplanasignatura="' + full.ID_ACTIVIDADPLANASIGNATURA + '>'
                           + '<option value="0">--SELECCIONE--</option> <option value="0">--SELECCIONE--</option>'
                           + opcion
                           + '</select>';

                    return resultado;
                }
            });
            columns.push({
                    data: 'ACTIVIDAD', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaNombreActividad, 'mRender': function (data, type, full) {
                    return '<input class="form-control" type="text" id_actividadplanasignatura="' + full.ID_ACTIVIDADPLANASIGNATURA + '" name="Actividad" value="' + full.ACTIVIDAD + '">';
                }
            });
            columns.push({
                data: 'FECHAINICIO', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaFechaInicio, 'mRender': function (data, type, full) {
                    var resultado = '<input  size="6" id="fechaInicio_' + base.Control.UltimoRegistro + '" class="form-control" type="text" id_actividadplanasignatura="' + full.ID_ACTIVIDADPLANASIGNATURA + '" name="FechaInicio" value="' + GDirectiva.Presentacion.Web.Components.Util.ConvertirFechaACadena(full.FECHAINICIO) + '">';
                    return resultado;
                }
            });
            columns.push({
                data: 'FECHAFIN', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaFechaFin, 'mRender': function (data, type, full) {
                    var resultado = '<input size="6" id="fechaFin_' + base.Control.UltimoRegistro + '" class="form-control" type="text" id_actividadplanasignatura="' + full.ID_ACTIVIDADPLANASIGNATURA + '" name="FechaFin" value="' + GDirectiva.Presentacion.Web.Components.Util.ConvertirFechaACadena(full.FECHAFIN) + '">';

                    return resultado;
                }
            });
            columns.push({
                data: 'PORCENTAJE', title: GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Resource.EtiquetaPorcentaje+('(%)'), 'mRender': function (data, type, full) {
                    return '<input size="3" class="form-control" type="text" Mask="integer" id_actividadplanasignatura="' + full.ID_ACTIVIDADPLANASIGNATURA + '" name="Procentaje" value="' + full.PORCENTAJE + '" maxlength="3" maxValue="100">';
                }
            });
            columns.push({
                data: 'PORCENTAJE', title: 'Estado',"sClass": "center", 'mRender': function (data, type, full) {
                    var resultado = '';

                    var dateString = GDirectiva.Presentacion.Web.Components.Util.ConvertirFechaACadena(full.FECHAFIN);

                    if (dateString != null && dateString != "") {
                        var dateObject = GDirectiva.Presentacion.Web.Components.Util.ConvertirCadenaAFecha(dateString);

                        if (dateObject < new Date()) {
                            if (full.PORCENTAJE == null || full.PORCENTAJE == "" || parseInt(full.PORCENTAJE) < 100) {
                                resultado = '<div style="width:100%;" align="center"><div id="circleRed" align="center" ></div></div>';
                            } else {
                                resultado = '<div style="width:100%;" align="center"><div id="circleGreen" ></div></div>';
                            }
                        } else {
                            resultado = '<div style="width:100%;" align="center"><div id="circleGreen" align="center"></div></div>';
                        }
                    } else {
                        resultado = '<div style="width:100%;" align="center"><div id="circleGreen" align="center"></div></div>';
                    }

                    return resultado;
                }
            });
            columns.push({
                data: 'ID_EMPLEADO', title: 'Empleado', 'mRender': function (data, type, full) {

                    var opcion = '';
                    
                    for (var i = 0; i < base.Control.ListaEmpleado.length; i++) {
                        var empleado = base.Control.ListaEmpleado[i];
                        opcion += '<option value="' + empleado.ID_EMPLEADO + '"' + ((empleado.ID_EMPLEADO == full.ID_EMPLEADO) ? 'selected' : '')  + '>' + empleado.NOMBRES + '</option>';
                    }

                    var resultado = '<select class="form-control" name="Id_Empleado" id_actividadplanasignatura="' + full.ID_ACTIVIDADPLANASIGNATURA + '>'
                           + '<option value="0">--SELECCIONE--</option> <option value="0">--SELECCIONE--</option>'
                           + opcion
                           + '</select>';

                    base.Function.FormatearInput();
                    return resultado;
                }
            });

            var listaOpciones = new Array();
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
                hasSelectionRows: false,
                hasPaging: false,
                hasPagingServer: false
            });
        }
    };
};