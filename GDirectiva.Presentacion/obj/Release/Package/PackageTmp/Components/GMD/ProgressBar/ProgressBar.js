// Copyright (c) 2015.
// All rights reserved.
/// <summary>
/// Libreria control de mensaje de carga
/// </summary>
/// <remarks>
/// Creacion: 	GMD(EMP) 20150107 <br />
/// </remarks>
ns('GDirectiva.Presentacion.Web.Components');
GDirectiva.Presentacion.Web.Components.ProgressBar = function (opts) {
    this.init(opts);
};

GDirectiva.Presentacion.Web.Components.ProgressBar.prototype = {
    idDivProgrees: 'divProgressBar_GMD',
    idDivDialog: 'divProgressDialogBar_GMD',

    init: function (opts) {
        this._privateFunction.createProgressBar.apply(this, [opts]);
    },

    setMaxValue: function (max) {
        if (this.divProgress) {
            this.divProgress.progressbar('option', 'max', max);
        }
    },

    setValue: function (value) {
        if (this.divProgress) {
            this.divProgress.progressbar('option', 'value', value);
        }
    },

    getValue: function () {
        var value = null;
        if (this.divProgress) {
            value = this.divProgress.progressbar('option', 'value');
        }
        return value;
    },

    show: function () {
        if (this.divDialog) {
            this.divDialog.dialog('open');
        }
        else {
            if (this.divProgress) {
                this.divProgress.show();
            }
        }
    },

    hide: function () {
        if (this.divDialog) {
            this.divDialog.dialog('close');
        }
        else {
            if (this.divProgress) {
                this.divProgress.hide();
            }
        }
    },

    destroy: function () {
        if (this.divProgress) {
            this.divProgress.progressbar('destroy');
            this.divProgress.remove();
        }
        if (this.divDialog) {
            this.divDialog.dialog('destroy');
            this.divDialog.remove();
        }
    },

    _privateFunction: {
        createProgressBar: function (opts) {
            if (!opts.targetLoading) {
                this.divDialog = this._privateFunction.implementDialogElement.apply(this, [opts]);
                opts.targetLoading = this.divDialog;
            }
            else {
                opts.targetLoading = $('#' + opts.targetLoading);
            }
            this.divProgress = this._privateFunction.implementProgressElement.apply(this, [opts.targetLoading, opts.maxValue]);
        },
        implementProgressElement: function (targetLoading, maxValue) {
            var divProgress = $('#' + this.idDivProgrees);
            if (divProgress.length == 0) {
                divProgress = $('<div />');
                divProgress.attr('id', this.idDivProgrees);
                divProgress.addClass("progressBar-custom");
                divProgress.append($('<div class="progressBar-custom-label"></div>'));
                //if (Yanbal.PYF.Web.Shared.General.Resources) {
                    divProgress.find('.progressBar-PYF-label').text('...');
                //}
                targetLoading.append(divProgress);
            }
            if (targetLoading) {
                divProgress.css('position', 'relative');
                divProgress.css('top', '0px');
                divProgress.css('left', '0px');
            }
            var me = this;
            var config = {
                value: maxValue ? 0 : false,
                change: function () {
                    if (me.getValue() != false) {
                        divProgress.find('.progressBar-custom-label').text(me.getValue() + "%");
                    }
                }
            };
            if (maxValue) {
                config.max = maxValue;
            }
            divProgress.progressbar(config);
            return divProgress;
        },
        implementDialogElement: function (opts) {
            var div = $('#' + this.idDivDialog);
            if (div.length == 0) {
                div = $('<div />');
                div.attr('id', this.idDivDialog);
                $('body').append(div);
            }
            div.dialog({
                dialogClass: "no-close-dialog DialogProgressbar",
                closeOnEscape: false,
                height: 100,
                width: 220,
                modal: opts.modal ? opts.modal : true,
                resizable: false,
                title: 'Loading...'
            });
            return div;
        }
    }
};