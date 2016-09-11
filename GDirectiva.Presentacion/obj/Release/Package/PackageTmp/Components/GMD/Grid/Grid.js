/// Copyright (c) 2015.
/// All rights reserved.
/// <summary>
/// Libreria para la creación de grillas
/// </summary>
/// <remarks>
/// Creacion: 	GMD(JLRR) 20150107 <br />
/// </remarks>
ns('GDirectiva.Presentacion.Web.Components.Grid');
GDirectiva.Presentacion.Web.Components.Grid.Url = null;
GDirectiva.Presentacion.Web.Components.Grid.Data = null;
GDirectiva.Presentacion.Web.Components.Grid.Source = null;
GDirectiva.Presentacion.Web.Components.Grid = function (opts) {
    this.Init(opts);
};

GDirectiva.Presentacion.Web.Components.Grid.prototype = {
    id: null,
    renderTo: null,
    columns: null,
    columnDefs: null,
    hasPaging: null,
    hasPagingServer: null,
    hasSelectionRows: null,
    height: null,
    proxy: null,
    core: null,
    table: null,
    events: null,
    scrollY: null,
    scrollCollapse: null,
    data: null,
    dataPagination: null,
    Init: function (opts) {

        this.renderTo = $('#' + opts.renderTo);
        if (this.renderTo && this.renderTo.length == 0) {
            alert('ERROR: Not renderTo defined');
            return;
        }

        this.columns = opts.columns && opts.columns != null ? opts.columns : null;
        this.columnDefs = opts.columnDefs && opts.columnDefs != null ? opts.columnDefs : null;
        this.proxy = opts.proxy && opts.proxy != null ? opts.proxy : null;
        this.events = opts.events && opts.events != null ? opts.events : null;
        this.hasPaging = opts.hasPaging != null ? opts.hasPaging : true;
        this.hasPagingServer = opts.hasPagingServer != null ? opts.hasPagingServer : true;
        this.height = opts.height != null ? opts.height : null;
        this.scrollY = opts.scrollY != null ? opts.scrollY : false;
        this.scrollCollapse = opts.scrollCollapse != null ? opts.scrollCollapse : false;
        this.hasSelectionRows = opts.hasSelectionRows != null ? opts.hasSelectionRows : true;
        this.data = opts.data != null ? opts.data : new Array();
        if (this.hasPagingServer) {
            this.dataPagination = {
                NumeroPagina: 0,
                RegistrosPagina: 10
            }
        }
        if (this.columns == null) {
            alert('ERROR: Not columms defined');
            return;
        }
        this.id = opts.id && opts.id != null ? opts.id : 'GMD-Grid-';
        this._privateFunction.ImplementTableElement.apply(this, [this.renderTo]);
        this._privateFunction.ProcessSelectionRows.apply(this);
        this._privateFunction.CreateGrid.apply(this, [this.data]);

    },
    Load: function (params) {
        this.proxy.params = params;
        this._privateFunction.CreateGrid.apply(this);
    },
    ExportToExcel: function (name) {
        this.table.tableExport({ type: 'excel', escape: 'false', tableName: "shieldui-grid", documentName: name });

    },
    DestroyGrid: function () {
        if (this.core != null) {
            this.core.destroy();
            this.core = null;
        }
    },
    GetSelectedData: function () {
        var data = this.core.rows('.selected').data();
        var lista = new Array();
        for (var i = 0; i < data.length; i++) {
            lista.push(data[i]);
        }
        return lista;
    },
    _privateFunction: {
        CreateGrid: function () {

            if (this.core != null) {
                this.core.destroy();
            }
            if (this.proxy != null) {
                GDirectiva.Presentacion.Web.Components.Grid.Url = this.proxy.url;
                GDirectiva.Presentacion.Web.Components.Grid.Source = this.proxy.source;
                if (this.proxy.params != null) {
                    if (this.hasPagingServer) {
                        this.proxy.params.NumeroPagina = this.dataPagination.NumeroPagina;
                        this.proxy.params.RegistrosPagina = this.dataPagination.RegistrosPagina;
                    }
                    GDirectiva.Presentacion.Web.Components.Grid.Data = this.proxy.params;
                }
            }
            this.core = this.table.DataTable({
                columns: this.columns,
                autoWidth: true,
                //data: dataSource,                
                paging: this.hasPaging,
                serverSide: this.hasPagingServer,
                ordering: false,
                searching: false,
                columnDefs: this.columnDefs,
                ajax: this._privateFunction.CallProxy,
                //scrollY: this.scrollY,
                //scrollCollapse: this.scrollCollapse,
                columnDefs: this.columnDefs
                //responsive: true
            });
        },
        ImplementTableElement: function (renderTo) {
            renderTo.addClass('table-responsive');

            this.table = $('<table />').uniqueId();
            this.id = this.id + this.table.attr('id');
            this.table.attr('id', this.id);
            this.table.width('100%');
            this.table.addClass('table table-striped table-bordered table-hover text-center text-middle');

            if (this.height != null) {
                this.table.attr('height', this.height);
            }

            renderTo.append(this.table);


            this._privateFunction.ProcessColumns.apply(this);

            if (this.events != null) {
                var me = this;
                $.each(this.events, function (index, event) {
                    if (event.isRowEvent) {
                        me.table.on(event.type, event.selector, function () {
                            var row = me.core.row($(this).parents('tr'))
                            var data = row.data();
                            event.callBack.apply(this, [row, data]);
                        });
                    }
                    else {
                        me.table.on(event.type, event.selector, event.callBack);
                    }
                });
            }
            return this.table;
        },
        ProcessColumns: function () {
            var me = this;
            this.events = this.events || new Array();
            $.each(this.columns, function (index, column) {
                if (column.actionButtons) {

                    $.each(column.actionButtons, function (index, action) {

                        me.events.push({
                            type: action.event.on,
                            selector: action.type.Icon + me.id,
                            callBack: action.event.callBack,
                            isRowEvent: true
                        });

                    });

                    column.mRender = function (data, type, full) {

                        var htmlSource = '';
                        $.each(column.actionButtons, function (index, action) {
                            var renderAction = action.validateRender ? action.validateRender(data, type, full) : true;
                            if (renderAction && (action.depend == null || eval( "data." + action.depend + " == true" ))) {
                                htmlSource += action.type.Source(me.id);
                            }
                        });
                        return htmlSource;
                    };
                };
            });
        },
        ProcessSelectionRows: function () {

            var me = this;

            var idCheckHeader = 'chkHeader-' + this.id;
            var idCheckRow = 'chkRow-' + this.id;

            if (this.hasSelectionRows) {
                this.columns.splice(0, 0, {
                    data: '', title: '<input class="' + idCheckHeader + '" type="checkbox">',
                    'mRender': function (data, type, full) {
                        var html = '';
                        html += '<input class="' + idCheckRow + '" type="checkbox">';
                        return html;
                    }
                });
            }

            this.table.on('click', '.' + idCheckRow, function () {
                var row = $(this).parents('tr');
                var chkHeader = $(me.table.find('.' + idCheckHeader));

                row.toggleClass('selected');
                chkHeader.prop('checked', (me.core.data().length == me.core.rows('.selected').data().length));
            });

            this.table.on('click', '.' + idCheckHeader, function () {
                var chk = $(this);
                var rows = me.table.find('tr');
                rows.removeClass('selected');
                if (chk.is(':checked')) {
                    rows.addClass('selected');
                }
                me.table.find('.' + idCheckRow).prop('checked', chk.is(':checked'));
            });

        },
        CallProxy: function (request, callback, settings) {
            if (GDirectiva.Presentacion.Web.Components.Grid.Data != null) {
                GDirectiva.Presentacion.Web.Components.Grid.Data.NumeroPagina = request.start / request.length;
                GDirectiva.Presentacion.Web.Components.Grid.Data.RegistrosPagina = request.length;
                var ajaxBuscar = new GDirectiva.Presentacion.Web.Components.Ajax({
                    action: GDirectiva.Presentacion.Web.Components.Grid.Url,
                    data: GDirectiva.Presentacion.Web.Components.Grid.Data,
                    onSuccess: function (data) {
                        var records = data[GDirectiva.Presentacion.Web.Components.Grid.Source];
                        callback({ 'data': records, "iTotalRecords": records.length > 0 ? records[0].FilasTotal : "0", "iDisplayLength": records.length > 0 ? records[0].NumeroFila : "0", "iTotalDisplayRecords": records.length > 0 ? records[0].FilasTotal : "0" });
                        //me._privateFunction.CreateGrid.apply(me, [data[me.proxy.source]]);
                    }
                });
            }
        }
    }
};
ns('GDirectiva.Presentacion.Web.Components.GridAction');
GDirectiva.Presentacion.Web.Components.GridAction = {
    Edit: {
        Class: 'edit',
        Icon: '.fa-edit-',
        Source: function (id) {
            var selector = 'fa-edit-' + id;
            return GDirectiva.Presentacion.Web.Components.Util.RenderIcono('edit', 'fa-edit ' + selector, GDirectiva.Presentacion.Base.GenericResource.EtiquetaEditar);

        }
    },
    Delete: {
        Class: 'eliminar',
        Icon: '.fa-trash-',
        Source: function (id) {
            var selector = 'fa-trash-' + id;
            return GDirectiva.Presentacion.Web.Components.Util.RenderIcono('delete', 'fa-trash ' + selector, GDirectiva.Presentacion.Base.GenericResource.EtiquetaEliminar);
        }
    },
    Disable: {
        Class: 'eliminar',
        Icon: '.fa-lock-',
        Source: function (id) {
            var selector = 'fa-lock-' + id;
            return GDirectiva.Presentacion.Web.Components.Util.RenderIcono('delete', 'fa-lock ' + selector, GDirectiva.Presentacion.Base.GenericResource.EtiquetaDesactivar);
        }
    },
    Enabled: {
        Class: 'eliminar',
        Icon: '.fa-unlock-',
        Source: function (id) {
            var selector = 'fa-unlock-' + id;
            return GDirectiva.Presentacion.Web.Components.Util.RenderIcono('delete', 'fa-unlock ' + selector, GDirectiva.Presentacion.Base.GenericResource.EtiquetaActivar);
        }
    },
    Calendario: {
        Class: 'eliminar',
        Icon: '.fa-calendar-',
        Source: function (id) {
            var selector = 'fa-calendar-' + id;
            return GDirectiva.Presentacion.Web.Components.Util.RenderIcono('calendat', 'fa-calendar ' + selector, GDirectiva.Presentacion.Base.GenericResource.EtiquetaCalendario);

        }
    }
};