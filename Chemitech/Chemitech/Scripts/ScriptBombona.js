
function grafico(nome, capacidade, id, quantidadeAtual, nomeres) {
    //$(document).ready(function () {

    console.log(nomeres.split(")"))

    //var arr = nomeres.split("|");
    //var dados = "";
    //for (var i = 0; i < arr.length-1; i++) {
    //    var separacao = arr[i].split("-");

    //    dados += "{ name: '" + separacao[0] + "', y: " + separacao[1] + "},";
    //}
    //var dados2 = dados.substr(0, dados.length - 1);
   
    //[nomeres, quantidadeAtual],
    //    {
    //        name: nomeres,
    //        y: quantidadeAtual,

    //    }


    Highcharts.chart('container' + id, {

        chart: {
            type: 'gauge',
            plotBackgroundColor: null,
            plotBackgroundImage: null,
            plotBorderWidth: 0,
            plotShadow: false
        },

        title: {
            text: nome, //'Nome da bombona' 
        },

        pane: {
            startAngle: -150,
            endAngle: 150,
            background: [{
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#FFF'],
                        [1, '#333']
                    ]
                },
                borderWidth: 0,
                outerRadius: '109%'
            }, {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#333'],
                        [1, '#FFF']
                    ]
                },
                borderWidth: 1,
                outerRadius: '107%'
            }, {
                // default background
            }, {
                backgroundColor: '#DDD',
                borderWidth: 0,
                outerRadius: '105%',
                innerRadius: '103%'
            }]
        },

        // the value axis
        yAxis: {
            min: 0,
            max: parseInt(capacidade), //500,// capacidade total da bombona

            minorTickInterval: 'auto',
            minorTickWidth: 1,
            minorTickLength: 10,
            minorTickPosition: 'inside',
            minorTickColor: '#666',

            tickPixelInterval: 30,
            tickWidth: 2,
            tickPosition: 'inside',
            tickLength: 10,
            tickColor: '#666',

            labels: {
                step: 2,
                rotation: 'auto'
            },
            title: {
                text: 'Capacidade'
            },
            plotBands: [{
                from: 0,
                to: 12000,
                color: (quantidadeAtual >= capacidade) ? '#ff0000' : '#55bf3b', // green

            }]
        },


        series: [{
            name: 'Speed',
            data: [quantidadeAtual], // valor do ponteiro - quantidade atual
            center: [110, 90],
            tooltip: {
                valueSuffix: ' litros'
            }
        },
        {
            type: 'pie',
            name: 'Total do resíduo',
            data: [
               dados2    
            ],
            center: [20, 20],
            size: 80,
            showInLegend: false,
            dataLabels: {
                enabled: false
            }
        }]

    });

    //});
}