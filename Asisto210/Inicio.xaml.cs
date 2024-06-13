using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using static MaterialDesignThemes.Wpf.Theme;

namespace Asisto210
{
    /// <summary> 
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Page
    {
        ConexionBiometrico conexionBiometrico = new ConexionBiometrico();

        private DispatcherTimer timer;
        public Inicio()
        {
            InitializeComponent();            
            encabezadosUR();
            encabezadosER();
            llenadoUltimos();
            llenadoEspera();
            obtenerHoraChecaador();
            obtenderHoraSistema();


        }

        public void llenadoUltimos()
        {

            List<UR> luR = new List<UR>
            {
                new UR { id = "123", nombre = "Mauricio Leoanrdo Aleman Diaz", horaRegistro = "22:00:35",fechaRegistro = "28-05-2024", metodoRegistro = "Huella digital"},
            };

            dvgUltimosRegistros.ItemsSource = luR;

        }       
        
        public void llenadoEspera()
        {

            List<ER> leR = new List<ER>
            {
                new ER { id = "1", nombre = "Mauricio Leoanrdo Aleman Diaz", estadoRegistro = "Ausente"},
            };

            dvgEsperandoRegistro.ItemsSource = leR;

        }

        public void obtencionHora(object sender,EventArgs e)
        {       
                lblHora.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void obtenerHoraChecaador()
        {
            int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;
            bool isTimeRetrieved = conexionBiometrico.Biometrico.GetDeviceTime(conexionBiometrico.iMachineNumber, ref year, ref month, ref day, ref hour, ref minute, ref second);

            if (isTimeRetrieved)
            {
                DateTime deviceTime = new DateTime(year, month, day, hour, minute, second);
                lblHoraChecador.Content = deviceTime.ToString();
            }
            else
            {
                Console.WriteLine("Error al obtener la hora del dispositivo.");
            }
        }

        private void sicronizarHoraChecador()
        {
            DateTime newTime = new DateTime(2024, 6, 3, 14, 30, 0); // Establecer el 3 de junio de 2024 a las 14:30:00

            bool isTimeSet = conexionBiometrico.Biometrico.SetDeviceTime2(conexionBiometrico.iMachineNumber, newTime.Year, newTime.Month, newTime.Day, newTime.Hour, newTime.Minute, newTime.Second);

            if (isTimeSet)
            {
                Console.WriteLine("Hora del dispositivo actualizada correctamente.");
            }
            else
            {
                Console.WriteLine("Error al actualizar la hora del dispositivo.");
            }
            
        }

        private void obtenderHoraSistema()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += obtencionHora;
            timer.Start();
        }

        private void encabezadosER()
        {
            DataGridTextColumn idColumn = new DataGridTextColumn
            {
                Header = " ID ",
                Binding = new Binding("id"),
                Width = 30,
            };
            dvgEsperandoRegistro.Columns.Add(idColumn);

            DataGridTextColumn nombreColumn = new DataGridTextColumn
            {
                Header = " Nombre ",
                Binding = new Binding("nombre"),
            };
            dvgEsperandoRegistro.Columns.Add(nombreColumn);
            
            DataGridTextColumn estadoColumn = new DataGridTextColumn
            {
                Header = " Estdo de registro ",
                Binding = new Binding("estadoRegistro"),
                Width = 100,
            };
            dvgEsperandoRegistro.Columns.Add(estadoColumn);
        }

        private void encabezadosUR()
        {

            DataGridTextColumn idColumn = new DataGridTextColumn
            {
                Header = " ID ",
                Binding = new Binding("id"),
                Width = 30,
            };
    dvgUltimosRegistros.Columns.Add(idColumn);
            
            DataGridTextColumn nombreColumn = new DataGridTextColumn
            {
                Header = " Nombre ",
                Binding = new Binding("nombre"),
            };
    dvgUltimosRegistros.Columns.Add(nombreColumn);

            DataGridTextColumn horaRegistroColumn = new DataGridTextColumn
            {
                Header = " Hora de registro ",
                Binding = new Binding("horaRegistro"),
            };
    dvgUltimosRegistros.Columns.Add(horaRegistroColumn);
                        
            DataGridTextColumn fechaRegistroColumn = new DataGridTextColumn
            {
                Header = " Fecha de registro ",
                Binding = new Binding("fechaRegistro"),
            };
    dvgUltimosRegistros.Columns.Add(fechaRegistroColumn);
                                    
            DataGridTextColumn metodoRegistroColumn = new DataGridTextColumn
            {
                Header = " Metodo de registro ",
                Binding = new Binding("metodoRegistro"),
            };
    dvgUltimosRegistros.Columns.Add(metodoRegistroColumn);
        }

        private void btnSincronizarChacador_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            sicronizarHoraChecador();
            obtenerHoraChecaador();
        }
    }


    public class UR
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string horaRegistro { get; set; }
        public string fechaRegistro { get; set; }
        public string metodoRegistro { get; set; }
    }

    public class ER
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string estadoRegistro { get; set; }
    }
}
