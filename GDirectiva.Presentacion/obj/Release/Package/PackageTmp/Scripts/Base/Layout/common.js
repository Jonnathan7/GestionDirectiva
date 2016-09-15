/*--------------------------------------------------------------------------------------------------------- 
*@function: loadAjax
*@description : Agrega la clase loading al body, la clase loading tiene los estilos para la carga.
**//*---------------------------------------------------------------------------------------------------- */
function loadAjax() {
    try {
        $.xAjaxPool = [];
        $.ajaxSetup({
            beforeSend: function (jqXAjax) {
                $('body').addClass('loading');


                $.xAjaxPool.push(jqXAjax);
            },
            complete: function (jqXAjax) {
                var index = $.xAjaxPool.indexOf(jqXAjax);
                if (index > -1) {
                    $.xAjaxPool.splice(index, 1);
                }
                if ($.xAjaxPool.length == 0) {
                    $('body').removeClass('loading');
                }
            },
            error: function (jqXAjax) {
                var index = $.xAjaxPool.indexOf(jqXAjax);
                if (index > -1) {
                    $.xAjaxPool.splice(index, 1);
                }
                $('body').removeClass('loading');
            }
        });

    } catch (mierror) {
        //hago algo cuando el error se ha detectado
    }
};

/*--------------------------------------------------------------------------------------------------------- 
*@function: scrollTop
*@description : Muestra flecha para scrollear hacia arriba
**//*---------------------------------------------------------------------------------------------------- */
function scrollTop() {
    var $element = $("[data-element='btnUp']");

    $(window).scroll(function () {
        if ($(this).scrollTop() > 250) {
            $element.fadeIn();
        } else {
            $element.fadeOut();
        }
    });

    // scroll body to 0px on click
    $element.click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 800);
        return false;
    });
};
scrollTop();


/*--------------------------------------------------------------------------------------------------------- 
RESIZE WINDOW SCROLL -- LAY-MAINCONTENT
**//*---------------------------------------------------------------------------------------------------- */
function MainContent() {
    $(window).resize(function () {
        $('.lay-MainContent').css("height", $(window).innerHeight() - 104);
    });
    $(window).trigger('resize');
};

MainContent();


/*--------------------------------------------------------------------------------------------------------- 
*@function: asideSlide
*@description : Efecto de slide al menu
**//*---------------------------------------------------------------------------------------------------- */
; (function ($, window, document, undefined) {



    // The actual plugin constructor
    function asideSlide(options) {
        //SELECTORES
        $body = $('body');
        $btn = $('[data-script="asideSlide"][data-element="button"'),
        $slide = $('[data-script="asideSlide"][data-element="slide"]');
        this.options = $.extend({}, asideSlide.defaults, options);
        this.init();
    }

    asideSlide.defaults = {
        speed: '' || 400
        , fx: ''
    }

    asideSlide.prototype = {
        init: function () {
            var that = this;
            that.constructor();
        },
        constructor: function () {
            var that = this;
            if ($body.hasClass('nav-Aside--closed')) {
                $('.wrap-text, .bl-slide').css({
                    left: -202
                }, that.options.speed)
            }
            if ($body.hasClass('nav-Aside--open')) {
                $btn.addClass('active');
            }
            that.eventos();
        },
        eventos: function () {
            var that = this;
            $btn.on('click', function () {
                var $el = $(this),
                    $open = $el.hasClass('active');

                if (!$open) {
                    $body.addClass('nav-Aside--open');
                    $body.removeClass('nav-Aside--closed nav-Aside--closedComplete');
                    $el.addClass('active');

                    $('.wrap-text, .bl-slide').stop().animate({
                        left: 0
                    }, that.options.speed, function () {
                        $body.addClass('nav-Aside--openComplete');
                        if ($body.hasClass('nav-Aside--openComplete')) {

                            $('[data-menu]').each(function (key, val) {
                                if ($(this).hasClass('active')) {
                                    $(this).find('.GmdNavMain-subMenu').slideDown();
                                }
                            });
                        }
                    });

                } else {
                    $body.addClass('nav-Aside--closed');
                    $body.removeClass('nav-Aside--open nav-Aside--openComplete');


                    $('.GmdNavMain-subMenu').slideUp(function () {
                        /*$("[data-navAjax='true']").each(function (key, val) {
                            var $el = $(this);

                            if ($el.hasClass('active')) {
                                $('.menu-dropdown').removeClass('active');
                                $el.closest('.menu-dropdown').addClass('active');
                            }
                        })*/
                    });


                    setTimeout(function () {
                        $('.bl-slide').stop().animate({
                            left: -202
                        }, that.options.speed);
                        $('.wrap-text').stop().animate({
                            left: -260
                        }, that.options.speed, function () {
                            $el.removeClass('active');
                            $body.addClass('nav-Aside-closedComplete');
                        });
                    }, 500)
                }
            });
        }
    };


    $.fn.asideSlide = function (options) {
        if (!$.data(this, 'plugin_' + 'asideSlide')) {
            $.data(this, 'plugin_' + 'asideSlide',
            new asideSlide(options));
        }
    }

})(jQuery, window, document);




