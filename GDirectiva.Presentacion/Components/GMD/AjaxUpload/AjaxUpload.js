ns('GDirectiva.Presentacion.Web.Components.AjaxUpload');
GDirectiva.Presentacion.Web.Components.AjaxUpload = function (opts) {
    this.init(opts);
};

GDirectiva.Presentacion.Web.Components.AjaxUpload.prototype = {

    inputFile: null,
    urlValidateFile: null,
    callbackValidateFile: null,
    init: function (opts) {
        this.inputFile = opts.inputFile;
        this.urlValidateFile = opts.urlValidateFile;
        this.callbackValidateFile = opts.callbackValidateFile;

        var that = this;
        var upload = new Ajax_upload(this.inputFile, {
            action: this.urlValidateFile,
            onSubmit: function (file, ext) {
            },
            onChange: function (file, extension) {

            },
            onComplete: function (response) {
                if (typeof that.callbackValidateFile == 'function') {
                    that.callbackValidateFile.call(this, response);
                }
            },
        });
        return upload;
    }
};
