using API.Clients;
using DTOs;

namespace Paseadores.WinForms
{
    // Reporte con gráfico: lo recaudado por cada paseador mes a mes (barras agrupadas),
    // con la tabla de valores abajo. Los datos salen de una consulta ADO.NET en la API.
    public partial class FormReporteRecaudacion : Form
    {
        // Orden fijo de paseadores para que cada uno conserve su color entre filtros
        private List<PaseadorDTO> paseadores = new List<PaseadorDTO>();

        public FormReporteRecaudacion()
        {
            InitializeComponent();
        }

        private async void FormReporteRecaudacion_Load(object sender, EventArgs e)
        {
            dgvDetalle.AutoGenerateColumns = false;

            // Por defecto, los últimos seis meses incluido el actual
            DateTime inicioMesActual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpDesde.Value = inicioMesActual.AddMonths(-5);
            dtpHasta.Value = DateTime.Today;

            try
            {
                paseadores = (await ApiClient.Paseadores.GetAllAsync()).OrderBy(p => p.Id).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await GenerarAsync();
        }

        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            await GenerarAsync();
        }

        private async Task GenerarAsync()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha desde no puede ser posterior a la fecha hasta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var filas = await ApiClient.Reportes.GetRecaudacionMensualAsync(dtpDesde.Value.Date, dtpHasta.Value.Date);
                MostrarTabla(filas);
                DibujarGrafico(filas);
            }
            catch (ApiException ex)
            {
                MessageBox.Show("No se pudo generar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al intentar conectar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarTabla(List<RecaudacionMensualDTO> filas)
        {
            dgvDetalle.DataSource = filas.Select(f => new
            {
                Mes = NombreMes(f.Anio, f.Mes),
                Paseador = f.PaseadorNombre,
                Paseos = f.CantidadPaseos,
                f.Minutos,
                Total = f.Total.ToString("N2")
            }).ToList();

            lblTotalGeneral.Text = filas.Count == 0
                ? "No hay paseos realizados en el período."
                : $"Total recaudado: ${filas.Sum(f => f.Total):N2} en {filas.Sum(f => f.CantidadPaseos)} paseos";
        }

        private void DibujarGrafico(List<RecaudacionMensualDTO> filas)
        {
            var plot = grafico.Plot;
            plot.Clear();
            plot.Legend.ManualItems.Clear();

            var meses = filas
                .Select(f => new DateTime(f.Anio, f.Mes, 1))
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            // Una serie por paseador con datos, en el orden fijo de la lista
            var series = SeriesDelReporte(filas);

            double anchoGrupo = 0.8;
            double anchoBarra = anchoGrupo / Math.Max(series.Count, 1);
            var barras = new List<ScottPlot.Bar>();

            for (int m = 0; m < meses.Count; m++)
            {
                for (int s = 0; s < series.Count; s++)
                {
                    decimal valor = filas
                        .Where(f => f.Anio == meses[m].Year && f.Mes == meses[m].Month && series[s].Ids.Contains(f.PaseadorId))
                        .Sum(f => f.Total);

                    barras.Add(new ScottPlot.Bar
                    {
                        Position = m - anchoGrupo / 2 + anchoBarra * (s + 0.5),
                        Value = (double)valor,
                        // Un poco más angosta que su lugar: deja una separación entre barras vecinas
                        Size = anchoBarra * 0.85,
                        FillColor = series[s].Color,
                        LineWidth = 0
                    });
                }
            }

            if (barras.Count > 0)
                plot.Add.Bars(barras);

            foreach (var serie in series)
            {
                plot.Legend.ManualItems.Add(new ScottPlot.LegendItem { LabelText = serie.Nombre, FillColor = serie.Color });
            }
            // Afuera del área de datos, así no tapa ninguna barra
            plot.ShowLegend(ScottPlot.Edge.Right);

            double[] posiciones = Enumerable.Range(0, meses.Count).Select(i => (double)i).ToArray();
            string[] etiquetas = meses.Select(m => NombreMes(m.Year, m.Month)).ToArray();
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(posiciones, etiquetas);

            plot.Title("Recaudación mensual por paseador");
            plot.YLabel("Recaudado ($)");

            // Las barras arrancan en cero
            plot.Axes.AutoScale();
            plot.Axes.Margins(bottom: 0, top: 0.1);

            grafico.Refresh();
        }

        private record Serie(string Nombre, ScottPlot.Color Color, List<int> Ids);

        private List<Serie> SeriesDelReporte(List<RecaudacionMensualDTO> filas)
        {
            var conDatos = filas.Select(f => f.PaseadorId).Distinct().ToHashSet();
            var series = new List<Serie>();
            var otros = new List<int>();

            for (int i = 0; i < paseadores.Count; i++)
            {
                if (!conDatos.Contains(paseadores[i].Id))
                    continue;

                if (i < PaletaGraficos.Series.Length)
                {
                    var p = paseadores[i];
                    series.Add(new Serie($"{p.Apellido}, {p.Nombre}",
                        ScottPlot.Color.FromHex(PaletaGraficos.Series[i]), new List<int> { p.Id }));
                }
                else
                {
                    otros.Add(paseadores[i].Id);
                }
            }

            // Un paseador que no estaba en la lista (dado de alta recién) también va a "Otros"
            otros.AddRange(conDatos.Where(id => !paseadores.Any(p => p.Id == id)));

            if (otros.Count > 0)
                series.Add(new Serie("Otros", ScottPlot.Color.FromHex(PaletaGraficos.Otros), otros));

            return series;
        }

        private static string NombreMes(int anio, int mes)
        {
            return new DateTime(anio, mes, 1).ToString("MMM yyyy");
        }
    }
}
