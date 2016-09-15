ns('GDirectiva.Presentacion.Web.Components.Chart');

GDirectiva.Presentacion.Web.Components.Chart = function (opts) {
    this.Init(opts);
}

GDirectiva.Presentacion.Web.Components.Chart.prototype = {
    chart: null,
    options: null,
    Init: function (opts) {
        var me = this;
        if (opts) {
            me._privateProperties.options.chart.renderTo = opts.id ? opts.id : null;
            me._privateProperties.options.chart.defaultSeriesType = opts.type ? opts.type : null;
            me._privateProperties.options.title.text = opts.titleText ? opts.titleText : null;
            me._privateProperties.options.subtitle.text = opts.subtitleText ? opts.subtitleText : null;
            me._privateProperties.options.tooltip.pointFormat = opts.tooltipPointFormat ? opts.tooltipPointFormat : null;
            me._privateProperties.options.plotOptions.pie.dataLabels.format = opts.dataLabelFormat ? opts.dataLabelFormat : null;
            me._privateProperties.options.series = opts.series ? opts.series : [];
            me._privateProperties.options.xAxis.type = opts.xAxisType ? opts.xAxisType : null;
            me._privateProperties.options.chart.zoomType = opts.zoomType ? opts.zoomType : null;
            me._privateProperties.options.yAxis.title.text = opts.yAxisTitle ? opts.yAxisTitle : null;
            me._serieInnerSize = opts.yAxisTitle ? opts.yAxisTitle : me._serieInnerSize;
        }

        me.options = me._privateProperties.options;
        me.chart = new Highcharts.Chart(me._privateProperties.options);

        Highcharts.setOptions({
            lang: {
                loading: 'Aguarde...',
                months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                weekdays: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                shortMonths: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Set', 'Oct', 'Nov', 'Dic'],
                exportButtonTitle: "Exportar",
                printButtonTitle: "Imprimir",
                rangeSelectorFrom: "De",
                rangeSelectorTo: "Até",
                rangeSelectorZoom: "Periodo",
                downloadPNG: 'Download imagem PNG',
                downloadJPEG: 'Download imagem JPEG',
                downloadPDF: 'Download documento PDF',
                downloadSVG: 'Download imagem SVG',
                resetZoom: "Restaurar tamaño",
                resetZoomTitle: "Restaurar tamaño original",
                thousandsSep: ",",
                decimalPoint: '.'
            }
        });
    },
    SetSeries: function (newSeries) {
        var me = this;

        while (me.chart.series.length > 0) {

            me.chart.series[0].remove();
        }

        //if (me._serieInnerSize && me._serieInnerSize != null)
        newSeries.innerSize = me._serieInnerSize;

        me.chart.addSeries(newSeries);
        me.chart.redraw();

    },
    _serieInnerSize: '70%',
    _privateProperties: {
        options: {
            chart: {
                renderTo: 'container',
                defaultSeriesType: 'pie',
                zoomType: null
            },
            title: {
                text: '',
                font: 'bold',
                align: 'center',
                style: {
                    color: '#7f7f7f',
                    'font-size': '18px'
                }
            },
            subtitle: {
                text: '100%',
                font: 'bold 120%',
                style: {
                    color: '#7f7f7f',
                    'font-size': '220%'
                },
                align: 'center',
                verticalAlign: 'middle',
                y: 20
            },
            tooltip: {
                pointFormat: null,
            },
            series: [],
            plotOptions: {
                pie: {
                    allowPointSelect: false,
                    borderColor: "#FFFFFF",
                    borderWidth: 1,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: null,
                        color: '{point.color}'
                    },
                    startAngle: -90,
                    endAngle: 270,
                    center: ['50%', '50%']
                },
                areaspline: {
                    lineColor: '#ffca8a',
                    marker: {
                        fillColor: '#ffca8a',
                        states: {
                            hover: {
                                fillColor: '#FFFFFF',
                                enabled: true,
                                lineColor: "orange",
                                lineWidth: 2,
                                lineWidthPlus: 2,
                                radiusPlus: 2,
                            }
                        }
                    },
                    tooltip: {
                        dateTimeLabelFormats: {
                            day: "%e %B %Y"
                        }
                    }
                }
            },
            xAxis: {
                type: 'datetime'
            },
            yAxis: {
                title: {
                    text: null
                }
            }
        }
    }
}