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
using System.Data.SqlClient;

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para Mas.xaml
    /// </summary>
    public partial class Mas : Page
    {
        Conexion conexion = new Conexion();

        Color colorRojo = (Color)ColorConverter.ConvertFromString("#FF4E1417");

        public Mas()
        {
            InitializeComponent();

            btnCCD.BorderBrush = new SolidColorBrush(colorRojo);
            btnCCS.BorderBrush = new SolidColorBrush(Colors.Transparent);
            btnCCU.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void btnCCD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CCD.Visibility == Visibility.Hidden)
            {
                CCD.Visibility = Visibility.Visible;
                btnCCD.BorderBrush = new SolidColorBrush(colorRojo);
                CCS.Visibility = Visibility.Hidden;
                btnCCS.BorderBrush = new SolidColorBrush(Colors.Transparent);
                CCU.Visibility = Visibility.Hidden;
                btnCCU.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                CCD.Visibility = Visibility.Hidden;
                btnCCD.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void btnCCS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CCS.Visibility == Visibility.Hidden)
            {
                CCS.Visibility = Visibility.Visible;
                btnCCS.BorderBrush = new SolidColorBrush(colorRojo);
                CCD.Visibility = Visibility.Hidden;
                btnCCD.BorderBrush = new SolidColorBrush(Colors.Transparent);
                CCU.Visibility = Visibility.Hidden;
                btnCCU.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                CCS.Visibility = Visibility.Hidden;
                btnCCS.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void btnCCU_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CCU.Visibility == Visibility.Hidden)
            {
                CCU.Visibility = Visibility.Visible;
                btnCCU.BorderBrush = new SolidColorBrush(colorRojo);
                CCD.Visibility = Visibility.Hidden;
                btnCCD.BorderBrush = new SolidColorBrush(Colors.Transparent);
                CCS.Visibility = Visibility.Hidden;
                btnCCS.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                CCU.Visibility = Visibility.Hidden;
                btnCCU.BorderBrush = new SolidColorBrush(Colors.Transparent);                
            }
        }

        private void CCU_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            consultaLlenado();

        }

        private void consultaLlenado()
        {
            string query = "select personal.nombre,personal.apelldio_pateno, personal.apellido_materno,roles.descripcion,usuarios_c.nombre_usuario from usuarios_c  inner join personal on personal.cve_personal = usuarios_c.cve_personal inner join roles on personal.rol_personal = roles.cve_rol where usuarios_c.cve_personal = '" + Global.Global_CVE + "'";

            using (var reader = conexion.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    lblClavePersonal.Content = "Clave de personal: " + Global.Global_CVE;
                    lblNombrePersonal.Content = "Nombre de personal: " + reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString();
                    lblNombreUsuario.Content = "Nombre de usuario: " + reader["nombre_usuario"].ToString();
                    lblRolPersonal.Content = "Rol: " + reader["descripcion"].ToString();

                    break;
                }
            }
        }
        private void limpiaActualizar()
        {
            txtActualizarNUsuario.Text = "";
            txtActualizarCUsuaio.Password = "";
        }

        private void btnActualizarNUsuario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(txtActualizarNUsuario.Text != ""){
                string usuario = txtActualizarNUsuario.Text;

                string query = "UPDATE [dbo].[usuarios_c] SET [nombre_usuario] = @usuario WHERE cve_personal = @cve_personal";

                SqlParameter[] parameters = {
                   new SqlParameter("@cve_personal", Global.Global_CVE),
                   new SqlParameter("@usuario", usuario),
                };

                try
                {
                    // Pasar el array de parámetros a ExecuteNonQuery
                    conexion.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Se actualizó el usuario: " + Global.Global_CVE);
                    consultaLlenado();
                }
                catch (Exception ex)
                {
                    // Manejo de excepción, mostrar el error o registrar
                    MessageBox.Show("Error al actualizar usuario: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("El campo para actualizar el usaurio esta vacío.");
            }
            
        }

        private void btnActualizarCUsuario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string contraseña = txtActualizarCUsuaio.Password;

            string query = "UPDATE [dbo].[usuarios_c] SET [contraseña] = @contraseña WHERE cve_personal = @cve_personal";

            SqlParameter[] parameters = {
                   new SqlParameter("@cve_personal", Global.Global_CVE),
                   new SqlParameter("@contraseña", contraseña),
                };

            try
            {
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Se actualizó la contraseña: " + Global.Global_CVE);
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar
                MessageBox.Show("Error al actualizar la contraseña: " + ex.Message);
            }
        }
    }
}
