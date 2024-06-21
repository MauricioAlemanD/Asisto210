using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para InputMessageBox.xaml
    /// </summary>
    public partial class InputMessageBox : Window
    {

        public string InputText { get; private set; }

        public InputMessageBox()
        {
            InitializeComponent();
            txtInputHora.Focus();
        }

        private void btnAceptar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string input = txtInputHora.Text;
            string pattern = @"^(?:[01]\d|2[0-3]):[0-5]\d\s?(AM|PM)?$";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(input))
            {
                this.InputText = input;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Ingresa un formato de hora correcto");
            }
        }

        private void btnCancelar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
        }

        public static string Show(string hora)
        {
            InputMessageBox inputMessageBox = new InputMessageBox();
            inputMessageBox.Title = "Ingrese hora para: " + hora;           

            if (inputMessageBox.ShowDialog() == true)
            {
                return inputMessageBox.InputText;
            }
            else
            {
                return null;
            }
        }

        private void txtInputHora_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtInputHora.Text != "")
            {
                if (e.Key == Key.Enter)
                {
                    string input = txtInputHora.Text;
                    string pattern = @"^(?:[01]\d|2[0-3]):[0-5]\d\s?(AM|PM)?$";
                    Regex regex = new Regex(pattern);

                    if (regex.IsMatch(input))
                    {
                        this.InputText = input;
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Ingresa un formato de hora correcto");
                    }
                }
            }
        }
    }
}
