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
using System.Globalization;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : System.Windows.Controls.Page
    {

        Color colorAzul = (Color)ColorConverter.ConvertFromString("#FF458AB7");
        Color colorRojo = (Color)ColorConverter.ConvertFromString("#FFC2504B");
        Color colorVerde = (Color)ColorConverter.ConvertFromString("#FF93BC52");
        Color colorAmarillo = (Color)ColorConverter.ConvertFromString("#FFCF9C45");

        //Filtros
        bool fechaNueva = false;
        public string G_cve_horario_profesor = "";
        public string G_ciclo_escolar = "";
        public string G_cve_profesor = "";
        public string G_turno = "";

        public bool confirmacion = false;

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


        string fechaSistema = DateTime.Now.ToString("yyyy-MM-dd");
        Conexion conexion;
        List<TablaRegistroDiario> registrosDiarios = new List<TablaRegistroDiario>();
        public Page1()
        {
            conexion = new Conexion();
            InitializeComponent();
            EncabezadosTablaRegistroDiario();
            logicaHorarios(fechaSistema);
            llenadoTablaRegistroDiario(fechaSistema);
            llenarCMBPersonal();
            llenarCMBTurno();
            llenarCMBHorarios();
        }

        private void EncabezadosTablaRegistroDiario()
        {
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Clave de personal", Binding = new Binding("id") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Nombre", Binding = new Binding("nombre") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Fecha de Registro", Binding = new Binding("fechaRegistro") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Hora de Ingreso", Binding = new Binding("horaRegistroIngreso") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Hora de Salida", Binding = new Binding("horaRegistroSalida") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Turno", Binding = new Binding("turno") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Justificante", Binding = new Binding("justificante") });

        }

        private void llenadoTablaRegistroDiario(string fecha_dia)
        {
            string query = @"
    SELECT 
        entradas_salidas.cve_personal, 
        personal.nombre, 
        personal.apelldio_pateno, 
        personal.apellido_materno,
        entradas_salidas.fecha, 
        entradas_salidas.hora_entrada, 
        entradas_salidas.hora_salida, 
        entradas_salidas.turno,
        justificantes.motivo
    FROM entradas_salidas 
    INNER JOIN personal ON entradas_salidas.cve_personal = personal.cve_personal 
    LEFT JOIN justificantes ON justificantes.cve_personal = personal.cve_personal
        AND justificantes.fecha_justificante = entradas_salidas.fecha
        AND justificantes.turno = entradas_salidas.turno
    WHERE entradas_salidas.fecha = @fechaBusqueda";

            SqlParameter parameter = new SqlParameter("@fechaBusqueda", System.Data.SqlDbType.Date);
            parameter.Value = fecha_dia;
            List<TablaRegistroDiario> registrosDiarios = new List<TablaRegistroDiario>();

            using (var reader = conexion.ExecuteParametrizedReader(query, parameter))
            {
                while (reader.Read())
                {
                    registrosDiarios.Add(new TablaRegistroDiario
                    {
                        id = reader["cve_personal"].ToString(),
                        nombre = $"{reader["nombre"]} {reader["apelldio_pateno"]} {reader["apellido_materno"]}",
                        fechaRegistro = Convert.ToDateTime(reader["fecha"]).ToString("yyyy-MM-dd"),
                        horaRegistroIngreso = reader["hora_entrada"].ToString(),
                        horaRegistroSalida = reader["hora_salida"].ToString(),
                        turno = reader["turno"].ToString(),
                        justificante = reader["motivo"] == DBNull.Value ? "Sin justificante" : reader["motivo"].ToString(),
                    });
                }
                dvgRegistroDiario.ItemsSource = registrosDiarios;
            }
        }

        private void llenarCMBPersonal()
        {
            cmbCVE_PersonalJust.Items.Clear();
            cmbCVE_Filtro.Items.Clear();
            string query = "select personal.cve_personal,personal.nombre,personal.apelldio_pateno,personal.apellido_materno,roles.descripcion from personal inner join roles on personal.rol_personal = roles.cve_rol where rol_personal = '3'";

            using (var reader = conexion.ExecuteReader(query))
            {
                string personal = "";
                while (reader.Read())
                {
                    personal = "";
                    personal = reader["cve_personal"].ToString() + " " + reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString();
                    cmbCVE_PersonalJust.Items.Add(personal);
                    cmbCVE_Filtro.Items.Add(personal);
                }
            }
        }

        private void llenarCMBTurno()
        {
            cmbTurno_Jus.Items.Clear();
            cmbTurno_Jus.Items.Add("Matutino");
            cmbTurno_Jus.Items.Add("Vespertino");
        }

        private void llenarCMBHorarios()
        {
            cmbHorarioProfesor.Items.Clear();
            string query = "select horario_profesor.cve_horario_profesor,horario_profesor.cve_profesor,personal.nombre,personal.apelldio_pateno,personal.apellido_materno,horario_profesor.ciclo_escolar,horario_profesor.turno from horario_profesor inner join personal on personal.cve_personal = horario_profesor.cve_profesor";

            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    horaioProfesor = "";
                    horaioProfesor = "Horario: " + reader["cve_horario_profesor"].ToString() + " Docente: " + reader["cve_profesor"] + "-" + reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString() + " Ciclo escolar: [" + reader["ciclo_escolar"] + "] Turno: " + reader["turno"];
                    cmbHorarioProfesor.Items.Add(horaioProfesor);
                }
            }
        }

        public void logicaHorarios(string fecha_dia)
        {
            string queryPersonal = "SELECT DISTINCT cve_personal FROM ultimos_ingresos WHERE fecha_registro = @fechaBusqueda; DELETE FROM [dbo].[entradas_salidas] WHERE fecha = @fechaBusqueda;";
            string queryHoras = "SELECT hora_registro FROM ultimos_ingresos WHERE fecha_registro = @fechaBusqueda AND cve_personal = @cvePersonal";
            string queryInsert = "INSERT INTO [dbo].[entradas_salidas] ([cve_personal], [fecha], [hora_entrada], [hora_salida], [turno]) VALUES (@cve_personal, @fecha, @hora_entrada, @hora_salida, @turno)";

            using (var connection = conexion.GetConnection())
            {
                connection.Open();

                List<string> personal_registrado_dia = new List<string>();

                // Obtener la lista de personal registrado en el día
                using (var commandPersonal = new SqlCommand(queryPersonal, connection))
                {
                    commandPersonal.Parameters.Add(new SqlParameter("@fechaBusqueda", System.Data.SqlDbType.Date) { Value = fecha_dia });

                    using (var reader = commandPersonal.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            personal_registrado_dia.Add(reader["cve_personal"].ToString());
                        }
                    }
                }

                foreach (var cvePersonal in personal_registrado_dia)
                {
                    List<string> horas_personal_registrado_dia = new List<string>();

                    // Obtener las horas de registro para el personal actual
                    using (var commandHoras = new SqlCommand(queryHoras, connection))
                    {
                        commandHoras.Parameters.Add(new SqlParameter("@fechaBusqueda", System.Data.SqlDbType.Date) { Value = fecha_dia });
                        commandHoras.Parameters.Add(new SqlParameter("@cvePersonal", System.Data.SqlDbType.VarChar) { Value = cvePersonal });

                        using (var horasReader = commandHoras.ExecuteReader())
                        {
                            while (horasReader.Read())
                            {
                                horas_personal_registrado_dia.Add(horasReader["hora_registro"].ToString());
                            }
                        }
                    }

                    // Insertar registros según las horas obtenidas
                    if (horas_personal_registrado_dia.Count >= 1)
                    {
                        using (var commandInsertMatutino = new SqlCommand(queryInsert, connection))
                        {
                            commandInsertMatutino.Parameters.Add(new SqlParameter("@cve_personal", System.Data.SqlDbType.VarChar) { Value = cvePersonal });
                            commandInsertMatutino.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.Date) { Value = fecha_dia });
                            commandInsertMatutino.Parameters.Add(new SqlParameter("@hora_entrada", System.Data.SqlDbType.Time) { Value = horas_personal_registrado_dia[0] });

                            if (horas_personal_registrado_dia.Count >= 2)
                            {
                                commandInsertMatutino.Parameters.Add(new SqlParameter("@hora_salida", System.Data.SqlDbType.Time) { Value = horas_personal_registrado_dia[1] });
                            }
                            else
                            {
                                commandInsertMatutino.Parameters.Add(new SqlParameter("@hora_salida", DBNull.Value));
                            }

                            commandInsertMatutino.Parameters.Add(new SqlParameter("@turno", System.Data.SqlDbType.VarChar) { Value = "Matutino" });
                            commandInsertMatutino.ExecuteNonQuery(); // Ejecutar la inserción
                        }
                    }

                    if (horas_personal_registrado_dia.Count >= 3)
                    {
                        using (var commandInsertVespertino = new SqlCommand(queryInsert, connection))
                        {
                            commandInsertVespertino.Parameters.Add(new SqlParameter("@cve_personal", System.Data.SqlDbType.VarChar) { Value = cvePersonal });
                            commandInsertVespertino.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.Date) { Value = fecha_dia });
                            commandInsertVespertino.Parameters.Add(new SqlParameter("@hora_entrada", System.Data.SqlDbType.Time) { Value = horas_personal_registrado_dia[2] });

                            if (horas_personal_registrado_dia.Count >= 4)
                            {
                                commandInsertVespertino.Parameters.Add(new SqlParameter("@hora_salida", System.Data.SqlDbType.Time) { Value = horas_personal_registrado_dia[3] });
                            }
                            else
                            {
                                commandInsertVespertino.Parameters.Add(new SqlParameter("@hora_salida", DBNull.Value));
                            }

                            commandInsertVespertino.Parameters.Add(new SqlParameter("@turno", System.Data.SqlDbType.VarChar) { Value = "Vespertino" });
                            commandInsertVespertino.ExecuteNonQuery(); // Ejecutar la inserción
                        }
                    }
                }
            }
        }

        private void dtpBusqueda_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            fechaNueva = true;
        }

        private void btnAplicarFiltros_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (fechaNueva)
            {
                fechaSistema = dtpBusqueda.SelectedDate.Value.ToString("yyyy-MM-dd").Substring(0, 10);
                llenadoTablaRegistroDiario(fechaSistema);

            }

            MessageBox.Show("Se aplicó el filtro");
        }

        private void btnGuardarJustificante_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string cve_personal_justificado = "";
            string fecha_justificada = "";
            string turno = "";
            string motivo = "";

            cve_personal_justificado = cmbCVE_PersonalJust.SelectedItem.ToString().Substring(0, 4);
            fecha_justificada = dtpJustificacion.SelectedDate.Value.ToString("yyyy-MM-dd");
            turno = cmbTurno_Jus.SelectedItem.ToString();
            motivo = txtJustificacon.Text;

            if (cve_personal_justificado != "" && fecha_justificada != "" && turno != "" && motivo != "")
            {

                string query = "INSERT INTO [dbo].[justificantes] ([cve_personal],[fecha_justificante],[motivo],[turno]) VALUES (@cve_personal,@fecha_justificada,@motivo,@turno)";

                SqlParameter[] parameters = {
        new SqlParameter("@cve_personal", cve_personal_justificado),
        new SqlParameter("@fecha_justificada",fecha_justificada),
        new SqlParameter("@turno",turno),
        new SqlParameter("@motivo",motivo),

            };

                try
                {
                    // Pasar el array de parámetros a ExecuteNonQuery
                    conexion.ExecuteNonQuery(query, parameters);
                    MessageBoxPersonalizado("Se añadio el justificante. ", "Confimación");
                }
                catch (Exception ex)
                {
                    // Manejo de excepción, mostrar el error o registrar

                    MessageBoxPersonalizado("Ya existe un justificante para este dia y turno " + ex, "Error");
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBoxPersonalizado("Revise que esté todo lleno.", "Alerta");
            }

        }

        public void MessageBoxPersonalizado(string mensajealerta, string titulo)
        {
            confirmacion = false;
            MensajePersonalizado messageBox = new MensajePersonalizado(mensajealerta, titulo);
            bool? result = messageBox.ShowDialog();

            if (result == true)
            {
                confirmacion = true;
            }
            else
            {
                confirmacion = false;
            }
        }

        private void dtpDiaAsistenciaRapida_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string cve_horario_profesor = "";
            cve_horario_profesor = cmbHorarioProfesor.SelectedItem.ToString().Substring(9, 4);
            string fechaAsistencaRapida = "";
            fechaAsistencaRapida = dtpDiaAsistenciaRapida.SelectedDate.Value.ToString("yyyy-MM-dd").Substring(0, 10);

            if (cve_horario_profesor != "")
            {
                llenadoHorarioSeleccionado(cve_horario_profesor);
                consultaAistenciaRapida(fechaAsistencaRapida, G_turno, G_cve_profesor);
            }
            else
            {
                MessageBox.Show("Seleccionar un horario.");
            }


        }

        private void consultaAistenciaRapida(string fecha_dia, string turno, string cve_personal)
        {
            string hora_entrada = "Sin datos";
            string hora_salida = "Sin datos";
            string justificacion_motivo = "Sin datos";

            string query = @"
    SELECT 
        entradas_salidas.hora_entrada, 
        entradas_salidas.hora_salida, 
        justificantes.motivo
    FROM entradas_salidas 
    INNER JOIN personal ON entradas_salidas.cve_personal = personal.cve_personal 
    LEFT JOIN justificantes ON justificantes.cve_personal = personal.cve_personal
        AND justificantes.fecha_justificante = entradas_salidas.fecha
        AND justificantes.turno = entradas_salidas.turno
    WHERE entradas_salidas.fecha = @fechaBusqueda 
        AND entradas_salidas.turno = @turnoBusqueda 
        AND entradas_salidas.cve_personal = @cvePersonalBusqueda";

            SqlParameter[] parameters = {
        new SqlParameter("@fechaBusqueda", SqlDbType.Date) { Value = fecha_dia },
        new SqlParameter("@turnoBusqueda", SqlDbType.VarChar) { Value = turno },
        new SqlParameter("@cvePersonalBusqueda", SqlDbType.VarChar) { Value = cve_personal }
    };
            using (var reader = conexion.ExecuteParametrizedReader(query, parameters))
            {
                if (reader.Read())
                {
                    hora_entrada = reader["hora_entrada"] != DBNull.Value ? reader["hora_entrada"].ToString() : "Sin datos";
                    hora_salida = reader["hora_salida"] != DBNull.Value ? reader["hora_salida"].ToString() : "Sin datos";
                    justificacion_motivo = reader["motivo"] != DBNull.Value ? reader["motivo"].ToString() : "Sin datos";
                }
            }

            DateTime fecha;
            if (DateTime.TryParse(fecha_dia, out fecha))
            {
                lblFechaSeleccionada.Content = "Fecha: " +  fecha_dia;
                //lblDiaSemana.Content = "Día de la semana: " + fecha.ToString("dddd", new CultureInfo("es-ES"));
                lblDiaSemana.Content = "Día de la semana: lunes";
                lblHoraEntrada.Content = "Hora entrada: " + hora_entrada;
                lblHoraSalida.Content = "Hora salida: " + hora_salida;
                lblJustificante.Content = "Justificación: " + justificacion_motivo;
                coloreadoSARBack();

                if (lblJustificante.Content != "Justificación: Sin datos")
                {
                    coloreadoJUSBack();
                }
                
            }
            else
            {
                // Manejar el caso donde la fecha no es válida
                lblFechaSeleccionada.Content = "Fecha: Fecha no válida";
                lblDiaSemana.Content = "Día de la semana: Desconocido";
                lblHoraEntrada.Content = "Hora entrada: Sin datos";
                lblHoraSalida.Content = "Hora salida: Sin datos";
                lblJustificante.Content = "Justificación: Sin datos";
            }

        }

        private void coloreadoSARBack(){

            if (lblDiaSemana.Content == "Día de la semana: lunes")
            {
                H1Lunes.Background = new SolidColorBrush(colorRojo);
                H2Lunes.Background = new SolidColorBrush(colorRojo);
                H3Lunes.Background = new SolidColorBrush(colorRojo);
                H4Lunes.Background = new SolidColorBrush(colorRojo);
                H5Lunes.Background = new SolidColorBrush(colorRojo);
                H6Lunes.Background = new SolidColorBrush(colorRojo);
                H7Lunes.Background = new SolidColorBrush(colorRojo);
                H8Lunes.Background = new SolidColorBrush(colorRojo);
                H9Lunes.Background = new SolidColorBrush(colorRojo);
            }

            if (lblDiaSemana.Content == "Día de la semana: martes")
            {
                H1Martes.Background = new SolidColorBrush(colorRojo);
                H2Martes.Background = new SolidColorBrush(colorRojo);
                H3Martes.Background = new SolidColorBrush(colorRojo);
                H4Martes.Background = new SolidColorBrush(colorRojo);
                H5Martes.Background = new SolidColorBrush(colorRojo);
                H6Martes.Background = new SolidColorBrush(colorRojo);
                H7Martes.Background = new SolidColorBrush(colorRojo);
                H8Martes.Background = new SolidColorBrush(colorRojo);
                H9Martes.Background = new SolidColorBrush(colorRojo);
            }

            if (lblDiaSemana.Content == "Día de la semana: miercoles")
            {
                H1Miercoles.Background = new SolidColorBrush(colorRojo);
                H2Miercoles.Background = new SolidColorBrush(colorRojo);
                H3Miercoles.Background = new SolidColorBrush(colorRojo);
                H4Miercoles.Background = new SolidColorBrush(colorRojo);
                H5Miercoles.Background = new SolidColorBrush(colorRojo);
                H6Miercoles.Background = new SolidColorBrush(colorRojo);
                H7Miercoles.Background = new SolidColorBrush(colorRojo);
                H8Miercoles.Background = new SolidColorBrush(colorRojo);
                H9Miercoles.Background = new SolidColorBrush(colorRojo);
            }

            if (lblDiaSemana.Content == "Día de la semana: jueves")
            {
                H1Jueves.Background = new SolidColorBrush(colorRojo);
                H2Jueves.Background = new SolidColorBrush(colorRojo);
                H3Jueves.Background = new SolidColorBrush(colorRojo);
                H4Jueves.Background = new SolidColorBrush(colorRojo);
                H5Jueves.Background = new SolidColorBrush(colorRojo);
                H6Jueves.Background = new SolidColorBrush(colorRojo);
                H7Jueves.Background = new SolidColorBrush(colorRojo);
                H8Jueves.Background = new SolidColorBrush(colorRojo);
                H9Jueves.Background = new SolidColorBrush(colorRojo);
            }

            if (lblDiaSemana.Content == "Día de la semana: viernes")
            {
                H2Viernes.Background = new SolidColorBrush(colorRojo);
                H3Viernes.Background = new SolidColorBrush(colorRojo);
                H4Viernes.Background = new SolidColorBrush(colorRojo);
                H5Viernes.Background = new SolidColorBrush(colorRojo);
                H1Viernes.Background = new SolidColorBrush(colorRojo);
                H6Viernes.Background = new SolidColorBrush(colorRojo);
                H7Viernes.Background = new SolidColorBrush(colorRojo);
                H8Viernes.Background = new SolidColorBrush(colorRojo);
                H9Viernes.Background = new SolidColorBrush(colorRojo);
            }
        }

        private void coloreadoJUSBack()
        {

            if (lblDiaSemana.Content == "Día de la semana: lunes")
            {
                H1Lunes.Background = new SolidColorBrush(colorAmarillo);
                H2Lunes.Background = new SolidColorBrush(colorAmarillo);
                H3Lunes.Background = new SolidColorBrush(colorAmarillo);
                H4Lunes.Background = new SolidColorBrush(colorAmarillo);
                H5Lunes.Background = new SolidColorBrush(colorAmarillo);
                H6Lunes.Background = new SolidColorBrush(colorAmarillo);
                H7Lunes.Background = new SolidColorBrush(colorAmarillo);
                H8Lunes.Background = new SolidColorBrush(colorAmarillo);
                H9Lunes.Background = new SolidColorBrush(colorAmarillo);
            }

            if (lblDiaSemana.Content == "Día de la semana: martes")
            {
                H1Martes.Background = new SolidColorBrush(colorAmarillo);
                H2Martes.Background = new SolidColorBrush(colorAmarillo);
                H3Martes.Background = new SolidColorBrush(colorAmarillo);
                H4Martes.Background = new SolidColorBrush(colorAmarillo);
                H5Martes.Background = new SolidColorBrush(colorAmarillo);
                H6Martes.Background = new SolidColorBrush(colorAmarillo);
                H7Martes.Background = new SolidColorBrush(colorAmarillo);
                H8Martes.Background = new SolidColorBrush(colorAmarillo);
                H9Martes.Background = new SolidColorBrush(colorAmarillo);
            }

            if (lblDiaSemana.Content == "Día de la semana: miercoles")
            {
                H1Miercoles.Background = new SolidColorBrush(colorAmarillo);
                H2Miercoles.Background = new SolidColorBrush(colorAmarillo);
                H3Miercoles.Background = new SolidColorBrush(colorAmarillo);
                H4Miercoles.Background = new SolidColorBrush(colorAmarillo);
                H5Miercoles.Background = new SolidColorBrush(colorAmarillo);
                H6Miercoles.Background = new SolidColorBrush(colorAmarillo);
                H7Miercoles.Background = new SolidColorBrush(colorAmarillo);
                H8Miercoles.Background = new SolidColorBrush(colorAmarillo);
                H9Miercoles.Background = new SolidColorBrush(colorAmarillo);
            }

            if (lblDiaSemana.Content == "Día de la semana: jueves")
            {
                H1Jueves.Background = new SolidColorBrush(colorAmarillo);
                H2Jueves.Background = new SolidColorBrush(colorAmarillo);
                H3Jueves.Background = new SolidColorBrush(colorAmarillo);
                H4Jueves.Background = new SolidColorBrush(colorAmarillo);
                H5Jueves.Background = new SolidColorBrush(colorAmarillo);
                H6Jueves.Background = new SolidColorBrush(colorAmarillo);
                H7Jueves.Background = new SolidColorBrush(colorAmarillo);
                H8Jueves.Background = new SolidColorBrush(colorAmarillo);
                H9Jueves.Background = new SolidColorBrush(colorAmarillo);
            }

            if (lblDiaSemana.Content == "Día de la semana: viernes")
            {
                H2Viernes.Background = new SolidColorBrush(colorAmarillo);
                H3Viernes.Background = new SolidColorBrush(colorAmarillo);
                H4Viernes.Background = new SolidColorBrush(colorAmarillo);
                H5Viernes.Background = new SolidColorBrush(colorAmarillo);
                H1Viernes.Background = new SolidColorBrush(colorAmarillo);
                H6Viernes.Background = new SolidColorBrush(colorAmarillo);
                H7Viernes.Background = new SolidColorBrush(colorAmarillo);
                H8Viernes.Background = new SolidColorBrush(colorAmarillo);
                H9Viernes.Background = new SolidColorBrush(colorAmarillo);
            }
        }


        private void llenadoHorarioSeleccionado(string cve_horario_profesor)
        {
            G_cve_horario_profesor = "";
            G_ciclo_escolar = "";
            G_cve_profesor = "";
            G_turno = "";

            G_cve_horario_profesor = cve_horario_profesor;

            string query = "select horario_profesor.cve_horario_profesor,horario_profesor.cve_profesor,horario_profesor.ciclo_escolar,horario_profesor.turno from horario_profesor where cve_horario_profesor = '" + cve_horario_profesor + "'";

            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    G_ciclo_escolar = reader["ciclo_escolar"].ToString();
                    G_cve_profesor = reader["cve_profesor"].ToString();
                    G_turno = reader["turno"].ToString();


                }
            }

            SHora1 = "";
            SHora2 = "";
            SHora3 = "";
            SHora4 = "";
            SHora5 = "";
            SHora6 = "";
            SHora7 = "";
            SHora8 = "";
            SHora9 = "";

            query = "WITH CTE AS (SELECT [cve_horario_profesor], [ciclo_escolar], [cve_profesor], [turno], [dia_clase], [hora_clase], [hora_clase_formateada], [cve_clase], ROW_NUMBER() OVER (PARTITION BY [hora_clase] ORDER BY [dia_clase]) AS rn FROM [dbo].[horario_profesor_clases_v3] WHERE [cve_horario_profesor] = '" + cve_horario_profesor + "') SELECT [cve_horario_profesor], [ciclo_escolar], [cve_profesor], [turno], [dia_clase], [hora_clase], [hora_clase_formateada], [cve_clase] FROM CTE WHERE rn = 1 ORDER BY [hora_clase];";

            using (var reader = conexion.ExecuteReader(query))
            {
                int count = 0;
                while (reader.Read() && count < 9)
                {
                    string horaFormateada = reader["hora_clase_formateada"].ToString();

                    // Asignar valores a variables
                    switch (count)
                    {
                        case 0: SHora1 = horaFormateada; break;
                        case 1: SHora2 = horaFormateada; break;
                        case 2: SHora3 = horaFormateada; break;
                        case 3: SHora4 = horaFormateada; break;
                        case 4: SHora5 = horaFormateada; break;
                        case 5: SHora6 = horaFormateada; break;
                        case 6: SHora7 = horaFormateada; break;
                        case 7: SHora8 = horaFormateada; break;
                        case 8: SHora9 = horaFormateada; break;
                    }
                    count++;
                }
            }

            completarHorasHorario();

            //Hora1Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora1 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora1Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora2Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora2 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora2Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora3Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora3 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora3Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora4Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora4 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora4Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora5Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora5 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora5Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora6Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora6 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora6Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora7Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora7 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora7Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora8Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora8 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora8Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora9Lunes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Lunes' and hora_clase_formateada = '" + SHora9 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora9Lunes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }

            //Hora1Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora1 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora1Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora2Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora2 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora2Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora3Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora3 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora3Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora4Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora4 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora4Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora5Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora5 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora5Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora6Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora6 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora6Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora7Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora7 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora7Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora8Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora8 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora8Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora9Martes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Martes' and hora_clase_formateada = '" + SHora9 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora9Martes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }

            //Hora1Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora1 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora1Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora2Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora2 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora2Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora3Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora3 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora3Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora4Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora4 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora4Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora5Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora5 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora5Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora6Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora6 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora6Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora7Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora7 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora7Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora8Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora8 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora8Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora9Miercoles
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Miercoles' and hora_clase_formateada = '" + SHora9 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora9Miercoles = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }

            //Hora1Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora1 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora1Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora2Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora2 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora2Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora3Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora3 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora3Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora4Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora4 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora4Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora5Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora5 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora5Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora6Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora6 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora6Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora7Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora7 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora7Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora8Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora8 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora8Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }//Hora9Jueves
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Jueves' and hora_clase_formateada = '" + SHora9 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora9Jueves = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }

            //Hora1Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora1 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora1Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora2Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora2 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora2Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora3Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora3 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora3Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora4Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora4 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora4Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora5Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora5 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora5Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora6Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora6 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora6Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora7Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora7 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora7Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora8Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora8 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora8Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }
            //Hora9Viernes
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '" + cve_horario_profesor + "' and dia_clase = 'Viernes' and hora_clase_formateada = '" + SHora9 + "'";
            using (var reader = conexion.ExecuteReader(query))
            {
                string horaioProfesor = "";
                while (reader.Read())
                {
                    SHora9Viernes = reader["cve_clase"].ToString() + " " + reader["nombre_materia"].ToString();
                }
            }


            completarAsignaturasHorario();
        }

        private void completarHorasHorario()
        {
            Hora1.Content = SHora1;
            Hora2.Content = SHora2;
            Hora3.Content = SHora3;
            Hora4.Content = SHora4;
            Hora5.Content = SHora5;
            Hora6.Content = SHora6;
            Hora7.Content = SHora7;
            Hora8.Content = SHora8;
            Hora9.Content = SHora9;
        }

        private void completarAsignaturasHorario()
        {
            Hora1Lunes.Content = SHora1Lunes;
            Hora2Lunes.Content = SHora2Lunes;
            Hora3Lunes.Content = SHora3Lunes;
            Hora4Lunes.Content = SHora4Lunes;
            Hora5Lunes.Content = SHora5Lunes;
            Hora6Lunes.Content = SHora6Lunes;
            Hora7Lunes.Content = SHora7Lunes;
            Hora8Lunes.Content = SHora8Lunes;
            Hora9Lunes.Content = SHora9Lunes;

            Hora1Martes.Content = SHora1Martes;
            Hora2Martes.Content = SHora2Martes;
            Hora3Martes.Content = SHora3Martes;
            Hora4Martes.Content = SHora4Martes;
            Hora5Martes.Content = SHora5Martes;
            Hora6Martes.Content = SHora6Martes;
            Hora7Martes.Content = SHora7Martes;
            Hora8Martes.Content = SHora8Martes;
            Hora9Martes.Content = SHora9Martes;

            Hora1Miercoles.Content = SHora1Miercoles;
            Hora2Miercoles.Content = SHora2Miercoles;
            Hora3Miercoles.Content = SHora3Miercoles;
            Hora4Miercoles.Content = SHora4Miercoles;
            Hora5Miercoles.Content = SHora5Miercoles;
            Hora6Miercoles.Content = SHora6Miercoles;
            Hora7Miercoles.Content = SHora7Miercoles;
            Hora8Miercoles.Content = SHora8Miercoles;
            Hora9Miercoles.Content = SHora9Miercoles;

            Hora1Jueves.Content = SHora1Jueves;
            Hora2Jueves.Content = SHora2Jueves;
            Hora3Jueves.Content = SHora3Jueves;
            Hora4Jueves.Content = SHora4Jueves;
            Hora5Jueves.Content = SHora5Jueves;
            Hora6Jueves.Content = SHora6Jueves;
            Hora7Jueves.Content = SHora7Jueves;
            Hora8Jueves.Content = SHora8Jueves;
            Hora9Jueves.Content = SHora9Jueves;

            Hora1Viernes.Content = SHora1Viernes;
            Hora2Viernes.Content = SHora2Viernes;
            Hora3Viernes.Content = SHora3Viernes;
            Hora4Viernes.Content = SHora4Viernes;
            Hora5Viernes.Content = SHora5Viernes;
            Hora6Viernes.Content = SHora6Viernes;
            Hora7Viernes.Content = SHora7Viernes;
            Hora8Viernes.Content = SHora8Viernes;
            Hora9Viernes.Content = SHora9Viernes;
        }

    }

    public class TablaRegistroDiario
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string fechaRegistro { get; set; }
        public string horaRegistroIngreso { get; set; }
        public string horaRegistroSalida { get; set; }
        public string turno { get; set; }
        public string justificante { get; set; }
    }
}
