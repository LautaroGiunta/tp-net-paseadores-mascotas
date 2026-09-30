using API.Clients;
using DTOs;

namespace Paseadores.WinForms
{
    // Reporte tabular: cuánto salió a pasear cada perro en el período y cuánto gastó su dueño.
    // Aparecen también los perros que no salieron (con cero). Consulta ADO.NET en la API.
    public partial class FormReporteActividad : Form
    {
        public FormReporteActividad()
        {
            InitializeComponent();
        }

        private async void FormReporteActividad_Load(object sender, EventArgs e)
        {
            dgvActividad.AutoGenerateColumns = false;

            // Por defecto, el mes en curso
            dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpHasta.Value = DateTime.Today;

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
                List<ActividadPerroDTO> filas = await ApiClient.Reportes.GetActividadPerrosAsync(dtpDesde.Value.Date, dtpHasta.Value.Date);

                dgvActividad.DataSource = filas.Select(f => new
                {
                    Perro = f.PerroNombre,
                    f.Raza,
                    Dueno = f.DuenoNombre,
                    Paseos = f.CantidadPaseos,
                    f.Minutos,
                    Total = f.Total.ToString("N2"),
                    UltimoPaseo = f.UltimoPaseo.HasValue ? f.UltimoPaseo.Value.ToString("dd/MM/yyyy HH:mm") : "Sin paseos"
                }).ToList();

                int sinPaseos = filas.Count(f => f.CantidadPaseos == 0);
                lblResumen.Text = $"{filas.Sum(f => f.CantidadPaseos)} paseos, ${filas.Sum(f => f.Total):N2} en total" +
                                  (sinPaseos > 0 ? $" · {sinPaseos} perro(s) sin paseos" : "");
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
    }
}
