using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para AsignacionAsignatura.xaml
    /// </summary>
    public partial class AsignacionAsignatura : Window
    {
        Conexion conexion;

        public string AsignaturaSeleccionada { get; private set; }

        public AsignacionAsignatura()
        {
            conexion = new Conexion();
            InitializeComponent();
            llenadoCMBAsignatura();

        }

        private void btnAceptar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                String asig = cmbAsignaturas.SelectedItem.ToString();
                this.AsignaturaSeleccionada = asig;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Selecciona una asignatura");
            }
        }

        private void btnCancelar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
        }

        public static string Show(string hora_dia)
        {
            AsignacionAsignatura asignaturaSeleccionada = new AsignacionAsignatura();
            asignaturaSeleccionada.Title = "Sleccione asignatura para: " + hora_dia;

            if (asignaturaSeleccionada.ShowDialog() == true)
            {
                return asignaturaSeleccionada.AsignaturaSeleccionada;
            }
            else
            {
                return null;
            }
        }

        private void llenadoCMBAsignatura()
        {
            cmbAsignaturas.Items.Clear();
            string query = "Select * from meterias";

            using (var reader = conexion.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    cmbAsignaturas.Items.Add(reader["cve_materia"].ToString() + " " + reader["nombre_materia"]);

                }
            }
        }
    }
}
