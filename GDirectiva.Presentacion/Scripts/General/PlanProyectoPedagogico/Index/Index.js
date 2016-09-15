ns('GDirectiva.Presentacion.General.PlanProyectoPedagogico.Index');
try {

    $(document).ready(function () {
        'use strict';
        GDirectiva.Presentacion.General.PlanProyectoPedagogico.Index.Vista = new GDirectiva.Presentacion.General.PlanProyectoPedagogico.Index.Controller();
        GDirectiva.Presentacion.General.PlanProyectoPedagogico.Index.Vista.Ini();
    });
} catch (ex) {

}