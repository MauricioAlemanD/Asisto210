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
using System.Data;
using iText.Kernel.Pdf;
using iText.Layout.Properties;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Win32;


namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Page
    {

        List<TablaRepotePersonal> lTablaRepotePersonal = new List<TablaRepotePersonal>();
        List<TablaRepotePersonal> lRepotePersonal = new List<TablaRepotePersonal>();
        Conexion conexion;

        public Reportes()
        {

            conexion = new Conexion();
            InitializeComponent();
            EncabezadosTablaReporte();            
            llenadoCMBPeriodo();
            llenadoCMBTurnos();
            llenadoCMBPersonal();

        }

        private void EncabezadosTablaReporte()
        {
            DataGridTextColumn idColumn = new DataGridTextColumn
            {
                Header = " Clave personal ",
                Binding = new Binding("id"),
                Width = 30,
            };
            dvgReporte.Columns.Add(idColumn);

            DataGridTextColumn nombreColumn = new DataGridTextColumn
            {
                Header = " Nombre ",
                Binding = new Binding("nombre"),
            };
            dvgReporte.Columns.Add(nombreColumn);

            DataGridTextColumn rolColumn = new DataGridTextColumn
            {
                Header = " Rol ",
                Binding = new Binding("rol"),
            };
            dvgReporte.Columns.Add(rolColumn);

            DataGridTextColumn tunoColumn = new DataGridTextColumn
            {
                Header = " Turno ",
                Binding = new Binding("turno"),
            };
            dvgReporte.Columns.Add(tunoColumn);

            DataGridTextColumn fechaColumn = new DataGridTextColumn
            {
                Header = " Fecha ",
                Binding = new Binding("fecha"),
            };
            dvgReporte.Columns.Add(fechaColumn);

            DataGridTextColumn horaColumn = new DataGridTextColumn
            {
                Header = " Hora de entrada ",
                Binding = new Binding("horaEntrada"),
            };
            dvgReporte.Columns.Add(horaColumn);

            DataGridTextColumn horasColumn = new DataGridTextColumn
            {
                Header = " Hora de salida ",
                Binding = new Binding("horaSalida"),
            };
            dvgReporte.Columns.Add(horasColumn);

            DataGridTextColumn asistencaColumn = new DataGridTextColumn
            {
                Header = " Asistencia ",
                Binding = new Binding("asistencia"),
            };
            dvgReporte.Columns.Add(asistencaColumn);
        }

        public void llenadoTablaReportePersonal(DateTime fechaInicio, DateTime fechaFin, string turno, string cve_personal)
        {

            string query = @"
SELECT
    personal.cve_personal,
    personal.nombre,
    personal.apelldio_pateno, 
    personal.apellido_materno,
    roles.descripcion,
    entradas_salidas.hora_entrada,
    entradas_salidas.hora_salida,
    entradas_salidas.turno,
    entradas_salidas.fecha,
    CASE
        WHEN entradas_salidas.hora_salida IS NULL THEN 'Esperando salida'
        ELSE 'Completo'
    END AS estado_registro
FROM entradas_salidas
INNER JOIN personal ON personal.cve_personal = entradas_salidas.cve_personal
INNER JOIN roles ON personal.rol_personal = roles.cve_rol
WHERE entradas_salidas.fecha BETWEEN @fechaInicio AND @fechaFin 
    AND entradas_salidas.turno = @turno 
    AND personal.cve_personal = @cve_personal
ORDER BY entradas_salidas.fecha, personal.cve_personal";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@fechaInicio", SqlDbType.Date) { Value = fechaInicio },
        new SqlParameter("@fechaFin", SqlDbType.Date) { Value = fechaFin },
        new SqlParameter("@turno", SqlDbType.VarChar, 10) { Value = turno },
        new SqlParameter("@cve_personal", SqlDbType.NVarChar, 4) { Value = cve_personal }
    };

            List<TablaRepotePersonal> lTablaRepotePersonal = new List<TablaRepotePersonal>();

            using (var reader = conexion.ExecuteParametrizedReader(query, parameters.ToArray()))
            {
                while (reader.Read())
                {
                    TimeSpan horaEntrada = reader["hora_entrada"] != DBNull.Value ? (TimeSpan)reader["hora_entrada"] : TimeSpan.Zero;
                    TimeSpan horaSalida = reader["hora_salida"] != DBNull.Value ? (TimeSpan)reader["hora_salida"] : TimeSpan.Zero;

                    string horaFormateada = (horaEntrada != TimeSpan.Zero && horaSalida != TimeSpan.Zero)
                        ? $"{horaEntrada.ToString(@"hh\:mm")} - {horaSalida.ToString(@"hh\:mm")}"
                        : (horaEntrada != TimeSpan.Zero ? horaEntrada.ToString(@"hh\:mm") : "");

                    lTablaRepotePersonal.Add(new TablaRepotePersonal
                    {
                        id = reader["cve_personal"].ToString(),
                        nombre = $"{reader["nombre"]} {reader["apelldio_pateno"]} {reader["apellido_materno"]}",
                        rol = reader["descripcion"].ToString(),
                        turno = reader["turno"].ToString(),
                        fecha = Convert.ToDateTime(reader["fecha"]).ToString("yyyy-MM-dd"),
                        horaEntrada = reader["hora_entrada"].ToString(),
                        horaSalida = reader["hora_salida"].ToString(),
                        asistencia = reader["estado_registro"].ToString()
                    });
                }
            }

            // Asigna la lista a tu DataGrid o ListView
            dvgReporte.ItemsSource = lTablaRepotePersonal;
            lRepotePersonal = lTablaRepotePersonal;

        }
        public void llenadoTablaReporte(DateTime fechaInicio, DateTime fechaFin, string turno)
        {

            string query = @"
SELECT
    personal.cve_personal,
    personal.nombre,
    personal.apelldio_pateno, 
    personal.apellido_materno,
    roles.descripcion,
    entradas_salidas.hora_entrada,
    entradas_salidas.hora_salida,
    entradas_salidas.turno,
    entradas_salidas.fecha,
    CASE
        WHEN entradas_salidas.hora_salida IS NULL THEN 'Esperando salida'
        ELSE 'Completo'
    END AS estado_registro
FROM entradas_salidas
INNER JOIN personal ON personal.cve_personal = entradas_salidas.cve_personal
INNER JOIN roles ON personal.rol_personal = roles.cve_rol
WHERE entradas_salidas.fecha BETWEEN @fechaInicio AND @fechaFin 
    AND entradas_salidas.turno = @turno 
ORDER BY entradas_salidas.fecha, personal.cve_personal";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@fechaInicio", SqlDbType.Date) { Value = fechaInicio },
        new SqlParameter("@fechaFin", SqlDbType.Date) { Value = fechaFin },
        new SqlParameter("@turno", SqlDbType.VarChar, 10) { Value = turno },
    };

            List<TablaRepotePersonal> lTablaRepotePersonal = new List<TablaRepotePersonal>();

            using (var reader = conexion.ExecuteParametrizedReader(query, parameters.ToArray()))
            {
                while (reader.Read())
                {
                    TimeSpan horaEntrada = reader["hora_entrada"] != DBNull.Value ? (TimeSpan)reader["hora_entrada"] : TimeSpan.Zero;
                    TimeSpan horaSalida = reader["hora_salida"] != DBNull.Value ? (TimeSpan)reader["hora_salida"] : TimeSpan.Zero;

                    string horaFormateada = (horaEntrada != TimeSpan.Zero && horaSalida != TimeSpan.Zero)
                        ? $"{horaEntrada.ToString(@"hh\:mm")} - {horaSalida.ToString(@"hh\:mm")}"
                        : (horaEntrada != TimeSpan.Zero ? horaEntrada.ToString(@"hh\:mm") : "");

                    lTablaRepotePersonal.Add(new TablaRepotePersonal
                    {
                        id = reader["cve_personal"].ToString(),
                        nombre = $"{reader["nombre"]} {reader["apelldio_pateno"]} {reader["apellido_materno"]}",
                        rol = reader["descripcion"].ToString(),
                        turno = reader["turno"].ToString(),
                        fecha = Convert.ToDateTime(reader["fecha"]).ToString("yyyy-MM-dd"),
                        horaEntrada = reader["hora_entrada"].ToString(),
                        horaSalida = reader["hora_salida"].ToString(),
                        asistencia = reader["estado_registro"].ToString()
                    });
                }
            }

            // Asigna la lista a tu DataGrid o ListView
            dvgReporte.ItemsSource = lTablaRepotePersonal;
            lRepotePersonal = lTablaRepotePersonal;

        }

        private void llenadoCMBPeriodo()
        {
            cmbPeriodoFiltro.Items.Clear();

            cmbPeriodoFiltro.Items.Add("Semanal");
            cmbPeriodoFiltro.Items.Add("Mensual");

        }
        private void llenadoCMBTurnos()
        {
            cmbTurnoFiltro.Items.Clear();

            cmbTurnoFiltro.Items.Add("Matutino");
            cmbTurnoFiltro.Items.Add("Vespertino");

        }
        private void llenadoCMBPersonal()
        {
            cmbPersonal.Items.Clear();            
            string query = "select personal.cve_personal,personal.nombre,personal.apelldio_pateno,personal.apellido_materno,roles.descripcion from personal inner join roles on personal.rol_personal = roles.cve_rol";

            using (var reader = conexion.ExecuteReader(query))
                while (reader.Read())
                {
                    cmbPersonal.Items.Add(reader["cve_personal"].ToString() + " " + reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString());                    
                }            

        }

        private void btnGenerar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!dtpFechaInicio.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, seleccione una fecha de inicio.");
                return;
            }

            DateTime fechaInicio = dtpFechaInicio.SelectedDate.Value;
            DateTime fechaFin;

            if (cmbPeriodoFiltro.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un período (Semanal o Mensual).");
                return;
            }

            string periodoSeleccionado = cmbPeriodoFiltro.SelectedItem.ToString();

            switch (periodoSeleccionado)
            {
                case "Semanal":
                    fechaFin = fechaInicio.AddDays(6); // 7 días en total (incluyendo el día de inicio)
                    break;
                case "Mensual":
                    fechaFin = fechaInicio.AddMonths(1).AddDays(-1); // Último día del mes
                    break;
                default:
                    MessageBox.Show("Período no válido seleccionado.");
                    return;
            }

            string turno = cmbTurnoFiltro.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(turno))
            {
                MessageBox.Show("Por favor, seleccione un turno.");
                return;
            }
            try
            {
            string clavePersonal = cmbPersonal.SelectedItem.ToString().Substring(0, 4);
            
                if (string.IsNullOrEmpty(clavePersonal))
                {
                    MessageBox.Show("Por favor, ingrese una clave de personal.");
                    return;
                }

                try
                {
                    llenadoTablaReportePersonal(fechaInicio, fechaFin, turno, clavePersonal);
                    MessageBox.Show($"Reporte generado desde {fechaInicio.ToShortDateString()} hasta {fechaFin.ToShortDateString()}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el reporte: {ex.Message}");
                }
            }
            catch
            {
                try
                {
                    llenadoTablaReporte(fechaInicio, fechaFin, turno);
                    MessageBox.Show($"Reporte generado desde {fechaInicio.ToShortDateString()} hasta {fechaFin.ToShortDateString()}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el reporte: {ex.Message}");
                }
            }
        }

        private void btnRiniciarFiltro_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            llenadoCMBPersonal();
        }

        private void btnGuardar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Exportar a PDF
            // Mostrar el diálogo para guardar el archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Guardar reporte como PDF";
            saveFileDialog.FileName = "reporte.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string pdfPath = saveFileDialog.FileName;

                using (PdfWriter writer = new PdfWriter(pdfPath))
                {
                    using (PdfDocument pdfDoc = new PdfDocument(writer))
                    {
                        Document document = new Document(pdfDoc);

                        iText.Layout.Element.Table table = new iText.Layout.Element.Table(new float[] { 2, 4, 2, 2, 2, 2, 2, 2 });
                        table.SetWidth(UnitValue.CreatePercentValue(100));

                        // Añadir encabezados de la tabla
                        table.AddHeaderCell("ID");
                        table.AddHeaderCell("Nombre");
                        table.AddHeaderCell("Rol");
                        table.AddHeaderCell("Turno");
                        table.AddHeaderCell("Fecha");
                        table.AddHeaderCell("Hora Entrada");
                        table.AddHeaderCell("Hora Salida");
                        table.AddHeaderCell("Asistencia");

                        // Añadir datos a la tabla
                        foreach (var item in lRepotePersonal)
                        {
                            table.AddCell(item.id);
                            table.AddCell(item.nombre);
                            table.AddCell(item.rol);
                            table.AddCell(item.turno);
                            table.AddCell(item.fecha);
                            table.AddCell(item.horaEntrada);
                            table.AddCell(item.horaSalida);
                            table.AddCell(item.asistencia);
                        }

                        document.Add(table);
                    }
                }

                MessageBox.Show("Archivo PDF guardado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        }

    public class TablaRepotePersonal
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string rol { get; set; }
        public string turno { get; set; }
        public string fecha { get; set; }
        public string horaEntrada { get; set; }
        public string horaSalida { get; set; }
        public string asistencia { get; set; }
    }

}