///*--------------------------------------------------------------------------------------------------------- 
//*@function: menuMultinivel
//*@description : Menu Multinivel
//**//*---------------------------------------------------------------------------------------------------- */
; (function ($, window, document, undefined) {


    defaults = {
        type: '' || 'dropdown',
        speed: '' || 600,
        fx: ''
    };

    // The actual plugin constructor
    function menuMultinivel(element, options) {

        //SELECTORES
        this.$body = $('body'),
        this.$allElement = $('[data-menu]'),
        this.$allElementBtn = $('[data-menu]').children('a'),
        this.$element = $(element);
        this.$btnToggle = $('[data-toggleclass="active"]');
        this.$elementBtn = this.$element.children('a');
        this.$elementSubMenu = this.$allElement.find('.GmdNavMain-subMenu');
        this.$btnDropdown = this.$element.find('[data-element="button-dropdown"]');
        this.$btnSlide = $('[data-script="asideSlide"][data-element="button"'),

        this.element = element;


        this.options = $.extend({}, defaults, options);
        this._defaults = defaults;


        this.toggleClass();
        var that = this;

        this.$btnDropdown.on('click', function () {
            if (that.$body.hasClass('nav-Aside--openComplete')) {
                that.fx(that.options.type, $(this))
            }
        });



    }
    menuMultinivel.prototype = {
        toggleClass: function () {
            var that = this,
                linkSubmenu = that.$element.filter('[data-menu="dropdown"]');

            that.$btnToggle.on('click', function (e) {
                var $el = $(this),
                    $elLi = $el.closest('li.GmdNavMain-menuDropdown');


                that.$allElement.removeClass('active');
                that.$btnToggle.removeClass('active');

                $elLi.addClass('active');
                $el.addClass('active');

                if (that.$body.hasClass('nav-Aside--openComplete')) {
                    that.$elementSubMenu.slideUp();
                    that.$btnSlide.trigger('click');
                };

            });


        },
        fx: function (optionType) {

            var that = this,
                $el = arguments[1],
                $elLi = $el.closest('.GmdNavMain-menuDropdown'),
                $elUl = $elLi.find('.GmdNavMain-subMenu');


            switch (optionType) {
                case 'dropdown': {
                    if (!$elLi.hasClass('active')) {
                        that.$allElement.removeClass('active');
                        that.$elementSubMenu.slideUp();
                        $elLi.addClass('active');
                        $elUl.stop().slideDown(this.options.speed);
                    } else {
                        $elLi.removeClass('active');
                        $elUl.stop().slideUp(this.options.speed);
                    }
                }
            }
        }
    };


    $.fn.menuMultinivel = function (options) {
        return this.each(function () {
            if (!$.data(this, 'plugin_' + 'menuMultinivel')) {
                $.data(this, 'plugin_' + 'menuMultinivel',
                new menuMultinivel(this, options));
            }
        });
    }

})(jQuery, window, document);


/*--------------------------------------------------------------------------------------------------------- 
*@function: navAjax
*@description : Navegacion utilizando Ajax
**//*---------------------------------------------------------------------------------------------------- */
; (function ($, window, document, undefined) {



    // The actual plugin constructor
    function navAjax(element, options) {

        //SELECTORES
        $element = $(element);
        $navAjaxBtn = $('[data-navAjax]');
        $navAjaxContent = $('[data-navAjaxcontent]');
        this.element = element;


        this.options = $.extend({}, defaults, options);
        this.init();
    }

    navAjax.prototype = {
        init: function () {
            var that = this;
            that.constructor();
        },
        constructor: function () {
            var that = this;
            that.eventos();
        },
        eventos: function () {
            var that = this;
            $element.on('click', function (e) {
                e.preventDefault();
                var $el = $(this),
                    url = $(this).attr('href');



                if (url == '#' || url == '') {
                    alert("Ingresar url valida")
                } else {
                    that.ajax(url)
                }

            })
        },
        addClass: function () {
            alert("hola");
        },
        ajax: function (url) {
            var that = this;
            $.ajax({
                url: url,
                success: function (data) {
                    $navAjaxContent.empty()
                    $navAjaxContent.append(data);
                }
            })

        }
    };


    $.fn.naxAjax = function (options) {
        return this.each(function () {
            if (!$.data(this, 'plugin_' + 'navAjax')) {
                $.data(this, 'plugin_' + 'navAjax',
                new navAjax(this, options));
            }
        });
    }





})(jQuery, window, document);




///*---------------------------------------------------------------------------------



/*--------------------------------------------------------------------------------------------------------- 
INIT
**//*---------------------------------------------------------------------------------------------------- */
$(function () {

    $('body').tooltip({
        selector: "[data-toggle='tooltip']"
    });
    $('body').popover({
        selector: "[data-toggle='popover']"
    });


    //SLIDEMENU
    $.fn.asideSlide();

    //NAVAJAX
    var nav = $('[data-navAjax]').naxAjax();

    //MENU MULTINIVEL
    $('[data-menu]').menuMultinivel({
        type: "dropdown"
    });

});