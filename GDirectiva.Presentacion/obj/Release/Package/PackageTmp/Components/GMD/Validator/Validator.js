    

ns('GDirectiva.Presentacion.Web.Components.Validator.Resources');
GDirectiva.Presentacion.Web.Components.Validator.Resources.LabelTitleSummary = 'Debe ingresar';

// Copyright (c) 2015.
// All rights reserved.
/// <summary>
/// Libreria para el manejo de validaciones de formulario
/// </summary>
/// <remarks>
/// Creacion: 	GMD(EMP) 20150107 <br />
/// </remarks>
ns('GDirectiva.Presentacion.Web.Components.Validator');
GDirectiva.Presentacion.Web.Components.Validator = function (opts) {
    this.init(opts);
};

GDirectiva.Presentacion.Web.Components.Validator.prototype = {
    form: null,
    validationsExtra: null,
    messages: null,
    validator: null,
    errorMessage: null,
    title: null,
    rulesExtra: null,
    messagesExtra: false,
    idDivSummaryErrorMessage: null,
    showSummary: null,

    init: function (opts) {
        if (opts) {
            this.form = opts.form ? opts.form : null;
            this.validationsExtra = opts.validationsExtra ? opts.validationsExtra : null;
            this.rulesExtra = opts.rulesExtra ? opts.rulesExtra : null;
            this.messages = opts.messages ? opts.messages : null;
            this.title = opts.title ? opts.title : null;
            this.messagesExtra = opts.messagesExtra ? opts.messagesExtra : null;
            this.showSummary = (opts.showSummary == false) ? opts.showSummary : true;

            if (this.form != null) {
                this.idDivSummaryErrorMessage = this.form.attr('id') + 'SummaryErrorMessage';

                //this.errorMessage = new GDirectiva.Presentacion.Web.Components.Message();

                var configValid = this.configure();
                //configValid.success = 'valid';
                configValid.errorClass = 'hasError';
                configValid.errorElement = 'span';
                configValid.errorPlacement = function (error, element) { };
                this.validator = this.form.validate(configValid);
                //this.validator.settings.success = 'valid';
                //this.configure();
            }
        }
    },

    showErrors: function (errorMap, errorList) {

        for (var i = 0; this.errorList[i]; i++) {
            var error = this.errorList[i];
            var labelError = '';
            this.settings.highlight && this.settings.highlight.call(this, error.element, this.settings.errorClass, this.settings.validClass);
            this.showLabel(error.element, labelError);
        }
        if (this.errorList.length) {
            this.toShow = this.toShow.add(this.containers);
        }
        if (this.settings.success) {
            for (var i = 0; this.successList[i]; i++) {
                this.showLabel(this.successList[i]);
            }
        }
        if (this.settings.unhighlight) {
            for (var i = 0, elements = this.validElements() ; elements[i]; i++) {
                this.settings.unhighlight.call(this, elements[i], this.settings.errorClass, this.settings.validClass);
            }
        }
        this.toHide = this.toHide.not(this.toShow).not('.ui-spinner');

        this.hideErrors();
        this.addWrapper(this.toShow).show();
    },

    configure: function () {
        var base = this;

        var rules = {};
        var messages = {};
        var messagesExtra = {};
        base.form.find('[validable]').each(function () {
            var nameControl = $(this).attr('name');
            if (nameControl == undefined) {
                var id = $(this).attr('id');
                $(this).attr('name', id);
                nameControl = $(this).attr('name');
            }

            var settingsControl = {};
            var settingsMessage = {};

            var validations = $(this).attr('validable').split(',');

            for (var i = 0; i < validations.length; i++) {
                var attributes = $.trim(validations[i]).split(' ');

                var typeValidation = $.trim(attributes[0]);
                var newValue = (typeValidation == 'required');

                var value = $.trim(attributes[1]);

                var codeMessage;

                if (value == 'true') {
                    var newValue = true;
                } else if (value == 'false') {
                    var newValue = false;
                } else if ($.isNumeric(value)) {
                    var newValue = parseFloat(value);
                } else {
                    codeMessage = value;
                }

                if (attributes.length > 2) {
                    codeMessage = $.trim(attributes[2]);
                }

                settingsControl[typeValidation] = newValue;
                if (base.messages != null) {
                    settingsMessage[typeValidation] = base.messages[codeMessage];
                }
            }

            rules[nameControl] = settingsControl;
            messages[nameControl] = settingsMessage;
        });

        if (this.rulesExtra != null) {
            for (var i = 0; i < this.rulesExtra.length; i++) {
                var rule = this.rulesExtra[i];
                var ruleSettingsComplete = $.extend({}, rules[rule.nameControl], rule.settings);
                var ruleMessagesComplete = $.extend({}, messages[rule.nameControl], rule.message);

                rules[rule.nameControl] = ruleSettingsComplete;
                messages[rule.nameControl] = ruleMessagesComplete;
            }
        }

        //base.validator.settings.rules = rules;
        //base.validator.settings.messages = messages;
        return { rules: rules, messages: messages };
    },

    isValid: function () {
        var valid = this.generateSummaryError();
        return valid;
    },

    reset: function () {
        this.deleteSummary();
        this.validator.resetForm();
    },

    generateSummaryError: function () {
        //var summaryHtml = GDirectiva.Presentacion.Web.Components.Validator.Resources.LabelTitleSummary + ' ';
        var summaryHtml = 'Debe ingresar: ';
        var valid = true;
        var base = this;
        var msj = this.messages;
        this.validator.form();
        valid = this.validator.valid();

        if (this.validator.errorList.length > 0) {
            for (var i = 0; i < this.validator.errorList.length; i++) {
                var error = this.validator.errorList[i];
                if (i == 0) {
                    summaryHtml = summaryHtml + ' ' + error.message;
                } else {
                    summaryHtml = summaryHtml + ', ' + error.message;
                }
            }
        }

        if (this.validationsExtra != null) {
            for (var i = 0; i < this.validationsExtra.length; i++) {

                var error = this.validationsExtra[i];
                if (error.Event && error.Event != null && !error.Event.apply(error, error.Args)) {
                    if (valid == true) {
                        summaryHtml = summaryHtml + ' ' + base.messages[error.codeMessage];
                    } else {
                        summaryHtml = summaryHtml + ', ' + base.messages[error.codeMessage];
                    }
                    valid = false;
                }
            }
        }

        summaryHtml = summaryHtml + '.';

        this.removeSummary();

        if (!valid && this.showSummary) {
            var divErrorMessage = $('<div />');
            divErrorMessage.attr('id', this.idDivSummaryErrorMessage);
            divErrorMessage.attr('class', 'alert alert-danger');
            divErrorMessage.html(summaryHtml);
            this.form.before(divErrorMessage);
        }

        return valid;
    },

    removeSummary: function () {
        if ($('#' + this.idDivSummaryErrorMessage).length > 0) {
            $('#' + this.idDivSummaryErrorMessage).remove();
        }
    }
}