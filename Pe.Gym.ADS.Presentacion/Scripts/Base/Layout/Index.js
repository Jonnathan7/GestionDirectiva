/// <summary>
/// Script del Layout de la aplicación
/// </summary>
/// <remarks>
/// Creacion: 	GMD(JLRR) 07/01/2015
/// </remarks>
ns('Pe.Stracon.Politicas.Presentacion.Base.Layout');
try {

    $(document).ready(function () {
        'use strict';
        Pe.Stracon.Politicas.Presentacion.Base.Layout.Page = new Pe.Stracon.Politicas.Presentacion.Base.Layout.Controller();
        Pe.Stracon.Politicas.Presentacion.Base.Layout.Page.Ini();
       
    });
} catch (ex) {
    //Belcorp.Planit.RegistrarError(ex);
}