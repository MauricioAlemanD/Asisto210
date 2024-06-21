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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para HorarioProfesor.xaml
    /// </summary>
    public partial class HorarioProfesor : Page
    {

        public String SHora1 = "";
        public String SHora2 = "";
        public String SHora3 = "";
        public String SHora4 = "";
        public String SHora5 = "";
        public String SHora6 = "";
        public String SHora7 = "";
        public String SHora8 = "";
        public String SHora9 = "";

        //Lunes
        public String SHora1Lunes = "";
        public String SHora2Lunes = "";
        public String SHora3Lunes = "";
        public String SHora4Lunes = "";
        public String SHora5Lunes = "";
        public String SHora6Lunes = "";
        public String SHora7Lunes = "";
        public String SHora8Lunes = "";
        public String SHora9Lunes = "";

        // Martes
        public String SHora1Martes = "";
        public String SHora2Martes = "";
        public String SHora3Martes = "";
        public String SHora4Martes = "";
        public String SHora5Martes = "";
        public String SHora6Martes = "";
        public String SHora7Martes = "";
        public String SHora8Martes = "";
        public String SHora9Martes = "";

        // Miércoles
        public String SHora1Miercoles = "";
        public String SHora2Miercoles = "";
        public String SHora3Miercoles = "";
        public String SHora4Miercoles = "";
        public String SHora5Miercoles = "";
        public String SHora6Miercoles = "";
        public String SHora7Miercoles = "";
        public String SHora8Miercoles = "";
        public String SHora9Miercoles = "";

        // Jueves
        public String SHora1Jueves = "";
        public String SHora2Jueves = "";
        public String SHora3Jueves = "";
        public String SHora4Jueves = "";
        public String SHora5Jueves = "";
        public String SHora6Jueves = "";
        public String SHora7Jueves = "";
        public String SHora8Jueves = "";
        public String SHora9Jueves = "";

        // Viernes
        public String SHora1Viernes = "";
        public String SHora2Viernes = "";
        public String SHora3Viernes = "";
        public String SHora4Viernes = "";
        public String SHora5Viernes = "";
        public String SHora6Viernes = "";
        public String SHora7Viernes = "";
        public String SHora8Viernes = "";
        public String SHora9Viernes = "";

        public HorarioProfesor()
        {
            InitializeComponent();
        }

        private void btnAñadirAsignatura_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Hora1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora1 = "";
            SHora1 = InputMessageBox.Show("Hora 1");
            if (SHora1 != null)
            {
                Hora1.Content = SHora1;
            }
            else
            {

            }

        }

        private void Hora2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora2 = "";
            SHora2 = InputMessageBox.Show("Hora 2");
            if (SHora2 != null)
            {
                Hora2.Content = SHora2;
            }
            else
            {

            }
        }

        private void Hora3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora3 = "";
            SHora3 = InputMessageBox.Show("Hora 3");
            if (SHora3 != null)
            {
                Hora3.Content = SHora3;
            }
            else
            {

            }
        }

        private void Hora4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora4 = "";
            SHora4 = InputMessageBox.Show("Hora 4");
            if (SHora4 != null)
            {
                Hora4.Content = SHora4;
            }
            else
            {

            }
        }

        private void Hora5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora5 = "";
            SHora5 = InputMessageBox.Show("Hora 5");
            if (SHora5 != null)
            {
                Hora5.Content = SHora5;
            }
            else
            {

            }
        }

        private void Hora6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora6 = "";
            SHora6 = InputMessageBox.Show("Hora 6");
            if (SHora6 != null)
            {
                Hora6.Content = SHora6;
            }
            else
            {

            }
        }

        private void Hora7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora7 = "";
            SHora7 = InputMessageBox.Show("Hora 7");
            if (SHora7 != null)
            {
                Hora7.Content = SHora7;
            }
            else
            {

            }
        }

        private void Hora8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora8 = "";
            SHora8 = InputMessageBox.Show("Hora 8");
            if (SHora8 != null)
            {
                Hora8.Content = SHora8;
            }
            else
            {

            }
        }

        private void Hora9_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora9 = "";
            SHora9 = InputMessageBox.Show("Hora 9");
            if (SHora9 != null)
            {
                Hora9.Content = SHora9;
            }
            else
            {

            }
        }

        private void Hora1Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora1Lunes = "";
            SHora1Lunes = AsignacionAsignatura.Show("Hora 1 de Lunes");

            if (SHora1Lunes != null)
            {
                Hora1Lunes.Content = SHora1Lunes;
            }
            else
            {

            }

        }
    }
}
