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
    /// Lógica de interacción para ASISTO210.xaml
    /// </summary>
    public partial class ASISTO210 : Window
    {
        Global gbl = new Global();
        
        public ASISTO210()
        {
            InitializeComponent();
            lblSaludo.Content = lblSaludo.Content + " " + gbl.Usuario;
            estadoConexion();
            
        }

        private void estadoConexion()
        {
            bool estado = gbl.Biometrico.Connect_Net("192.168.0.45",4370);
            if (estado == true)
            {
                lblEstado.Visibility = Visibility.Visible;
            }
        }

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lblContenedor.Content = "Inicio";
        }

        private void ListViewItem_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            lblContenedor.Content = "Personal";
        }

        private void ListViewItem_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            lblContenedor.Content = "Reportes";
        }

        private void ListViewItem_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            lblContenedor.Content = "Configuración";
        }

        private void ListViewItem_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            lblContenedor.Content = "Reporte diario";
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
    }
}
