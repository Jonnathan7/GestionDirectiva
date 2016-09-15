/// <summary>
/// Script de controlador del layaut del site.
/// </summary>
/// <remarks>
/// Creacion: 	GMD(JLRR) 07/01/2015
/// </remarks>
ns('Pe.Stracon.Politicas.Presentacion.Base.Layout.Current');
Pe.Stracon.Politicas.Presentacion.Base.Layout.Current.SubModule = null;

ns('Pe.Stracon.Politicas.Presentacion.Base.Layout');
Pe.Stracon.Politicas.Presentacion.Base.Layout.Controller = function () {
    var base = this;
    base.Ini = function () {
        'use strict';
        base.Control.btnMenu().on('click', base.Event.BtnMenuClick);
        base.Control.btnOffCanvas().on('click', base.Event.BtnOffCanvasClick);
        base.Control.accordionMenu().on('click', base.Event.BtnSubMenuClick);
        
    };

    base.Control = {
        btnMenu: function () { return $('#btnTogleMenu'); },
        btnOffCanvas: function () { return $('#btnOffcanvas'); },
        cssModuleNav: function () { return $('#divSidebar') },
        cssModulePanel: function () { return $('#divSideContent') },
        divOffCanvas: function () { return $('#divOffCanvas'); },
        accordionMenu: function () { return $('#accordionMenu > li.panel'); }
    };

    base.Event = {
        BtnOffCanvasClick: function (e) {
            base.Control.cssModuleNav().toggleClass('active');
            base.Control.cssModulePanel().toggleClass('active');
            base.Control.divOffCanvas().toggleClass('active');
            base.Control.cssModuleNav().removeClass('expanded');
            base.Control.cssModulePanel().removeClass('expanded');
        },
        BtnMenuClick: function (e) {
            base.Control.cssModuleNav().toggleClass('expanded');
            base.Control.cssModulePanel().toggleClass('expanded');
        },
        BtnSubMenuClick: function (e) {
            base.Control.cssModuleNav().removeClass('expanded');
            base.Control.cssModulePanel().removeClass('expanded');
        },
        CssToogleClick: function (event) {
            event.preventDefault();
            $(this).parent().parent().next().toggle('fast');
        },
    };

    base.Ajax = {

    };

    base.Function = {
        SelectCurrenteModule: function () {
            /*
            var modulo = Yanbal.PYF.Web.Global.MenuSeleccionado.Modulo;
            var submenu = Yanbal.PYF.Web.Global.MenuSeleccionado.SubModulo;

            $('#' + modulo).addClass('active');
            $('#' + submenu).addClass('active');
            $('#' + modulo).find('.btn-submenu, .w-submenu').addClass('active');
            $('#' + modulo).find('.btn-submenu').next('ul').slideDown('fast', function () {
                $('#' + submenu).focus();
            });
            subMenuActual = $('#' + modulo).find('.btn-submenu');
            */
        }
    };
};