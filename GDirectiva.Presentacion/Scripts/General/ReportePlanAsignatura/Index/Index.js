ns('GDirectiva.Presentacion.General.ReportePlanAsignatura.Index');
try {

    $(document).ready(function () {
        'use strict';
        GDirectiva.Presentacion.General.ReportePlanAsignatura.Index.Vista = new GDirectiva.Presentacion.General.ReportePlanAsignatura.Index.Controller();
        GDirectiva.Presentacion.General.ReportePlanAsignatura.Index.Vista.Ini();
    });
} catch (ex) {

}