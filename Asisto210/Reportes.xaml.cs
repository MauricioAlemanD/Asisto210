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
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Page
    {
        public Reportes()
        {
            InitializeComponent();
            EncabezadosTablaReporte();
            llenadoReporte();
        }

        private void EncabezadosTablaReporte()
        {
            DataGridTextColumn idColumn = new DataGridTextColumn
            {
                Header = " ID ",
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
                Header = " Hora de registro ",
                Binding = new Binding("hora"),
            };
            dvgReporte.Columns.Add(horaColumn);

            DataGridTextColumn asistencaColumn = new DataGridTextColumn
            {
                Header = " Asistencia ",
                Binding = new Binding("asistencia"),
            };
            dvgReporte.Columns.Add(asistencaColumn);
        }
        public void llenadoReporte()
        {

            List<TablaRepotePersonal> lTablaRepotePersonal = new List<TablaRepotePersonal>
            {
                new TablaRepotePersonal { id = "123", nombre = "Mauricio Leoanrdo Aleman Diaz", rol = "Docente",turno="Matutino",fecha = "10-05-2024",hora = "13:23:00",asistencia = "Registrada"},
            };

            dvgReporte.ItemsSource = lTablaRepotePersonal;

        }
    }

    public class TablaRepotePersonal
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string rol { get; set; }
        public string turno { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string asistencia { get; set; }
    }

}
