/// Copyright (c) 2015.
/// All rights reserved.
/// <summary>
/// Manejo de Storage
/// </summary>
/// <remarks>
/// Creacion: 	GMD(CERS) 20150107 <br />
/// </remarks>
ns('GDirectiva.Presentacion.Web.Components.Storage');
GDirectiva.Presentacion.Web.Components.Storage = {
    Exists: function (name) {
        var item = sessionStorage.getItem(name);
        return (item != null);
    },
    Get: function (name) {
        return JSON.parse(sessionStorage.getItem(name));
    },
    Set: function (name, value, days) {
        sessionStorage.setItem(name, JSON.stringify(value))
    },
    Remove: function (name) {
        sessionStorage.removeItem(name);
    }
}