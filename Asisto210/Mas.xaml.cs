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

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para Mas.xaml
    /// </summary>
    public partial class Mas : Page
    {
        public Mas()
        {
            InitializeComponent();
        }

        private void btnCCD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CCD.Visibility == Visibility.Hidden)
            {
                CCD.Visibility = Visibility.Visible;
                CCS.Visibility = Visibility.Hidden;
            }
            else
            {
                CCD.Visibility = Visibility.Hidden;
            }
        }

        private void btnCCS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CCS.Visibility == Visibility.Hidden)
            {
                CCS.Visibility = Visibility.Visible;
                CCD.Visibility = Visibility.Hidden;
            }
            else
            {
                CCS.Visibility = Visibility.Hidden;
            }
        }
    }
}
