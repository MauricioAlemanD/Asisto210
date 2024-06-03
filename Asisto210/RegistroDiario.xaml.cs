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
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            EncabezadosTablaRegistroDiario();
            llenadoTablaRegistroDiario();
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

        private void llenadoTablaRegistroDiario()
        {
            List<TablaRegistroDiario> registrosDiarios = new List<TablaRegistroDiario>
            {
                new TablaRegistroDiario
                {
                    id = "001",
                    nombre = "Juan Pérez",
                    fechaRegistro = "2024-05-28",
                    horaRegistroIngreso = "08:00",
                    horaRegistroSalida = "17:00",
                    estadoRegistro = "Presente"
                },
                new TablaRegistroDiario
                {
                    id = "002",
                    nombre = "María García",
                    fechaRegistro = "2024-05-28",
                    horaRegistroIngreso = "08:15",
                    horaRegistroSalida = "17:10",
                    estadoRegistro = "Presente"
                }
            };
            dvgRegistroDiario.ItemsSource = registrosDiarios;
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
