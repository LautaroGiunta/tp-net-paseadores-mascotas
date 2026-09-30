// Puente entre Blazor y Chart.js: Blazor manda los datos ya armados y acá solo se dibuja.
window.graficos = (function () {
    const instancias = {};
    const pesos = new Intl.NumberFormat("es-AR", { style: "currency", currency: "ARS", maximumFractionDigits: 0 });

    // series: [{ nombre, color, valores: [número por etiqueta] }]
    function barrasAgrupadas(canvasId, etiquetas, series, tituloEjeY) {
        const canvas = document.getElementById(canvasId);
        if (!canvas) return;

        // Al regenerar el reporte se reemplaza el gráfico anterior
        if (instancias[canvasId]) {
            instancias[canvasId].destroy();
        }

        instancias[canvasId] = new Chart(canvas, {
            type: "bar",
            data: {
                labels: etiquetas,
                datasets: series.map(s => ({
                    label: s.nombre,
                    data: s.valores,
                    backgroundColor: s.color,
                    borderRadius: 4,          // puntas redondeadas, la base queda recta sobre el eje
                    borderSkipped: "start",
                    categoryPercentage: 0.8,
                    barPercentage: 0.9        // deja una separación entre barras vecinas
                }))
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                interaction: { mode: "index", intersect: false },
                plugins: {
                    legend: { position: "right" },
                    tooltip: {
                        callbacks: {
                            label: ctx => `${ctx.dataset.label}: ${pesos.format(ctx.parsed.y)}`
                        }
                    }
                },
                scales: {
                    x: { grid: { display: false } },
                    y: {
                        beginAtZero: true,
                        title: { display: true, text: tituloEjeY },
                        ticks: { callback: valor => pesos.format(valor) }
                    }
                }
            }
        });
    }

    return { barrasAgrupadas };
})();
