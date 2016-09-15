ns('GDirectiva.Presentacion.General.PlanArea.Index');
try {

    $(document).ready(function () {
        'use strict';
        GDirectiva.Presentacion.General.PlanArea.Index.Vista = new GDirectiva.Presentacion.General.PlanArea.Index.Controller();
        GDirectiva.Presentacion.General.PlanArea.Index.Vista.Ini();
    });
} catch (ex) {

}