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
    /// Lógica de interacción para Personal.xaml
    /// </summary>
    public partial class Personal : Page
    {
        public Personal()
        {
            InitializeComponent();
            EncabezadosTablaBusquedaPersonal();
            llenadoBusquedaPersonal();
        }


        public void llenadoBusquedaPersonal()
        {

            List<TablaBusquedaPersonal> lTablaBusquedaPersonal = new List<TablaBusquedaPersonal>
            {
                new TablaBusquedaPersonal { id = "123", nombre = "Mauricio Leoanrdo Aleman Diaz", rol = "Docente"},
            };

            dvgBusquedaPersonal.ItemsSource = lTablaBusquedaPersonal;

        }

        private void EncabezadosTablaBusquedaPersonal()
        {
            DataGridTextColumn idColumn = new DataGridTextColumn
            {
                Header = " ID ",
                Binding = new Binding("id"),
                Width = 30,
            };
            dvgBusquedaPersonal.Columns.Add(idColumn);

            DataGridTextColumn nombreColumn = new DataGridTextColumn
            {
                Header = " Nombre ",
                Binding = new Binding("nombre"),
            };
            dvgBusquedaPersonal.Columns.Add(nombreColumn);

            DataGridTextColumn rolColumn = new DataGridTextColumn
            {
                Header = " Rol ",
                Binding = new Binding("rol"),
            };
            dvgBusquedaPersonal.Columns.Add(rolColumn);
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }


    public class TablaBusquedaPersonal
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string rol { get; set; }
    }
}
