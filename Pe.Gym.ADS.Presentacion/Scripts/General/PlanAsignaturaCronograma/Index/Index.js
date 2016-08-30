ns('GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Index');
try {

    $(document).ready(function () {
        'use strict';
        GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Index.Vista = new GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Index.Controller();
        GDirectiva.Presentacion.General.PlanAsignaturaCronograma.Index.Vista.Ini();
    });
} catch (ex) {

}