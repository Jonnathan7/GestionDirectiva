ns('GDirectiva.Presentacion.General.PlanAsignatura.Index');
try {

    $(document).ready(function () {
        'use strict';
        GDirectiva.Presentacion.General.PlanAsignatura.Index.Vista = new GDirectiva.Presentacion.General.PlanAsignatura.Index.Controller();
        GDirectiva.Presentacion.General.PlanAsignatura.Index.Vista.Ini();
    });
} catch (ex) {

}