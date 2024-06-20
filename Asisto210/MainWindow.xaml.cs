using Microsoft.IdentityModel.Tokens;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }

        private void txtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsuario.Text = "";
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lblContraseña.Visibility = Visibility.Hidden;
            txtContraseña.Focus();
        }

        private void btnIngresar_MouseEnter(object sender, MouseEventArgs e)
        {
            bBtnIngresar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD45C5C"));
        }

        private void btnIngresar_MouseLeave(object sender, MouseEventArgs e)
        {
            bBtnIngresar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4E1417"));
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Label_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void txtContraseña_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            lblContraseña.Visibility= Visibility.Hidden;
        }

        private void btnIngresar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Password;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBoxPersonalizado("Por favor, ingrese el usuario y la contraseña.","Alerta inicio de sesón");
            }
            else
            {
                // Aquí puedes agregar la lógica para manejar la autenticación
                if (usuario == "admin" && contraseña == "password")
                {
                    ASISTO210 aSISTO210 = new ASISTO210();
                    aSISTO210.Show();
                    this.Close();
                }
                else
                {
                    MessageBoxPersonalizado("Por favor, ingrese el usuario y la contraseña correctos.","Alerta inicio de sesón");
                }
            }
        }

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void MessageBoxPersonalizado(string mensajealerta, string titulo)
        {
            MensajePersonalizado messageBox = new MensajePersonalizado(mensajealerta,titulo);
            bool? result = messageBox.ShowDialog();

            if (result == true)
            {

            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ASISTO210 aSISTO210 = new ASISTO210();
            aSISTO210.Show();
            this.Close();
        }
    }
}