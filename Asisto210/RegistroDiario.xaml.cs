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

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {

        //Filtros
        bool fechaNueva = false;


        string fechaSistema = DateTime.Now.ToString("yyyy-MM-dd");
        Conexion conexion;
        List<TablaRegistroDiario> registrosDiarios = new List<TablaRegistroDiario>();
        public Page1()
        {
            conexion = new Conexion();
            InitializeComponent();
            EncabezadosTablaRegistroDiario();            
            llenadoTablaRegistroDiario(fechaSistema);
        }

        private void EncabezadosTablaRegistroDiario()
        {
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Nombre", Binding = new Binding("nombre") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Fecha de Registro", Binding = new Binding("fechaRegistro") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Hora de Ingreso", Binding = new Binding("horaRegistroIngreso") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Hora de Salida", Binding = new Binding("horaRegistroSalida") });
            dvgRegistroDiario.Columns.Add(new DataGridTextColumn { Header = "Estado del Registro", Binding = new Binding("estadoRegistro") });
        }

        private void llenadoTablaRegistroDiario(string fecha_dia)
        {

            string query = "\t\t   SELECT \r\n    ui.cve_personal,\r\n    p.nombre,\r\n    p.apelldio_pateno,\r\n    p.apellido_materno,\r\n    ui.fecha_registro,\r\n    ui.hora_registro AS hora_entrada,\r\n    LEAD(ui.hora_registro) OVER (PARTITION BY ui.cve_personal, ui.fecha_registro ORDER BY ui.hora_registro) AS hora_salida,\r\n    e.descripcion_estado\r\nFROM \r\n    ultimos_ingresos ui\r\nINNER JOIN \r\n    personal p ON ui.cve_personal = p.cve_personal\r\nINNER JOIN \r\n    estados e ON ui.estado_asistencia = e.cve_estado\r\nWHERE \r\n    ui.fecha_registro = @fechaBusqueda\r\nORDER BY \r\n    ui.cve_personal, ui.fecha_registro, ui.hora_registro;\r\n";

            // Parámetro para la fecha
            SqlParameter parameter = new SqlParameter("@fechaBusqueda", System.Data.SqlDbType.Date);
            parameter.Value = fecha_dia;

            List<TablaRegistroDiario> registrosDiarios = new List<TablaRegistroDiario>();

            // Ejecutar la consulta con parámetros
            using (var reader = conexion.ExecuteParametrizedReader(query, parameter))
            {
                while (reader.Read())
                {
                    registrosDiarios.Add(new TablaRegistroDiario
                    {
                        id = reader["cve_personal"].ToString(),
                        nombre = reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString(),
                        fechaRegistro = reader["fecha_registro"].ToString(),
                        horaRegistroIngreso = reader["hora_entrada"].ToString(),
                        horaRegistroSalida = reader["hora_salida"].ToString(),
                        estadoRegistro = reader["descripcion_estado"].ToString()
                    });
                }
                dvgRegistroDiario.ItemsSource = registrosDiarios;
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
    }

    public class TablaRegistroDiario
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string fechaRegistro { get; set; }
        public string horaRegistroIngreso { get; set; }
        public string horaRegistroSalida { get; set; }
        public string estadoRegistro { get; set; }
    }
}
