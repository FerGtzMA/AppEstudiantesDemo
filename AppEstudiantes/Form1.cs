using Logica;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq; //
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEstudiantes
{
    public partial class Form1 : Form
    {
        private Estudiantes estudiante;
        // private Libreria libreria;
        public Form1()
        {
            InitializeComponent();

            //libreria = new Libreria();

            var listTextBox = new List<TextBox>();
            listTextBox.Add(txtNid);
            listTextBox.Add(txtNombre);
            listTextBox.Add(txtApellido);
            listTextBox.Add(txtEmail);

            var listLabels = new List<Label>();
            listLabels.Add(lblNid);
            listLabels.Add(lblNombre);
            listLabels.Add(lblApellido);
            listLabels.Add(lblEmail);
            listLabels.Add(lblPaginas);

            Object[] objetos = { 
                picEstudiante, 
                Properties.Resources.baseline_person_white_36dp, 
                tableGridEstudiantes, 
                numericPages 
            };

            estudiante = new Estudiantes(listTextBox, listLabels, objetos);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void picEstudiante_Click(object sender, EventArgs e)
        {
            estudiante.uploadImage.CargarImagen(picEstudiante); //
        }

        private void txtNid_TextChanged(object sender, EventArgs e)
        {
            if (txtNid.Text.Equals(""))
            {
                lblNid.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblNid.ForeColor = Color.Green;
                lblNid.Text = "Nid";
            }
        }

        private void txtNid_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvents.numberKeyPress(e); //
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre.Text.Equals(""))
            {
                lblNombre.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblNombre.ForeColor = Color.Green;
                lblNombre.Text = "Nombre";
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvents.textKeyPress(e); //
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            if (txtApellido.Text.Equals(""))
            {
                lblApellido.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblApellido.ForeColor = Color.Green;
                lblApellido.Text = "Apellido";
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvents.textKeyPress(e); //
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.Equals(""))
            {
                lblEmail.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblEmail.ForeColor = Color.Green;
                lblEmail.Text = "Email";
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            estudiante.Registrar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            estudiante.SearchEstudiantes(txtBuscar.Text);
        }

        private void btnPreviewPage_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Primero");
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Anterior");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Siguiente");
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Ultimo");
        }

        private void numericPages_ValueChanged(object sender, EventArgs e)
        {
            estudiante.RegistroPaginas();
        }

        private void tableGridEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tableGridEstudiantes.Rows.Count != 0)
                estudiante.getEstudiante();
        }

        private void tableGridEstudiantes_KeyUp(object sender, KeyEventArgs e)
        {
            if (tableGridEstudiantes.Rows.Count != 0)
                estudiante.getEstudiante();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            estudiante.Restablecer();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            estudiante.Eliminar();
        }
    }
}
