/// Copyright (c) 2015.
/// All rights reserved.
/// <summary>
/// Controlador de progreso de carga o peticiones
/// </summary>
/// <remarks>
/// Creacion: 	GMD(CERS) 20150107 <br />
/// </remarks>
ns('GDirectiva.Presentacion.Web.Components.Dialog');
GDirectiva.Presentacion.Web.Components.Dialog = function (opts) {
    //$.extend( {
    //    show: true,
    //    hide: true,
    //    width: '50%',
    //    minWidth : 320,
    //    maxWidth: 1024
    //},opts);
    this.init(opts);
};

GDirectiva.Presentacion.Web.Components.Dialog.prototype = {
    idDivDialog: 'divDialog_GMD',
    noCloseButtonClass: 'no-close-dialog',
    init: function (opts) {
        opts.show = opts.show == undefined ? true : opts.show;
        opts.hide = opts.hide == undefined ? true : opts.hide;
        opts.width = opts.width == undefined ? '50%' : opts.width;
        opts.maxHeight = opts.maxHeight == undefined ? undefined : opts.maxHeight;
        opts.height = opts.height == undefined ? undefined : opts.height;
        opts.minWidth = opts.minWidth == undefined ? 320 : opts.minWidth;
        opts.maxWidth = opts.maxWidth == undefined ? 1024 : opts.maxWidth;

        this._privateFunction.createDialog.apply(this, [opts]);
        if (opts != null && opts.onClose != null && opts.onClose)
            this.onClose = opts.onClose;
        if (opts != null && opts.onDestroy != null && opts.onDestroy)
            this.onDestroy = opts.onDestroy;
    },

    setCloseOnEscape: function (value) {
        if (this.divDialog) {
            this.divDialog.progressbar('option', 'closeOnEscape', value);
        }
    },
    setCloseText: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'closeText', value);
        }
    },
    setDraggable: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'draggable', value);
        }
    },
    setHeight: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'height', value);
        }
    },
    setWidth: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'width', value);
        }
    },
    setMaxHeight: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'maxHeight', value);
        }
    },
    setMaxWidth: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'maxWidth', value);
        }
    },
    setMinHeight: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'minHeight', value);
        }
    },
    setMinWidth: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'minWidth', value);
        }
    },
    setModal: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'modal', value);
        }
    },
    setPosition: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'position', value);
        }
    },
    setResizable: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'resizable', value);
        }
    },
    setTitle: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'title', value);
        }
    },
    setClass: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'dialogClass', value);
        }
    },
    setButtons: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'buttons', value);
        }
    },

    getMainContainer: function () {
        return this.divDialog;
    },
    getHeight: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'height');
        }
        return value;
    },
    getWidth: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'width');
        }
        return value;
    },
    getPosition: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'position');
        }
        return value;
    },
    getTitle: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'title');
        }
        return value;
    },
    isModal: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'modal');
        }
        return value;
    },
    isResizable: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'resizable');
        }
        return value;
    },
    isOpen: function (value) {
        $.datepicker._hideDatepicker();
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('isOpen');
        }
        return value;
    },

    close: function () {
        if (this.divDialog && this.isOpen()) {
            this.divDialog.dialog('close');
            if (this.onClose != null)
                this.onClose();
        }
    },
    open: function () {
        if (this.divDialog && !this.isOpen()) {
            if (navigator.userAgent.indexOf('Chrome') != -1) {
                $(window).scrollTop(0);
                $("body").scrollTop(0);
            }
            this.divDialog.dialog('open');
            $("body").css("overflow", "hidden");
            $('.ui-dialog-titlebar-close').prop('title', '');
            $('.ui-dialog').css('overflow', 'visible');
        }
    },
    moveToTop: function () {
        if (this.divDialog) {
            this.divDialog.dialog('moveToTop');
        }
    },

    setContent: function (html) {
        if (this.divDialog) {
            this.divDialog.html(html);
        }
    },

    getAjaxContent: function (opts) {
        var me = this;
        var ajaxBuscar = new GDirectiva.Presentacion.Web.Components.Ajax({
            action: opts.action,
            dataType: 'html',
            data: opts.data,
            method: opts.method,
            onSuccess: function (html) {
                me.setContent(html);
                GDirectiva.Presentacion.Web.Components.TextBox.Function.Configure(me.idDivDialog);
                if (opts.onSuccess) {
                    opts.onSuccess.apply(opts.scope ? opts.scope : me, [html, opts.customParam]);
                }
                opts.autoOpen = (opts.autoOpen == undefined || opts.autoOpen == null) ? true : opts.autoOpen;
                if (opts.autoOpen) {
                    me.open();
                    if (opts.afterOpen) {
                        opts.afterOpen.apply(opts.scope ? opts.scope : me, [html, opts.customParam]);
                    }
                }
                if (GDirectiva.Presentacion.Web.Components.Util.RemoveDrop) {
                    GDirectiva.Presentacion.Web.Components.Util.RemoveDrop();
                }
            }
        });
    },

    destroy: function () {
        if (this.divDialog) {
            this.divDialog.dialog('destroy');
            if (this.onDestroy != null)
                this.onDestroy();
            this.divDialog.remove();
        }
    },

    _privateFunction: {
        createDialog: function (opts) {
            if (!opts.target || opts.target == '') {
                this.divDialog = this._privateFunction.implementDialogElement.apply(this, [opts]);
            }
            else {
                this.divDialog = $('#' + opts.target);
            }
            this.divDialog.dialog(this._privateFunction.getConfig.apply(this, [opts]));
            if (opts.maxHeight != undefined || opts.height != undefined) {
                this.divDialog.css('overflow-x', 'hidden');
            }

        },
        implementDialogElement: function (opts) {
            var div = $('<div />').uniqueId();
            this.idDivDialog = this.idDivDialog + div.attr('id');
            div.attr('id', this.idDivDialog);
            $('body').append(div);
            return div;
        },
        getConfig: function (opts) {

            var config = {
                autoOpen: opts.autoOpen ? opts.autoOpen : false,
                modal: opts.modal ? opts.modal : true,
                resizable: opts.resizable ? opts.resizable : false,
                show: { effect: "clip", duration: 400 },
                hide: { effect: "clip", duration: 50 },
                close: function () { $("body").css("overflow", "auto"); }
            };

            if (opts) {
                for (var property in opts) {
                    config[property] = opts[property];
                }
                if (opts.showCloseButton == false) {
                    config.dialogClass = config.dialogClass ? config.dialogClass + ' ' + this.noCloseButtonClass : this.noCloseButtonClass;
                }
                if (opts.close && opts.close != null) {
                    config.close = function () {
                        $("body").css("overflow", "auto");
                        opts.close();
                    };
                }
            }
            return config;
        }
    }
};
