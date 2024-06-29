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
using System.Xml.Linq;


namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para ASISTO210.xaml
    /// </summary>
    /// 
  
    public partial class ASISTO210 : Window
    {
        //Global gbl = new Global();
        //bool estado = false;
        //private int iMachineNumber = 1;

        private Uri pagInico = new Uri("Inicio.xaml", UriKind.Relative);
        private Uri pagPersonal = new Uri("Personal.xaml", UriKind.Relative);
        private Uri pagReportes = new Uri("Reportes.xaml", UriKind.Relative);
        private Uri pagConfiguracion = new Uri("Configuracion.xaml", UriKind.Relative);
        private Uri pagRegistroDiario = new Uri("RegistroDiario.xaml", UriKind.Relative);

        Conexion conexion = new Conexion();

        string nombre_completo = "";
        string abr = "";
        string rol = "";


        public ASISTO210()
        {

            InitializeComponent();
            encabezado();

        }

        private void encabezado() {

            string query = "select personal.nombre,personal.apelldio_pateno, personal.apellido_materno,roles.descripcion from usuarios_c  inner join personal on personal.cve_personal = usuarios_c.cve_personal inner join roles on personal.rol_personal = roles.cve_rol where usuarios_c.cve_personal = '" + Global.Global_CVE+"'";

            using (var reader = conexion.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    nombre_completo = reader["nombre"].ToString() + " " +reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString();
                    abr = reader["apelldio_pateno"].ToString().Substring(0,1) + "" + reader["apellido_materno"].ToString().Substring(0, 1);
                    rol = reader["descripcion"].ToString();
                }
            }

            lblInicalesUsuario.Content = abr;
            lblNombreUsuario.Content = nombre_completo;
            lblRolUsuario.Content = rol;

        }


        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            frmContenido.Source = pagInico;
            lblContenedor.Content = "Asisto210 / Inicio";
        }

        private void ListViewItem_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            
            frmContenido.Source = pagPersonal;
            lblContenedor.Content = "Asisto210 / Personal";

        }

        private void ListViewItem_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            frmContenido.Source = pagReportes;
            lblContenedor.Content = "Asisto210 / Reportes";
        }

        private void ListViewItem_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            frmContenido.Source = pagConfiguracion;
            lblContenedor.Content = "Asisto210 / Configuración";
        }

        private void ListViewItem_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            frmContenido.Source = pagRegistroDiario;
            lblContenedor.Content = "Asisto210 / Registro diario";
        }

        private void ListViewItem_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void lblClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void lblMin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
