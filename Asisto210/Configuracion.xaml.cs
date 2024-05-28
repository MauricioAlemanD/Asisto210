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
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Page
    {
        public Configuracion()
        {
            InitializeComponent();
            EncabezadosTablaClases();
            llenadoTablaClases();
            EncabezadosTablaMaterias();
            llenadoTablaMaterias();
            EncabezadosTablaHoras();
                llenadoTablaHoras();
        }


        private void EncabezadosTablaClases()
        {
            dvgClases.Columns.Add(new DataGridTextColumn { Header = "Clave Clase", Binding = new Binding("claveClase") });
            dvgClases.Columns.Add(new DataGridTextColumn { Header = "Clave Personal", Binding = new Binding("cvePersonal") });
            dvgClases.Columns.Add(new DataGridTextColumn { Header = "Materia", Binding = new Binding("materia") });
            dvgClases.Columns.Add(new DataGridTextColumn { Header = "Hora Inicio", Binding = new Binding("horaInicio") });
            dvgClases.Columns.Add(new DataGridTextColumn { Header = "Hora Fin", Binding = new Binding("horaFin") });
        }

        private void llenadoTablaClases()
        {
            List<TablaClases> clases = new List<TablaClases>
            {
                new TablaClases { claveClase = "001", cvePersonal = "123", materia = "Matemáticas", horaInicio = "08:00", horaFin = "09:00" }
            };
            dvgClases.ItemsSource = clases;
        }

        private void EncabezadosTablaMaterias()
        {
            dvgMaterias.Columns.Add(new DataGridTextColumn { Header = "Clave Materia", Binding = new Binding("claveMateria") });
            dvgMaterias.Columns.Add(new DataGridTextColumn { Header = "Nombre Materia", Binding = new Binding("nombreMateria") });
            dvgMaterias.Columns.Add(new DataGridTextColumn { Header = "Acrónimo Materia", Binding = new Binding("acrMateria") });
        }

        private void llenadoTablaMaterias()
        {
            List<TablaMaterias> materias = new List<TablaMaterias>
            {
                new TablaMaterias { claveMateria = "MAT001", nombreMateria = "Matemáticas", acrMateria = "MAT" }
            };
            dvgMaterias.ItemsSource = materias;
        }

        private void EncabezadosTablaHoras()
        {
            dvgHoras.Columns.Add(new DataGridTextColumn { Header = "Clave Hora", Binding = new Binding("claveHora") });
            dvgHoras.Columns.Add(new DataGridTextColumn { Header = "Hora", Binding = new Binding("hora") });
        }

        private void llenadoTablaHoras()
        {
            List<TablaHoras> horas = new List<TablaHoras>
            {
                new TablaHoras { claveHora = "H001", hora = "08:00" }
            };
            dvgHoras.ItemsSource = horas;
        }

        private void EncabezadosTablaHorarioProfesor()
        {
           //dvgHorarioProfesor.Columns.Add(new DataGridTextColumn { Header = "Clave Horario", Binding = new Binding("claveHorario") });
         //   dvgHorarioProfesor.Columns.Add(new DataGridTextColumn { Header = "Ciclo Escolar", Binding = new Binding("cicloEscolar") });
        }

        private void llenadoTablaHorarioProfesor()
        {
            List<TablaHorarioProfesor> horarioProfesor = new List<TablaHorarioProfesor>
            {
                new TablaHorarioProfesor { claveHorario = "HP001", cicloEscolar = "2023-2024" }
            };
           // dvgHorarioProfesor.ItemsSource = horarioProfesor;
        }

        private void EncabezadosTablaHorarioClases()
        {
            //dvgHorarioClases.Columns.Add(new DataGridTextColumn { Header = "Clave Horario Clase", Binding = new Binding("claveHorarioClase") });
          //  dvgHorarioClases.Columns.Add(new DataGridTextColumn { Header = "Clave Horario", Binding = new Binding("claveHorario") });
            //dvgHorarioClases.Columns.Add(new DataGridTextColumn { Header = "Clave Clase", Binding = new Binding("claveClase") });
        }

        private void llenadoTablaHorarioClases()
        {
            List<TablaHorarioClases> horarioClases = new List<TablaHorarioClases>
            {
                new TablaHorarioClases { claveHorarioClase = "HC001", claveHorario = "HP001", claveClase = "001" }
            };
           // dvgHorarioClases.ItemsSource = horarioClases;
        }

        private void EncabezadosTablaCiclosEscolares()
        {
           // dvgCiclosEscolares.Columns.Add(new DataGridTextColumn { Header = "Clave Ciclo Escolar", Binding = new Binding("claveCicloEscolar") });
           // dvgCiclosEscolares.Columns.Add(new DataGridTextColumn { Header = "Año Inicio", Binding = new Binding("añoInicio") });
          //  dvgCiclosEscolares.Columns.Add(new DataGridTextColumn { Header = "Año Fin", Binding = new Binding("añoFin") });
        }

        private void llenadoTablaCiclosEscolares()
        {
            List<TablaCiclosEscolares> ciclosEscolares = new List<TablaCiclosEscolares>
            {
                new TablaCiclosEscolares { claveCicloEscolar = "CE2023", añoInicio = "2023", añoFin = "2024" }
            };
            //dvgCiclosEscolares.ItemsSource = ciclosEscolares;
        }




    }






    //Clases de tablas

    public class TablaClases
    {
        public string claveClase { get; set; }
        public string cvePersonal { get; set; }
        public string materia { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
    }

    public class TablaMaterias
    {
        public string claveMateria { get; set; }
        public string nombreMateria { get; set; }
        public string acrMateria { get; set; }
    }

    public class TablaHoras
    {
        public string claveHora { get; set; }
        public string hora { get; set; }
    }

    public class TablaHorarioProfesor
    { 
        public string claveHorario { get; set; }
        public string cicloEscolar { get; set; }
    }

    public class TablaHorarioClases
    {
        public string claveHorarioClase { get; set; }
        public string claveHorario { get; set; }
        public string claveClase { get; set; }
    }

    public class TablaCiclosEscolares
    {
        public string claveCicloEscolar { get; set; }
        public string añoInicio { get; set; }
        public string añoFin { get; set; }
    }


}
