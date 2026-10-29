using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paseadores.WinForms
{
    public partial class FormPerros : Form
    {
        private int idPerroSeleccionado = 0;
        private List<PerroDTO> perrosCargados = new List<PerroDTO>();

        // Un dueño solo ve y carga sus propios perros (la API igual lo controla)
        private readonly UsuarioDTO usuario;
        private bool EsDueno => usuario.Rol == "Dueno";


        public FormPerros(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            usuario = usuarioLogueado;
        }

        private async void FormPerros_Load(object sender, EventArgs e)
        {
            dgvPerros.AutoGenerateColumns = false;
            await CargarDuenosAsync();
            await CargarPerrosAsync();
        }

        // Los dueños se cargan en el combo para no tener que escribir el Id a mano
        private async Task CargarDuenosAsync()
        {
            try
            {
                List<DuenoDTO> duenos = await ApiClient.Duenos.GetAllAsync();

                if (EsDueno)
                    duenos = duenos.Where(d => d.Id == usuario.Id).ToList();

                cmbDueno.DataSource = duenos
                    .Select(d => new { d.Id, Descripcion = d.Apellido + ", " + d.Nombre })
                    .ToList();
                cmbDueno.DisplayMember = "Descripcion";
                cmbDueno.ValueMember = "Id";
                cmbDueno.SelectedIndex = -1;
                FijarDuenoSiCorresponde();
            }
            catch (ApiException ex) when (ex.StatusCode == 401)
            {
                ApiClient.SesionVencida();
            }
            catch (ApiException ex)
            {
                // La API respondió, pero con un error
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                // No hubo respuesta: la API está apagada o la URL está mal
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Si es dueño, el combo queda fijo en él: no puede cargarle perros a otro
        private void FijarDuenoSiCorresponde()
        {
            if (!EsDueno) return;

            cmbDueno.SelectedValue = usuario.Id;
            cmbDueno.Enabled = false;
        }

        private async Task CargarPerrosAsync()
        {
            try
            {
                // El dueño usa el endpoint anidado /duenos/{id}/perros
                perrosCargados = EsDueno
                    ? await ApiClient.Perros.GetByDuenoAsync(usuario.Id)
                    : await ApiClient.Perros.GetAllAsync();

                dgvPerros.DataSource = perrosCargados.Select(p => new
                {
                    p.Id,
                    Dueno = p.DuenoNombre,
                    p.Nombre,
                    p.Raza,
                    p.Edad
                }).ToList();
            }
            catch (ApiException ex) when (ex.StatusCode == 401)
            {
                ApiClient.SesionVencida();
            }
            catch (ApiException ex)
            {
                // La API respondió, pero con un error
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                // No hubo respuesta: la API está apagada o la URL está mal
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbDueno.SelectedValue == null)
            {
                MessageBox.Show("Seleccioná un dueño para el perro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoPerro = new PerroDTO
            {
                Id = idPerroSeleccionado,
                DuenoId = Convert.ToInt32(cmbDueno.SelectedValue),
                Nombre = txtNombre.Text,
                Raza = txtRaza.Text,
                Edad = (int)nudEdad.Value
            };

            try
            {
                if (idPerroSeleccionado == 0)
                    await ApiClient.Perros.AddAsync(nuevoPerro);
                else
                    await ApiClient.Perros.UpdateAsync(nuevoPerro);

                MessageBox.Show("¡Perro guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                await CargarPerrosAsync();
                dgvPerros.ClearSelection();
            }
            catch (ApiException ex) when (ex.StatusCode == 401)
            {
                ApiClient.SesionVencida();
            }
            catch (ApiException ex)
            {
                // La API rechazó el guardado (validación, etc.) con su mensaje real
                MessageBox.Show("La API rechazó el guardado.\n\nDetalles:\n" + ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
            dgvPerros.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtRaza.Clear();
            nudEdad.Value = 0;
            cmbDueno.SelectedIndex = -1;
            FijarDuenoSiCorresponde();
            idPerroSeleccionado = 0;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPerros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccioná un perro de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar a este perro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvPerros.SelectedRows[0].Cells["colId"].Value);
                    await ApiClient.Perros.DeleteAsync(idSeleccionado);

                    MessageBox.Show("Perro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarPerrosAsync();
                }
                catch (ApiException ex) when (ex.StatusCode == 401)
                {
                    ApiClient.SesionVencida();
                }
                catch (ApiException ex)
                {
                    MessageBox.Show("La API rechazó la eliminación.\n\nDetalles:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intentar conectar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPerros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvPerros.Rows[e.RowIndex].Cells["colId"].Value);
            PerroDTO? perro = perrosCargados.FirstOrDefault(p => p.Id == id);
            if (perro == null) return;

            idPerroSeleccionado = perro.Id;
            cmbDueno.SelectedValue = perro.DuenoId;
            txtNombre.Text = perro.Nombre;
            txtRaza.Text = perro.Raza;
            nudEdad.Value = perro.Edad;
        }
    }
}
