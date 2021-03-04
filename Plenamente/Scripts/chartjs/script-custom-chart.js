

var canvasChart = document.getElementsByClassName("chart-container");
for (var i = 0; i < canvasChart.length; i++) {
    var id = canvasChart[i].getAttribute("id");
    var chartRoute = canvasChart[i].getAttribute("chart-route");
    var chartType = canvasChart[i].getAttribute("chart-type");
    Chart_Load(id, chartRoute, chartType);
}
function Chart_Load(nameObject, routeMethod, typeChart) {
    $.ajax({
        type: "POST",
        url: routeMethod,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            var datasets = [];
            for (var i = 0; i < chData.datasets.length; i++) {
                var graphColors = [];
                var graphOutlines = [];
                var hoverColor = [];
                for (var j = 0; j < chData.labels.length; j++) {
                    var R = Math.floor(Math.random() * 230);
                    var G = Math.floor(Math.random() * 230);
                    var B = Math.floor(Math.random() * 230);
                    graphColors.push(`rgb(${R},${G},${B})`);
                    graphOutlines.push(`rgb(${R - 80},${G - 80},${B - 80})`);
                    hoverColor.push(`rgb(${R + 25},${G + 25},${B + 25})`);
                }
                datasets.push({
                    label: chData.datasets[i].label,
                    data: chData.datasets[i].data ? chData.datasets[i].data : {},
                    fill: chData.datasets[i].fill,
                    backgroundColor: chData.datasets[i].backgroundColor ? chData.datasets[i].backgroundColor : graphColors,
                    borderColor: chData.datasets[i].borderColor ? chData.datasets[i].borderColor : graphOutlines,
                    hoverBackgroundColor: hoverColor,
                    borderWidth: chData.datasets[i].borderWidth
                });
            }
            var ctx = document.getElementById(nameObject).getContext('2d');
            new Chart(ctx, {
                type: typeChart ? typeChart : 'bar',
                data: {
                    labels: chData.labels,
                    datasets: datasets
                },
                options: {
                    title: {
                        display: true,
                        text: chData.title
                    },
                    scales: {
                        yAxes: [{
                            stacked: true,
                            ticks: {
                                beginAtZero: true
                            }
                        }],
                        xAxes: [{
                            stacked: true,
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });
        }
    });
}