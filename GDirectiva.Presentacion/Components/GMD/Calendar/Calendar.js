/// <summary>
/// Controlador de progreso de carga o peticiones
/// </summary>
/// <remarks>
/// Creacion: 	EDGAR MELGAREJO 20140707 <br />
/// </remarks>
ns('GDirectiva.Presentacion.Web.Components.Calendar');
GDirectiva.Presentacion.Web.Components.Calendar = function (opts) {
    this.init(opts);
};

GDirectiva.Presentacion.Web.Components.Calendar.prototype = {

    inputFrom: null,
    inputTo: null,
    inputHour: null,
    is12HourFormat: null,
    onSelectedDate: null,

    init: function (opts) {
        this.inputHour = opts.inputHour ? opts.inputHour : null;
        this.inputFrom = opts.inputFrom ? opts.inputFrom : null;
        this.inputTo = opts.inputTo ? opts.inputTo : null;
        this.onSelectedDate = opts.onSelectedDate ? opts.onSelectedDate : null;

        var me = this;
        //this._privateFunction
        if (me.inputHour && me.inputHour != null) {
            me.is12HourFormat = me.is12HourFormat == null ? true : me.is12HourFormat;

            me.inputHour.datetimepicker({
                format: GDirectiva.Presentacion.Web.Formato.Hora,
                pickDate: false,
                pick12HourFormat: me.is12HourFormat
            });

            var zIndexInputHour = parseInt(me.inputHour.zIndex());
            $(".bootstrap-datetimepicker-widget").zIndex(zIndexInputHour + 1);
        }

        if (this.inputFrom && this.inputFrom != null) {
            var configFrom = {
                dateFormat: GDirectiva.Presentacion.Web.Formato.Fecha,
                //minDate: new Date(),
                onClose: function (selected) {

                    var result = GDirectiva.Presentacion.Web.Components.Util.ValidateBlurOnlyDate(this);

                    if (me.inputTo && me.inputTo != null) {
                        if (result) {
                            me.inputTo.datepicker('option', 'minDate', selected);
                        }
                        else {
                            me.inputTo.datepicker('option', 'minDate', null);
                        }
                    }
                }
            };
            if (opts.maxDateFrom && opts.maxDateFrom != null) {
                configFrom.maxDate = opts.maxDateFrom;
            }
            if (opts.minDateFrom && opts.minDateFrom != null) {
                configFrom.minDate = opts.minDateFrom;
            }
            this.inputFrom.datepicker(configFrom);
            this.inputFrom.bind("blur", function () {
                return GDirectiva.Presentacion.Web.Components.Util.ValidateBlurOnlyDate(this);
            });
            this.inputFrom.mask(GDirectiva.Presentacion.Web.Formato.FechaMascara);
            if (me.inputTo && me.inputTo != null) {
                this.inputFrom.change(function () {
                    if ($(this).val() == "" || $(this).val() == null) {
                        me.inputTo.datepicker('option', 'minDate', null);
                    }
                });
            }

            if (me.onSelectedDate && me.onSelectedDate != null) {
                me.inputFrom.datepicker("option", "onSelect", me.onSelectedDate);
            }
        }

        if (this.inputTo && this.inputTo != null) {
            var configTo = {
                dateFormat: GDirectiva.Presentacion.Web.Formato.Fecha,
                onClose: function (selected) {
                    var result = GDirectiva.Presentacion.Web.Components.Util.ValidateBlurOnlyDate(this);
                    if (me.inputFrom && me.inputFrom != null) {
                        if (result) {
                            me.inputFrom.datepicker('option', 'maxDate', selected);
                        }
                        else {
                            me.inputFrom.datepicker('option', 'maxDate', null);
                        }
                    }
                }
            };
            if (opts.maxDateTo && opts.maxDateTo != null) {
                configTo.maxDate = opts.maxDateTo;
            }
            this.inputTo.datepicker(configTo);
            this.inputTo.bind("blur", function () {
                return GDirectiva.Presentacion.Web.Components.Util.ValidateBlurOnlyDate(this);
            });
            this.inputTo.mask(GDirectiva.Presentacion.Web.Formato.Fecha);
            if (me.inputFrom && me.inputFrom != null) {
                this.inputTo.change(function () {
                    if ($(this).val() == "" || $(this).val() == null) {
                        me.inputFrom.datepicker('option', 'maxDate', null);
                    }
                });
            }

            if (me.onSelectedDate && me.onSelectedDate != null) {
                me.inputTo.datepicker("option", "onSelect", me.onSelectedDate);
            }
        }
    },
    getDateSelected: function () {
        if (this.inputTo && this.inputTo != null) {
            return this.inputTo.datepicker('getDate');
        }

        if (this.inputFrom && this.inputFrom != null) {
            return this.inputFrom.datepicker('getDate');
        }
    },
    setDate: function (date) {
        if (this.inputTo && this.inputTo != null) {
            return this.inputTo.datepicker('setDate', date);
        }

        if (this.inputFrom && this.inputFrom != null) {
            return this.inputFrom.datepicker('setDate', date);
        }
    },
    destroy: function () {
        if (this.inputFrom) {
            this.inputFrom.datepicker("destroy");
        }
        if (this.inputTo) {
            this.inputTo.datepicker("destroy");
        }
    },

    _privateFunction: {
        createDatePicker: function (input, reference) {
            if (input && input != null) {
                input.datepicker({
                    dateFormat: GDirectiva.Presentacion.Web.Formato.Fecha,
                    onClose: function (selected) {
                        if (reference && reference != null) {
                            reference.datepicker('option', 'minDate', selected);
                        }
                    }
                });
            }
        }
    }
};
