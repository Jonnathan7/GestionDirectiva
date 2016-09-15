(function () {
    var d = document, w = window;
    function get(element) {
        if (typeof element == "string") {
            element = d.getElementById(element);
        }
        return element;
    }

    function addEvent(el, type, fn) {
        if (w.addEventListener) {
            el.addEventListener(type, fn, false);
        } else if (w.attachEvent) {
            var f = function () {
                fn.call(el, w.event);
            };
            el.attachEvent('on' + type, f);
        }
    }

    var toElement = function () {
        var div = d.createElement('div');
        return function (html) {
            div.innerHTML = html;
            var el = div.childNodes[0];
            div.removeChild(el);
            return el;
        }
    }();

    function hasClass(ele, cls) {
        return ele.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
    }

    function addClass(ele, cls) {
        if (!hasClass(ele, cls)) ele.className += " " + cls;
    }

    function removeClass(ele, cls) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        ele.className = ele.className.replace(reg, ' ');
    }

    if (document.documentElement["getBoundingClientRect"]) {
        var getOffset = function (el) {
            var box = el.getBoundingClientRect();
            var doc = el.ownerDocument;
            var body = doc.body;
            var docElem = doc.documentElement;
            var clientTop = docElem.clientTop || body.clientTop || 0;
            var clientLeft = docElem.clientLeft || body.clientLeft || 0;
            var zoom = 1;

            if (body.getBoundingClientRect) {
                var bound = body.getBoundingClientRect();
                zoom = (bound.right - bound.left) / body.clientWidth;
            }
            if (zoom > 1) {
                clientTop = 0;
                clientLeft = 0;
            }
            var top = box.top / zoom + (window.pageYOffset || docElem && docElem.scrollTop / zoom || body.scrollTop / zoom) - clientTop;
            var left = box.left / zoom + (window.pageXOffset || docElem && docElem.scrollLeft / zoom || body.scrollLeft / zoom) - clientLeft;
            return {
                top: top,
                left: left
            };
        }
    } else {
        var getOffset = function (el) {
            if (w.jQuery) {
                return jQuery(el).offset();
            }
            var top = 0, left = 0;
            do {
                top += el.offsetTop || 0;
                left += el.offsetLeft || 0;
            }
            while (el = el.offsetParent);
            return {
                left: left,
                top: top
            };
        }
    }

    function getBox(el) {
        var left, right, top, bottom;
        var offset = getOffset(el);
        left = offset.left;
        top = offset.top;
        right = left + el.offsetWidth;
        bottom = top + el.offsetHeight;
        return {
            left: left,
            right: right,
            top: top,
            bottom: bottom
        };
    }

    function getMouseCoords(e) {
        if (!e.pageX && e.clientX) {
            var zoom = 1;
            var body = document.body;

            if (body.getBoundingClientRect) {
                var bound = body.getBoundingClientRect();
                zoom = (bound.right - bound.left) / body.clientWidth;
            }

            return {
                x: e.clientX / zoom + d.body.scrollLeft + d.documentElement.scrollLeft,
                y: e.clientY / zoom + d.body.scrollTop + d.documentElement.scrollTop
            };
        }
        return {
            x: e.pageX,
            y: e.pageY
        };
    }

    var getUID = function () {
        var id = 0;
        return function () {
            return 'ValumsAjaxUpload' + id++;
        }
    }();

    var getFormUID = function () {
        var id = 0;
        return function () {
            return 'FormValumsAjaxUpload' + id++;
        }
    }();

    function fileFromPath(file) {
        return file.replace(/.*(\/|\\)/, "");
    }

    function getExt(file) {
        return (/[.]/.exec(file)) ? /[^.]+$/.exec(file.toLowerCase()) : '';
    }

    var getXhr = function () {
        var xhr;
        return function () {
            if (xhr) return xhr;
            if (typeof XMLHttpRequest !== 'undefined') {
                xhr = new XMLHttpRequest();
            } else {
                var v = ["Microsoft.XmlHttp",
				"MSXML2.XmlHttp.5.0",
				"MSXML2.XmlHttp.4.0",
				"MSXML2.XmlHttp.3.0",
				"MSXML2.XmlHttp.2.0"];

                for (var i = 0; i < v.length; i++) {
                    try {
                        xhr = new ActiveXObject(v[i]);
                        break;
                    } catch (e) { }
                }
            }
            return xhr;
        }
    }();

    Ajax_upload = AjaxUpload = function (inputFile, options) {


        //this._buttonOk = buttonOk;
        this._inputFile = inputFile;
        this._disabled = false;
        this._submitting = false;
        this._response;

        this._settings = {
            action: 'upload.php',
            name: 'userfile',
            data: {},
            responseType: false,
            closeConnection: '',
            hoverClass: 'hover',
            //onChange: function (file, extension) { },
            //onSubmit: function (file, extension) { },
            onComplete: function (file, response) { },
            //onError: function (file) { },
            //multiple: false,
            //urlDelete: "",
            //urlShow: "",
            btnAceptarFile: null,
            identificador: null,
            //urlUploadFile: null,
            //ajaxUploadSuccess: null,
        };

        for (var i in options) {
            this._settings[i] = options[i];
        }

        this._createInput();
        //this._getdocument();
    }

    AjaxUpload.prototype = {
        setData: function (data) {
            this._settings.data = data;
        },
        disable: function () {
            this._disabled = true;
        },
        enable: function () {
            this._disabled = false;
        },
        destroy: function () {

        },
        _createInput: function () {
            var self = this;
            var settings = this._settings;
            var iframe = this._createIframe();
            var form = this._createForm(iframe);

            $(this._inputFile).detach().appendTo($(form));


            addEvent($(self._inputFile)[0], 'change', function () {
                settings.reset = false
                self.submit(iframe, form, false);
            });


            //$(self._buttonOk).off("click");
            //$(self._buttonOk).on("click", function () {
            //    form.action = settings.urlUploadFile + (settings.identificador == null ? "" : settings.identificador);
            //    settings.reset = true;
            //    self.submit(iframe, form, true);
            //})
        },
        _createIframe: function () {
            var self = this, settings = this._settings;
            var id = getUID();
            var iframe = toElement('<iframe src="javascript:false;" name="' + id + '" />');
            iframe.id = id;
            iframe.style.display = 'none';

            $(self._inputFile).before(iframe);
            var toDeleteFlag = false;
            addEvent(iframe, 'load', function (e) {
                try {
                    doc = iframe.contentDocument ? iframe.contentDocument : frames[iframe.id].document;
                } catch (permissionDenied) {
                    //settings.onError.call(self, file);
                    toDeleteFlag = true;
                    return;
                }
                if (doc.readyState && doc.readyState != 'complete') {
                    return;
                }
                if (doc.body && doc.body.innerHTML == "false") {
                    return;
                }
                //var response;
                if (doc.XMLDocument) {
                    self.response = doc.XMLDocument;
                } else if (doc.body) {
                    self.response = doc.body.innerHTML;
                    if (settings.responseType && settings.responseType.toLowerCase() == 'json') {
                        if (doc.body.firstChild && doc.body.firstChild.nodeName.toUpperCase() == 'PRE') {
                            self.response = doc.body.firstChild.firstChild.nodeValue;
                        }
                        if (self.response) {
                            self.response = window["eval"]("(" + response + ")");
                        } else {
                            self.response = {};
                        }
                    }
                } else {
                    self.response = doc;
                }

                //if (settings.reset) {
                //if (typeof settings.ajaxUploadSuccess == 'function') {
                //    settings.ajaxUploadSuccess.call(this, JSON.parse(self.response));
                //}

                //} else {
                settings.onComplete.call(self, JSON.parse(self.response));
                //}
            });
            return iframe;
        },
        _createForm: function (iframe) {
            var settings = this._settings;
            var id = getFormUID();
            var form = toElement('<form method="post" enctype="multipart/form-data" name="' + id + '" /></form>');
            form.id = id;
            form.action = settings.action; //+ (settings.identificador == null ? "" : settings.identificador);
            form.target = iframe.name;
            $(this._inputFile).after(form);

            ///init(id, iframe.name)

            return form;
        },
        submit: function (iframe, form, reset) {
            form.submit();
            if (reset) {
                resetFormElement($("#" + $(form).find("input[type=file]").attr("id")));
            }
        }
    };
})();

//function ajaxUpload(btnAceptarFile, idElement, callback, urlSubmit, identificador, urlUploadFile) {

//}
function resetFormElement(e) {
    e.wrap('<form>').closest('form').get(0).reset();
    e.unwrap();
}

//function init(id, nameIframe) {
//    document.getElementById(id).onsubmit = function () {
//        document.getElementById(id).target = nameIframe;
//        document.getElementById(nameIframe).onload = function () { uploadDone(nameIframe) }; //This function should be called when the iframe has compleated loading
//        // That will happen when the file is completely uploaded and the server has returned the data we need.
//    }
//}

//function uploadDone(nameIframe) { //Function will be called when iframe is loaded
//    var ret = frames[nameIframe].document.getElementsByTagName("body")[0].innerHTML;
//    var data = eval("(" + ret + ")"); //Parse JSON // Read the below explanations before passing judgment on me

//    if (data.success) { //This part happens when the image gets uploaded.
//        document.getElementById("image_details").innerHTML = "<img src='image_uploads/" + data.file_name + "' /><br />Size: " + data.size + " KB";
//    }
//    else if (data.failure) { //Upload failed - show user the reason.
//        alert("Upload Failed: " + data.failure);
//    }
//}
