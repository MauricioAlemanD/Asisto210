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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para Materias.xaml
    /// </summary>
    public partial class Materias : Page
    {
        Conexion conexion;

        public Materias()
        {
            conexion = new Conexion();
            InitializeComponent();
            EncabezadosTablaMaterias();
            llenadoTablaMaterias();
            llenadoCMBAsignatura();
        }

        private void Actualizar()
        {
            llenadoTablaMaterias();
            llenadoCMBAsignatura();
        }

        private void EncabezadosTablaMaterias()
        {
            dvgAsugnaturas.Columns.Add(new DataGridTextColumn { Header = "Clave Materia", Binding = new Binding("claveMateria") });
            dvgAsugnaturas.Columns.Add(new DataGridTextColumn { Header = "Nombre Materia", Binding = new Binding("nombreMateria") });
            dvgAsugnaturas.Columns.Add(new DataGridTextColumn { Header = "Abreviatura Materia", Binding = new Binding("acrMateria") });
        }

        private void llenadoTablaMaterias()
        {
            List<TablaMaterias> lTablaMaterias = new List<TablaMaterias>();
            string query = "select * from meterias";
            using (var reader = conexion.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    lTablaMaterias.Add(new TablaMaterias
                    {
                        claveMateria = reader["cve_materia"].ToString(),
                        nombreMateria = reader["nombre_materia"].ToString(),
                        acrMateria = reader["acronimo_materia"].ToString(),
                    });
                }
                dvgAsugnaturas.ItemsSource = lTablaMaterias;

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

        private void añadirMateria(string cveMateria, string nombreMateria, string acronimoMateria)
        {
            string query = "INSERT INTO meterias (cve_materia, nombre_materia,acronimo_materia) " +
                   "VALUES (@cve_materia, @nombre_materia, @acronimo_materia)";

            SqlParameter[] parameters = {
        new SqlParameter("@cve_materia", cveMateria),
        new SqlParameter("@nombre_materia", nombreMateria),
        new SqlParameter("@acronimo_materia", acronimoMateria),
    };

            try
            {
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Se agregó correctamente la asignatura.");
                Actualizar();
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar
                MessageBox.Show("Error al añadir asignatura: " + ex.Message);
            }
        }

        private void eliminarAsignatura()
        {
            try
            {
                string cve_eliminar = "0000";
                string cmbAsignaturaAEliminar = "0";
                cmbAsignaturaAEliminar = cmbAsignaturas.SelectedItem.ToString();
                cve_eliminar = cmbAsignaturaAEliminar.Substring(0, 4);

                string query = "DELETE FROM meterias WHERE cve_materia = @cve_materia";

                SqlParameter[] parameters = {
                new SqlParameter("@cve_materia", cve_eliminar)
            };

                try
                {
                    // Pasar el array de parámetros a ExecuteNonQuery
                    conexion.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Asignatura eliminada.");
                    Actualizar();
                }
                catch (Exception ex)
                {
                    // Manejo de excepción, mostrar el error o registrar
                    MessageBox.Show("Error al eliminar la asignatura: " + ex.Message);
                }
            }
            catch
            {

            }
        }

        private void btnAñadirAsignatura_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            añadirMateria(txtClaveMateria.Text.ToString(),txtNombreMateria.Text.ToString(),txtAbreviaturaMateria.Text.ToString());
        }

        private void btnEliminarAsignatura_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            eliminarAsignatura();
        }
    }

    public class TablaMaterias
    {
        public string claveMateria { get; set; }
        public string nombreMateria { get; set; }
        public string acrMateria { get; set; }
    }

}
