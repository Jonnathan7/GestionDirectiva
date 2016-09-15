/* @TheDiegaso */ /*DAGC*/

$.ajaxSetup({
    beforeSend: function () {
        $('body').addClass('loading');
    },
    complete: function () {
        $('body').removeClass('loading');
    }
});

var navSlide = function(){
    
};

navSlide.prototype = {
  selectores : function(){
  this.$html = $('html'),
        this.$wrap = $('.view-wrap'),
        this.$btnAjx = $('.nav_ajx');
        this.eventAjx();
    },
    eventAjx : function(){
    var that = this;
    this.$html.on('click','.nav_ajx',function(event){
    event.preventDefault();
    var url = $(this).attr('href'),
              $el = $(this);
    if(!url || url == '' || url == '#'){
    alert("Ingrese Ruta")
    }else{
            if(!$el.hasClass('btn_inner')){
              that.$btnAjx.removeClass('active');
            }          
            $el.addClass('active');
            that.navAjx(url);
          }  
    })
    },
    navAjx : function(url){ 
        var that = this; 
        $.ajax({
          url : url,
          success:function(data){    
            that.$wrap.empty();
            that.$wrap.html(data);
          }
        });  
    },
    init : function(){
    this.selectores();  
    }
}