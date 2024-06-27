using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para MensajePersonalizado.xaml
    /// </summary>
    public partial class MensajePersonalizado : Window
    {
        public MensajePersonalizado(string message,string titulo)
        {
            InitializeComponent();
            MessageText.Text = message;
            Title = titulo;
        }

        private void btnAcpetar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancelar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        //private void OkButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.DialogResult = true;
        //    this.Close();
        //}

        //private void CancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.DialogResult = false;
        //    this.Close();
        //}
    }
}
