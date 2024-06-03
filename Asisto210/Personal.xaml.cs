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
using System.Collections;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para Personal.xaml
    /// </summary>
    public partial class Personal : Page
    {

        Conexion conexion;

        public Personal()
        {

            conexion = new Conexion();
            InitializeComponent();
            EncabezadosTablaBusquedaPersonal();
            
            //llenadoBusquedaPersonal();
        }
        private void EncabezadosTablaBusquedaPersonal()
        {
            DataGridTextColumn idColumn = new DataGridTextColumn
            {
                Header = " ID ",
                Binding = new Binding("id"),
                Width = 30,
            };
            dvgBusquedaPersonal.Columns.Add(idColumn);

            DataGridTextColumn nombreColumn = new DataGridTextColumn
            {
                Header = " Nombre ",
                Binding = new Binding("nombre"),
            };
            dvgBusquedaPersonal.Columns.Add(nombreColumn);
            
            DataGridTextColumn apellidoPCoulmn = new DataGridTextColumn
            {
                Header = " Apellido paterno ",
                Binding = new Binding("apellido_paterno"),
            };
            dvgBusquedaPersonal.Columns.Add(apellidoPCoulmn);
            
            DataGridTextColumn apellidoMaterno = new DataGridTextColumn
            {
                Header = " Apellido materno ",
                Binding = new Binding("apellido_materno"),
            };
            dvgBusquedaPersonal.Columns.Add(apellidoMaterno);

            DataGridTextColumn rolColumn = new DataGridTextColumn
            {
                Header = " Rol ",
                Binding = new Binding("rol"),
            };
            dvgBusquedaPersonal.Columns.Add(rolColumn);
        }
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            actrualizarPersonal();
        }
        private void actrualizarPersonal()
        {
            llenadoTablaPersonal();
            llenadoCMBRoles();
            llenadoCMBPErsonal();
        }
        private void txtBusquedaPersonal_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBusquedaPersonal.Text = string.Empty;
        }
        private void btnAñadir_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string cvePersonal = "";
            string nombre = "";
            string apellidoPaterno = "";
            string apellidoMaterno = "";
            string rol = "1";

            cvePersonal = obtenerUltimaID();
            nombre = txtNombre.Text;
            apellidoPaterno = txtApellidoPaterno.Text;
            apellidoMaterno = txtApellidoMaterno.Text;
            rol = cmbRoles.SelectedItem.ToString();
            
            if (rol == "Directivo") {
                rol = "1";
            }else if (rol == "Administrdor")
            {
                rol = "2";
            }else if (rol == "Docente")
            {
                rol = "3";
            }

            añadirPersonal(cvePersonal, nombre, apellidoPaterno, apellidoMaterno, rol);
        }
        private void llenadoTablaPersonal()
        {
            string query = "select personal.cve_personal,personal.nombre,personal.apelldio_pateno,personal.apellido_materno,roles.descripcion from personal inner join roles on personal.rol_personal = roles.cve_rol";
            List<TablaBusquedaPersonal> lTablaBusquedaPersonal = new List<TablaBusquedaPersonal>();

            using (var reader = conexion.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    lTablaBusquedaPersonal.Add(new TablaBusquedaPersonal
                    {
                        id = reader["cve_personal"].ToString(),
                        nombre = reader["nombre"].ToString(),
                        apellido_paterno = reader["apelldio_pateno"].ToString(),
                        apellido_materno = reader["apellido_materno"].ToString(),
                        rol = reader["descripcion"].ToString()
                    });
                }
                dvgBusquedaPersonal.ItemsSource = lTablaBusquedaPersonal;
            }
        }
        private void llenadoCMBRoles()
        {
            cmbRoles.Items.Clear();
            string query = "Select descripcion from roles";

            using (var reader = conexion.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    cmbRoles.Items.Add(reader["descripcion"].ToString());
                    cmbRoles_Editar.Items.Add(reader["descripcion"].ToString());
                }
            }
        }
        private void llenadoCMBPErsonal()
        {
            cmbPersonal_Eliminar.Items.Clear();
            string query = "select personal.cve_personal,personal.nombre,personal.apelldio_pateno,personal.apellido_materno,roles.descripcion from personal inner join roles on personal.rol_personal = roles.cve_rol";

            using (var reader = conexion.ExecuteReader(query))
            {
                string personal = "";
                while (reader.Read())
                {
                    personal = "";
                    personal = reader["cve_personal"].ToString() + " " + reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString() + " " + reader["descripcion"].ToString();
                    cmbPersonal_Eliminar.Items.Add(personal);
                    cmbPersonal_Editar.Items.Add(personal);
                }
            }
        }
        private void añadirPersonal(string cvePersonal, string nombre, string apellidoPaterno, string apellidoMaterno, string rol)
        {
            string query = "INSERT INTO personal (cve_personal, nombre, apelldio_pateno, apellido_materno, rol_personal) " +
                   "VALUES (@cve_personal, @nombre, @apelldio_pateno, @apellido_materno, @rol_personal)";

            SqlParameter[] parameters = {
        new SqlParameter("@cve_personal", cvePersonal),
        new SqlParameter("@nombre", nombre),
        new SqlParameter("@apelldio_pateno", apellidoPaterno),
        new SqlParameter("@apellido_materno", apellidoMaterno),
        new SqlParameter("@rol_personal", rol)
    };

            try
            {
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
                llenadoTablaPersonal();
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar
                MessageBox.Show("Error al añadir personal: " + ex.Message);
            }
        }
        private string obtenerUltimaID()
        {
            string query = "SELECT TOP 1 [cve_personal] FROM [dbo].[personal] ORDER BY [cve_personal] DESC";
            string ultimaId = "0";

            using (var reader = conexion.ExecuteReader(query))
            {
                if (reader.Read())
                {
                    ultimaId = reader["cve_personal"].ToString();
                }
            }

            int ultimaIdInt = Convert.ToInt32(ultimaId) + 1;
            string nuevaId = ultimaIdInt.ToString().PadLeft(4, '0');

            return nuevaId;
        }
        private void btnEliminarPersonal_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string cve_eliminar = "0000";
            string cmbEliminar = "";
            cmbEliminar =  cmbPersonal_Eliminar.SelectedItem.ToString();
            cve_eliminar = cmbEliminar.Substring(0, 4);

            string query = "DELETE FROM personal WHERE cve_personal = @cve_personal";

            SqlParameter[] parameters = {
                new SqlParameter("@cve_personal", cve_eliminar)
            };

            try
            {
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
                
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar
                MessageBox.Show("Error al eliminar personal: " + ex.Message);
            }

            actrualizarPersonal();
        }

        private void cmbPersonal_Editar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string cve_editar = "0000";
            string cmbEditar = "";
            cmbEditar = cmbPersonal_Editar.SelectedItem.ToString();
            cve_editar = cmbEditar.Substring(0, 4);

            string query = "select personal.nombre,personal.apelldio_pateno,personal.apellido_materno,roles.descripcion from personal inner join roles on personal.rol_personal = roles.cve_rol where personal.cve_personal = '"+cve_editar+"'";

            using (var reader = conexion.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    txtNombre_Editar.Text = reader["nombre"].ToString();
                    txtApellidoPaterno_Editar.Text = reader["apelldio_pateno"].ToString();
                    txtApellidoMaterno_Editar.Text = reader["apellido_materno"].ToString();
                    if (reader["descripcion"].ToString() == "Directivo")
                    {
                        cmbRoles_Editar.SelectedItem = 1;
                    }
                    else if ((reader["descripcion"].ToString()) == "Administrador")
                    {
                        cmbRoles_Editar.SelectedItem = 2;
                    }
                    else if ((reader["descripcion"].ToString()) == "Docente")
                    {
                        cmbRoles_Editar.SelectedItem = 3;
                    }
                    cmbRoles_Editar.Text = reader["descripcion"].ToString();
                }
            }
        }



    } // End class
    }// End namespace
       


    public class TablaBusquedaPersonal
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string rol { get; set; }
    }