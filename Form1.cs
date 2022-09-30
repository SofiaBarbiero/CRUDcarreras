using CRUDcarreras.Datos;
using CRUDcarreras.Dominio;
using CRUDcarreras.Servicios.Implementacion;
using CRUDcarreras.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDcarreras
{
    public partial class FrmAlta : Form
    {
        private IServicio gestor;
        private Carrera nueva;
        public FrmAlta()
        {
            InitializeComponent();
            gestor = new Servicio();
            nueva = new Carrera();
        }

        private void FrmAlta_Load(object sender, EventArgs e)
        {
            ObtenerProximo();
            ObtenerMaterias();

        }

        private void ObtenerMaterias()
        {
            cboMaterias.ValueMember = "IdMateria";
            cboMaterias.DisplayMember = "Nombre";
            cboMaterias.DataSource = gestor.ObtenerMaterias();
            cboMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ObtenerProximo()
        {
            int next = gestor.ObtenerProximo();
            if(next > 0)
            {
                lblNext.Text = "Carrera N°: " + next.ToString();
            }
            else
            {
                MessageBox.Show("No se pudo obtener el numero de carrera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cboMaterias.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una materia", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if(txtAnio.Text == "")
            {
                MessageBox.Show("Ingrese un año de cursado", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if(txtCuat.Text == "")
            {
                MessageBox.Show("Ingrese un cuatrimestre", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            int anio = int.Parse(txtAnio.Text);
            int cuat = int.Parse(txtCuat.Text);
            Materia m = (Materia)cboMaterias.SelectedItem;
            DetalleCarrera d = new DetalleCarrera(anio, cuat, m);
            nueva.AgregarDetalle(d);
            dgvCarreras.Rows.Add(d.Materia.IdMateria, d.Materia.Nombre, d.AnioCursado, d.Cuatrimestre);

            txtCantidad.Text = nueva.CalcularTotal().ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese un nombre de carrera", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if(txtTitulo.Text == "")
            {
                MessageBox.Show("Ingrese un titulo", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            GuardarCarrera();
        }

        private void GuardarCarrera()
        {
            nueva.Nombre = txtNombre.Text;
            nueva.Titulo = txtTitulo.Text;
            if (Helper.ObtenerInstancia().ConfirmarCarrera(nueva))
            {
                MessageBox.Show("Se registro la carrera con exito!", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("No se pudo insertar la carrera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
