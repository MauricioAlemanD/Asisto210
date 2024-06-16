using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;
using System.Globalization;

namespace Asisto210
{
    /// <summary> 
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Page
    {
        Conexion conexion;
        ConexionBiometrico conexionBiometrico = new ConexionBiometrico();
        
        public int n_ultimo_registro_bd = 0;
        public DateTime fechaBusqueda;        

        List<UR> lUR = new List<UR>();

        private DispatcherTimer timer;
        private DispatcherTimer timer1;        

        public Inicio()
        {
            conexion = new Conexion();
            InitializeComponent();            
            encabezadosUR();
            encabezadosER();
            obtenerUltimoRegistro();
            llenadoUltimos();
            llenadoEspera();
            obtenerDia();
            llenadoTablaUltimos();
            obtenerHoraChecaador();
            obtenderHoraSistema();

        }

        public void llenadoUltimos()
        {
                  
            if (conexionBiometrico.estado == true)
            {
                string sdwEnrollNumber = "";
                int idwVerifyMode = 0;
                int idwInOutMode = 0;
                int idwYear = 0;
                int idwMonth = 0;
                int idwDay = 0;
                int idwHour = 0;
                int idwMinute = 0;
                int idwSecond = 0;
                int idwWorkcode = 0;

                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = true;

                int idwErrorCode = 0;
                int iGLCount = 0;
                int iIndex = 1;

                conexionBiometrico.Biometrico.EnableDevice(conexionBiometrico.iMachineNumber, false);

                if (conexionBiometrico.Biometrico.ReadGeneralLogData(conexionBiometrico.iMachineNumber))
                {
                    while (conexionBiometrico.Biometrico.SSR_GetGeneralLogData(conexionBiometrico.iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                           out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                    {
                        iGLCount++;

                        if (conexionBiometrico.Biometrico.SSR_GetUserInfo(conexionBiometrico.iMachineNumber, sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                        {
                            var fechaHora = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                            string Metodo = "";

                            if (idwVerifyMode.ToString() == "3")
                            {
                                Metodo = "Pin";
                            }
                            else if (idwVerifyMode.ToString() == "1")
                            {
                                Metodo = "Huella";
                            }
                            else if (idwVerifyMode.ToString() == "15")
                            {
                                Metodo = "Facial";
                            }

                            string nuevaId = sdwEnrollNumber.ToString().PadLeft(4, '0');

                            if (iIndex > n_ultimo_registro_bd || n_ultimo_registro_bd == 0)
                            {
                                cargaRegistros(nuevaId, TimeSpan.Parse(fechaHora.ToString("HH:mm:ss")), DateTime.Parse($"{idwDay}-{idwMonth}-{idwYear}"), Metodo, "ESS");
                            }
                            


                            //luR.Add(new UR
                            //{
                            //    id = sdwEnrollNumber,
                            //    nombre = sName,
                            //    horaRegistro = fechaHora.ToString("HH:mm:ss"),
                            //    fechaRegistro = $"{idwDay}-{idwMonth}-{idwYear}",
                            //    metodoRegistro = Metodo
                            //});
                                                        
                        }



                        //lsvEntradas.Items.Add(iGLCount.ToString());
                        //lsvEntradas.Items.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
                        //lsvEntradas.Items.Add(idwVerifyMode.ToString());
                        //lsvEntradas.Items.Add(idwInOutMode.ToString());
                        //lsvEntradas.Items.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
                        //lsvEntradas.Items.Add(idwWorkcode.ToString());
                        iIndex++;

                        //var FechaHora = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                        
                        

                    }

                    //Guardar el ultimo registro del checador
                    guardaUltimoRegistro(iIndex-1);
                }
            }


            //bool llenadoUltimosRegistros = false;
            //llenadoUltimosRegistros = true ;

            //List<UR> luR2 = new List<UR>
            //{
            //    new UR { id = "123", nombre = "Mauricio Leoanrdo Aleman Diaz", horaRegistro = "22:00:35",fechaRegistro = "28-05-2024", metodoRegistro = "Huella digital"},
            //};
            
            //////dvgUltimosRegistros.ItemsSource = luR;           

        }               
        public void llenadoEspera()
        {

            List<ER> leR = new List<ER>
            {
                new ER { id = "1", nombre = "Mauricio Leoanrdo Aleman Diaz", estadoRegistro = "Ausente"},
            };

            dvgEsperandoRegistro.ItemsSource = leR;

        }
        public void llenadoTablaUltimos()
        {
            int n = 1;
            string query = "select ultimos_ingresos.cve_personal,personal.nombre,personal.apelldio_pateno,personal.apellido_materno,ultimos_ingresos.hora_registro,ultimos_ingresos.fecha_registro,ultimos_ingresos.metodo_verificacion from ultimos_ingresos inner join personal on ultimos_ingresos.cve_personal = personal.cve_personal ";
            

            using (var reader = conexion.ExecuteReader(query))
            {
                
                while (reader.Read())
                {
                    lUR.Add(new UR
                    {
                        id_registro = n,
                        id = reader["cve_personal"].ToString(),
                        nombre = reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString(),
                        horaRegistro = reader["hora_registro"].ToString(),
                        fechaRegistro = reader["fecha_registro"].ToString(),
                        metodoRegistro = reader["metodo_verificacion"].ToString()
                    });
                    n++;
                }
                dvgUltimosRegistros.ItemsSource = lUR;
            }
        }        
        public void llenadoTablaUltimosBusqueda(DateTime fecha_seleccionada_busqueda)
        {
            string query = "SELECT ultimos_ingresos.cve_personal, personal.nombre, personal.apelldio_pateno, " +
                           "personal.apellido_materno, ultimos_ingresos.hora_registro, " +
                           "ultimos_ingresos.fecha_registro, ultimos_ingresos.metodo_verificacion " +
                           "FROM ultimos_ingresos " +
                           "INNER JOIN personal ON ultimos_ingresos.cve_personal = personal.cve_personal " +
                           "WHERE ultimos_ingresos.fecha_registro = @fechaBusqueda";

            // Parámetro para la fecha
            SqlParameter parameter = new SqlParameter("@fechaBusqueda", System.Data.SqlDbType.Date);
            parameter.Value = fecha_seleccionada_busqueda;

            // Lista para almacenar los resultados
            List<UR> lUR = new List<UR>();

            // Ejecutar la consulta con parámetros
            using (var reader = conexion.ExecuteParametrizedReader(query, parameter))
            {
                while (reader.Read())
                {
                    lUR.Add(new UR
                    {
                        id = reader["cve_personal"].ToString(),
                        nombre = reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString(),
                        horaRegistro = reader["hora_registro"].ToString(),
                        fechaRegistro = reader["fecha_registro"].ToString(),
                        metodoRegistro = reader["metodo_verificacion"].ToString()
                    });
                }
                dvgUltimosRegistros.ItemsSource = lUR;
            }
        }
        //Lenados
        public void obtencionHora(object sender,EventArgs e)
        {       
            lblHora.Content = DateTime.Now.ToString("dd/MM/yyyy"+" "+"HH:mm:ss tt"); 
            obtenerHoraChecaador();
        }
        public void obtencionHoraChecador(object sender, EventArgs e)
        {
            obtenerHoraChecaador();
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
        private void obtenderHoraSistema()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += obtencionHora;
            timer.Start();
        }
        //Obtención
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void sicronizarHoraChecador()
        {
            DateTime currentTime = DateTime.Now;
            
            bool isTimeSet = conexionBiometrico.Biometrico.SetDeviceTime2(
                conexionBiometrico.iMachineNumber,
                currentTime.Year,
                currentTime.Month,
                currentTime.Day,
                currentTime.Hour,
                currentTime.Minute,
                currentTime.Second
            );

            if (isTimeSet)
            {
                MessageBox.Show("Hora del dispositivo actualizada correctamente.");
            }
            else
            {
                MessageBox.Show("Error al actualizar la hora del dispositivo.");
            }

        }

        private void btnSincronizarChacador_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            sicronizarHoraChecador();
            obtenerHoraChecaador();
        }

        private void cargaRegistros(string cvePersonal, TimeSpan hora_registro, DateTime fecha_registro, string metodo_verificacion, string estado_asistencia)
        {
            string query = "INSERT INTO ultimos_ingresos (cve_personal, hora_registro, fecha_registro, metodo_verificacion, estado_asistencia) " +
                   "VALUES (@cve_personal, @hora_registro, @fecha_registro, @metodo_verificacion, @estado_asistencia)";

            SqlParameter[] parameters = {
        new SqlParameter("@cve_personal", cvePersonal),
        new SqlParameter("@hora_registro", hora_registro),
        new SqlParameter("@fecha_registro", fecha_registro),
        new SqlParameter("@metodo_verificacion", metodo_verificacion),
        new SqlParameter("@estado_asistencia", estado_asistencia)
    };

            try
            {
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar
                MessageBox.Show("Error al añadir registros a la tabla: " + ex.Message);
            }
        }
        private void guardaUltimoRegistro(int ultimo_registro_bd)
        {
            string query = "INSERT INTO ultimo_registro_bd (ultimo_registro_bd) " +
                   "VALUES (@ultimo_registro_bd)";

            SqlParameter[] parameters = {
        new SqlParameter("@ultimo_registro_bd", ultimo_registro_bd),
    };

            try
            {
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar
                MessageBox.Show("Error al añadir registros a la tabla: " + ex.Message);
            }
        }
        private void obtenerUltimoRegistro()
        {
            string query = "SELECT TOP 1 [ultimo_registro_bd] FROM [dbo].[ultimo_registro_bd] ORDER BY [ultimo_registro_bd] DESC";
            string ultimaId = "0";

            using (var reader = conexion.ExecuteReader(query))
            {
                if (reader.Read())
                {
                    ultimaId = reader["ultimo_registro_bd"].ToString();
                }
            }
            n_ultimo_registro_bd = int.Parse(ultimaId);
        }
        private void obtenerDia()
        {
            fechaBusqueda = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy"));            
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
            DataGridTextColumn id_registro = new DataGridTextColumn
            {
                Header = " No. ",
                Binding = new Binding("id_registro"),
                Width = 30,
            };
            dvgUltimosRegistros.Columns.Add(id_registro);

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
        //Encabezados
        private void calendario_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            fechaBusqueda = (DateTime)calendario.SelectedDate;
            llenadoTablaUltimosBusqueda(fechaBusqueda);
        }


    }
    public class UR
    {
        public int id_registro { get; set; }
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
