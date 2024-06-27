using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Microsoft.Identity.Client;

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para HorarioProfesor.xaml
    /// </summary>
    public partial class HorarioProfesor : Page
    {

        public string G_cve_horario_profesor = "";
        public string  G_ciclo_escolar = "";
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

        Conexion conexion;
        public HorarioProfesor()
        {
            conexion = new Conexion();
            InitializeComponent();
            llenarCMBPersonal();
            llenarCMBTurnos();
            llenarCMBHorarios();
            llenarCMBPlantillas();
        }

        private void btnAñadirAsignatura_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            revisionHorario();
        }

        private void cmbHorarioProfesor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string cve_horario_editar = "";
                cve_horario_editar = cmbHorarioProfesor.SelectedItem.ToString().Substring(9, 4);

                if (cve_horario_editar != ""){
                    llenadoHorarioSeleccionado(cve_horario_editar);

                }
            }
            catch
            {

            }
        }

        private void btnEditarAsignatura_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            if (G_cve_horario_profesor != "" && G_ciclo_escolar != "" && G_cve_profesor !="" && G_turno != "" && cmbHorarioProfesor.SelectedItem == null )
            {

                MessageBoxPersonalizado("Estas seguro de actualizar el horario " + G_cve_horario_profesor, "Alerta");

                if (SHora1Lunes != null && SHora2Lunes != null && SHora3Lunes != null && SHora4Lunes != null && SHora5Lunes != null && SHora6Lunes != null && SHora7Lunes != null && SHora8Lunes != null && SHora9Lunes != null &&
    SHora1Martes != null && SHora2Martes != null && SHora3Martes != null && SHora4Martes != null && SHora5Martes != null && SHora6Martes != null && SHora7Martes != null && SHora8Martes != null && SHora9Martes != null &&
    SHora1Miercoles != null && SHora2Miercoles != null && SHora3Miercoles != null && SHora4Miercoles != null && SHora5Miercoles != null && SHora6Miercoles != null && SHora7Miercoles != null && SHora8Miercoles != null && SHora9Miercoles != null &&
    SHora1Jueves != null && SHora2Jueves != null && SHora3Jueves != null && SHora4Jueves != null && SHora5Jueves != null && SHora6Jueves != null && SHora7Jueves != null && SHora8Jueves != null && SHora9Jueves != null &&
    SHora1Viernes != null && SHora2Viernes != null && SHora3Viernes != null && SHora4Viernes != null && SHora5Viernes != null && SHora6Viernes != null && SHora7Viernes != null && SHora8Viernes != null && SHora9Viernes != null)
                { 

                    if (confirmacion = true){



                        string query = "DELETE FROM [dbo].[horario_profesor_clases_v3] WHERE cve_horario_profesor = @cve_horario_profesor";

                        SqlParameter[] parameters = {
                        new SqlParameter("@cve_horario_profesor", G_cve_horario_profesor),
                    };

                        try
                        {
                            // Pasar el array de parámetros a ExecuteNonQuery
                            conexion.ExecuteNonQuery(query, parameters);


                            limpiarTablaHorarios();
                        }
                        catch (Exception ex)
                        {
                            // Manejo de excepción, mostrar el error o registrar

                            MessageBoxPersonalizado("Error al editar el horario. " + ex, "Error");

                        }


                        //Clase Hora 1 Lunes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora1.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora1.Content.ToString(), Hora1Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 2 Lunes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora2.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora2.Content.ToString(), Hora2Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 3 Lune
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora3.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora3.Content.ToString(), Hora3Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 4 Lunes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora4.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora4.Content.ToString(), Hora4Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 5 Lunes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora5.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora5.Content.ToString(), Hora5Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 6 Lunes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora6.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora6.Content.ToString(), Hora6Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 7 Lunes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora7.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora7.Content.ToString(), Hora7Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 8 Lunes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora8.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora8.Content.ToString(), Hora8Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 9 Lunes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Lunes", TimeSpan.ParseExact(Hora9.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora9.Content.ToString(), Hora9Lunes.Content.ToString().Substring(0, 4));
                        //Clase Hora 1 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora1.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora1.Content.ToString(), Hora1Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 2 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora2.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora2.Content.ToString(), Hora2Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 3 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora3.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora3.Content.ToString(), Hora3Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 4 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora4.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora4.Content.ToString(), Hora4Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 5 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora5.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora5.Content.ToString(), Hora5Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 6 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora6.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora6.Content.ToString(), Hora6Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 7 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora7.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora7.Content.ToString(), Hora7Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 8 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora8.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora8.Content.ToString(), Hora8Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 9 Martes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Martes", TimeSpan.ParseExact(Hora9.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora9.Content.ToString(), Hora9Martes.Content.ToString().Substring(0, 4));
                        //Clase Hora 1 MiercolesG_turno
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora1.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora1.Content.ToString(), Hora1Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 2 Miercoles
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora2.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora2.Content.ToString(), Hora2Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 3 Miercoles
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora3.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora3.Content.ToString(), Hora3Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 4 Miercoles
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora4.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora4.Content.ToString(), Hora4Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 5 Miercoles
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora5.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora5.Content.ToString(), Hora5Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 6 Miercoles
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora6.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora6.Content.ToString(), Hora6Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 7 Miercoles
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora7.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora7.Content.ToString(), Hora7Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 8 Miercoles
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora8.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora8.Content.ToString(), Hora8Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 9 Miercoles
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Miercoles", TimeSpan.ParseExact(Hora9.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora9.Content.ToString(), Hora9Miercoles.Content.ToString().Substring(0, 4));
                        //Clase Hora 1 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora1.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora1.Content.ToString(), Hora1Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 2 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora2.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora2.Content.ToString(), Hora2Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 3 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora3.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora3.Content.ToString(), Hora3Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 4 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora4.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora4.Content.ToString(), Hora4Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 5 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora5.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora5.Content.ToString(), Hora5Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 6 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora6.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora6.Content.ToString(), Hora6Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 7 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora7.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora7.Content.ToString(), Hora7Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 8 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora8.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora8.Content.ToString(), Hora8Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 9 Jueves
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Jueves", TimeSpan.ParseExact(Hora9.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora9.Content.ToString(), Hora9Jueves.Content.ToString().Substring(0, 4));
                        //Clase Hora 1 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora1.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora1.Content.ToString(), Hora1Viernes.Content.ToString().Substring(0, 4));
                        //Clase Hora 2 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora2.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora2.Content.ToString(), Hora2Viernes.Content.ToString().Substring(0, 4));
                        //Clase Hora 3 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora3.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora3.Content.ToString(), Hora3Viernes.Content.ToString().Substring(0, 4));
                        //Clase Hora 4 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora4.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora4.Content.ToString(), Hora4Viernes.Content.ToString().Substring(0, 4));
                        //Clase Hora 5 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora5.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora5.Content.ToString(), Hora5Viernes.Content.ToString().Substring(0, 4));
                        //Clase Hora 6 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora6.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora6.Content.ToString(), Hora6Viernes.Content.ToString().Substring(0, 4));
                        //Clase Hora 7 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora7.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora7.Content.ToString(), Hora7Viernes.Content.ToString().Substring(0, 4));
                        //Clase Hora 8 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora8.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora8.Content.ToString(), Hora8Viernes.Content.ToString().Substring(0, 4));
                        //Clase Hora 9 Viernes
                        añadirHorarioProfesorClases(G_cve_horario_profesor, G_ciclo_escolar, G_cve_profesor, G_turno, "Viernes", TimeSpan.ParseExact(Hora9.Content.ToString().Substring(0, 5), @"hh\:mm", null), Hora9.Content.ToString(), Hora9Viernes.Content.ToString().Substring(0, 4));

                        MessageBoxPersonalizado("Se actualizaron las clases al horario." + G_cve_horario_profesor + ".", "Confimación");



                    }
                }

                else{

                }

                   
            }
            else
            {
                MessageBoxPersonalizado("Selecciona un horario para editar.","Alerta");
            }
        }

        private void btnEliminarHorario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {

                MessageBoxPersonalizado("Confirma que desea eliminar el horiario: " + cmbHorarioProfesor.SelectedItem.ToString().Substring(9, 4) + ".", "Alerta");
                if (confirmacion)
                {
                    string cve_horario_profesor = cmbHorarioProfesor.SelectedItem.ToString().Substring(9, 4);

                    string query = "DELETE FROM [dbo].[horario_profesor_clases_v3] WHERE cve_horario_profesor = @cve_horario_profesor; DELETE FROM [dbo].[horario_profesor] WHERE cve_horario_profesor = @cve_horario_profesor";

                    SqlParameter[] parameters = {
                        new SqlParameter("@cve_horario_profesor", cve_horario_profesor),
                    };

                    try
                    {
                        // Pasar el array de parámetros a ExecuteNonQuery
                        conexion.ExecuteNonQuery(query, parameters);
                        MessageBoxPersonalizado("Se Borró el horario." + cve_horario_profesor + ".", "Confimación");
                        limpiarTablaHorarios();
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepción, mostrar el error o registrar

                        MessageBoxPersonalizado("Error al borrar el horario. " + ex, "Error");
                    }
                    
                }
                llenarCMBHorarios();
            }
            catch
            {
                MessageBoxPersonalizado("No se ha seleccionado un horario para eliminar.","Alerta");
            }
        }

        private void revisionHorario()
        {
            if (SHora1 != "" &&
                SHora2 != "" &&
                SHora3 != "" &&
                SHora4 != "" &&
                SHora5 != "" &&
                SHora6 != "" &&
                SHora7 != "" &&
                SHora8 != "" &&
                SHora9 != "")
            {

                if (SHora1Lunes != null && SHora2Lunes != null && SHora3Lunes != null && SHora4Lunes != null && SHora5Lunes != null && SHora6Lunes != null && SHora7Lunes != null && SHora8Lunes != null && SHora9Lunes != null &&
    SHora1Martes != null && SHora2Martes != null && SHora3Martes != null && SHora4Martes != null && SHora5Martes != null && SHora6Martes != null && SHora7Martes != null && SHora8Martes != null && SHora9Martes != null &&
    SHora1Miercoles != null && SHora2Miercoles != null && SHora3Miercoles != null && SHora4Miercoles != null && SHora5Miercoles != null && SHora6Miercoles != null && SHora7Miercoles != null && SHora8Miercoles != null && SHora9Miercoles != null &&
    SHora1Jueves != null && SHora2Jueves != null && SHora3Jueves != null && SHora4Jueves != null && SHora5Jueves != null && SHora6Jueves != null && SHora7Jueves != null && SHora8Jueves != null && SHora9Jueves != null &&
    SHora1Viernes != null && SHora2Viernes != null && SHora3Viernes != null && SHora4Viernes != null && SHora5Viernes != null && SHora6Viernes != null && SHora7Viernes != null && SHora8Viernes != null && SHora9Viernes != null)
                {

                    string ciclo_escolar_ed = "";
                    ciclo_escolar_ed = txtAñoInicio.Text + "-" + txtAñoFin.Text;
                    
                        try 
                        {

                            string cve_docente = "";
                            cve_docente = cmbDocente.SelectedItem.ToString();
                            cve_docente = cve_docente.Substring(0, 4);

                            if (ValidarCicloEscolar(ciclo_escolar_ed))
                            {

                                try
                                {
                                    string turno = "";
                                    turno = cmbTurnos.SelectedItem.ToString();

                                try
                                    {

                                    string cve_horario_profesor = "";
                                    cve_horario_profesor = ObtenerClaveHorarioProfesor();


                                    //Insertar a horario profesor
                                    añadirHorarioProfesor(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno);

                                    //Insertar clase a horario profesor

                                    if (confirmacion = true)
                                    {
                                        //Clase Hora 1 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora1.Substring(0, 5), @"hh\:mm", null), SHora1, SHora1Lunes.Substring(0, 4));
                                        //Clase Hora 2 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora2.Substring(0, 5), @"hh\:mm", null), SHora2, SHora2Lunes.Substring(0, 4));
                                        //Clase Hora 3 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora3.Substring(0, 5), @"hh\:mm", null), SHora3, SHora3Lunes.Substring(0, 4));
                                        //Clase Hora 4 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora4.Substring(0, 5), @"hh\:mm", null), SHora4, SHora4Lunes.Substring(0, 4));
                                        //Clase Hora 5 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora5.Substring(0, 5), @"hh\:mm", null), SHora5, SHora5Lunes.Substring(0, 4));
                                        //Clase Hora 6 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora6.Substring(0, 5), @"hh\:mm", null), SHora6, SHora6Lunes.Substring(0, 4));
                                        //Clase Hora 7 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora7.Substring(0, 5), @"hh\:mm", null), SHora7, SHora7Lunes.Substring(0, 4));
                                        //Clase Hora 8 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora8.Substring(0, 5), @"hh\:mm", null), SHora8, SHora8Lunes.Substring(0, 4));
                                        //Clase Hora 9 Lunes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Lunes", TimeSpan.ParseExact(SHora9.Substring(0, 5), @"hh\:mm", null), SHora9, SHora9Lunes.Substring(0, 4));
                                        //Clase Hora 1 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora1.Substring(0, 5), @"hh\:mm", null), SHora1, SHora1Martes.Substring(0, 4));
                                        //Clase Hora 2 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora2.Substring(0, 5), @"hh\:mm", null), SHora2, SHora2Martes.Substring(0, 4));
                                        //Clase Hora 3 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora3.Substring(0, 5), @"hh\:mm", null), SHora3, SHora3Martes.Substring(0, 4));
                                        //Clase Hora 4 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora4.Substring(0, 5), @"hh\:mm", null), SHora4, SHora4Martes.Substring(0, 4));
                                        //Clase Hora 5 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora5.Substring(0, 5), @"hh\:mm", null), SHora5, SHora5Martes.Substring(0, 4));
                                        //Clase Hora 6 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora6.Substring(0, 5), @"hh\:mm", null), SHora6, SHora6Martes.Substring(0, 4));
                                        //Clase Hora 7 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora7.Substring(0, 5), @"hh\:mm", null), SHora7, SHora7Martes.Substring(0, 4));
                                        //Clase Hora 8 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora8.Substring(0, 5), @"hh\:mm", null), SHora8, SHora8Martes.Substring(0, 4));
                                        //Clase Hora 9 Martes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Martes", TimeSpan.ParseExact(SHora9.Substring(0, 5), @"hh\:mm", null), SHora9, SHora9Martes.Substring(0, 4));
                                        //Clase Hora 1 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora1.Substring(0, 5), @"hh\:mm", null), SHora1, SHora1Miercoles.Substring(0, 4));
                                        //Clase Hora 2 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora2.Substring(0, 5), @"hh\:mm", null), SHora2, SHora2Miercoles.Substring(0, 4));
                                        //Clase Hora 3 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora3.Substring(0, 5), @"hh\:mm", null), SHora3, SHora3Miercoles.Substring(0, 4));
                                        //Clase Hora 4 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora4.Substring(0, 5), @"hh\:mm", null), SHora4, SHora4Miercoles.Substring(0, 4));
                                        //Clase Hora 5 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora5.Substring(0, 5), @"hh\:mm", null), SHora5, SHora5Miercoles.Substring(0, 4));
                                        //Clase Hora 6 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora6.Substring(0, 5), @"hh\:mm", null), SHora6, SHora6Miercoles.Substring(0, 4));
                                        //Clase Hora 7 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora7.Substring(0, 5), @"hh\:mm", null), SHora7, SHora7Miercoles.Substring(0, 4));
                                        //Clase Hora 8 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora8.Substring(0, 5), @"hh\:mm", null), SHora8, SHora8Miercoles.Substring(0, 4));
                                        //Clase Hora 9 Miercoles
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Miercoles", TimeSpan.ParseExact(SHora9.Substring(0, 5), @"hh\:mm", null), SHora9, SHora9Miercoles.Substring(0, 4));
                                        //Clase Hora 1 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora1.Substring(0, 5), @"hh\:mm", null), SHora1, SHora1Jueves.Substring(0, 4));
                                        //Clase Hora 2 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora2.Substring(0, 5), @"hh\:mm", null), SHora2, SHora2Jueves.Substring(0, 4));
                                        //Clase Hora 3 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora3.Substring(0, 5), @"hh\:mm", null), SHora3, SHora3Jueves.Substring(0, 4));
                                        //Clase Hora 4 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora4.Substring(0, 5), @"hh\:mm", null), SHora4, SHora4Jueves.Substring(0, 4));
                                        //Clase Hora 5 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora5.Substring(0, 5), @"hh\:mm", null), SHora5, SHora5Jueves.Substring(0, 4));
                                        //Clase Hora 6 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora6.Substring(0, 5), @"hh\:mm", null), SHora6, SHora6Jueves.Substring(0, 4));
                                        //Clase Hora 7 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora7.Substring(0, 5), @"hh\:mm", null), SHora7, SHora7Jueves.Substring(0, 4));
                                        //Clase Hora 8 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora8.Substring(0, 5), @"hh\:mm", null), SHora8, SHora8Jueves.Substring(0, 4));
                                        //Clase Hora 9 Jueves
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Jueves", TimeSpan.ParseExact(SHora9.Substring(0, 5), @"hh\:mm", null), SHora9, SHora9Jueves.Substring(0, 4));
                                        //Clase Hora 1 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora1.Substring(0, 5), @"hh\:mm", null), SHora1, SHora1Viernes.Substring(0, 4));
                                        //Clase Hora 2 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora2.Substring(0, 5), @"hh\:mm", null), SHora2, SHora2Viernes.Substring(0, 4));
                                        //Clase Hora 3 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora3.Substring(0, 5), @"hh\:mm", null), SHora3, SHora3Viernes.Substring(0, 4));
                                        //Clase Hora 4 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora4.Substring(0, 5), @"hh\:mm", null), SHora4, SHora4Viernes.Substring(0, 4));
                                        //Clase Hora 5 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora5.Substring(0, 5), @"hh\:mm", null), SHora5, SHora5Viernes.Substring(0, 4));
                                        //Clase Hora 6 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora6.Substring(0, 5), @"hh\:mm", null), SHora6, SHora6Viernes.Substring(0, 4));
                                        //Clase Hora 7 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora7.Substring(0, 5), @"hh\:mm", null), SHora7, SHora7Viernes.Substring(0, 4));
                                        //Clase Hora 8 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora8.Substring(0, 5), @"hh\:mm", null), SHora8, SHora8Viernes.Substring(0, 4));
                                        //Clase Hora 9 Viernes
                                        añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno, "Viernes", TimeSpan.ParseExact(SHora9.Substring(0, 5), @"hh\:mm", null), SHora9, SHora9Viernes.Substring(0, 4));

                                        MessageBoxPersonalizado("Se añadieron las clases al horario." + cve_horario_profesor + ".", "Confimación");
                                    }

                                    llenarCMBHorarios();
                                    limpiarTablaHorarios();

                                }
                                catch (Exception ex)
                                {
                                    MessageBoxPersonalizado("Error al crear el horario. " + ex, "Error");
                                    }
                                }
                                catch
                                {
                                    MessageBoxPersonalizado("No se ingresó un turno para el horario, seleccione uno.", "Alerta");
                                }


                            }
                            else
                            {
                            MessageBoxPersonalizado("Verifique el ciclo escolar.", "Alerta");
                            }

                        }
                        catch
                        {
                            MessageBoxPersonalizado("No se ingresó un docente para el horario, seleccione uno.", "Alerta");
                        }
                 
                }
                else
                {
                    MessageBoxPersonalizado("No se ingresaron todas las asignaturas correctamente", "Alerta");
                    if (SHora1Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 1 Lunes", "Alerta");
                    }
                    if (SHora2Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 2 Lunes", "Alerta");
                    }
                    if (SHora3Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 3 Lunes", "Alerta");
                    }
                    if (SHora4Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 4 Lunes", "Alerta");
                    }
                    if (SHora5Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 5 Lunes", "Alerta");
                    }
                    if (SHora6Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 6 Lunes", "Alerta");
                    }
                    if (SHora7Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 7 Lunes", "Alerta");
                    }
                    if (SHora8Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 8 Lunes", "Alerta");
                    }
                    if (SHora9Lunes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 9 Lunes", "Alerta");
                    }

                    if (SHora1Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 1 Martes", "Alerta");
                    }
                    if (SHora2Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 2 Martes", "Alerta");
                    }
                    if (SHora3Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 3 Martes", "Alerta");
                    }
                    if (SHora4Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 4 Martes", "Alerta");
                    }
                    if (SHora5Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 5 Martes", "Alerta");
                    }
                    if (SHora6Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 6 Martes", "Alerta");
                    }
                    if (SHora7Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 7 Martes", "Alerta");
                    }
                    if (SHora8Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 8 Martes", "Alerta");
                    }
                    if (SHora9Martes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 9 Martes", "Alerta");
                    }

                    if (SHora1Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 1 Miércoles", "Alerta");
                    }
                    if (SHora2Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 2 Miércoles", "Alerta");
                    }
                    if (SHora3Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 3 Miércoles", "Alerta");
                    }
                    if (SHora4Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 4 Miércoles", "Alerta");
                    }
                    if (SHora5Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 5 Miércoles", "Alerta");
                    }
                    if (SHora6Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 6 Miércoles", "Alerta");
                    }
                    if (SHora7Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 7 Miércoles", "Alerta");
                    }
                    if (SHora8Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 8 Miércoles", "Alerta");
                    }
                    if (SHora9Miercoles == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 9 Miércoles", "Alerta");
                    }

                    if (SHora1Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 1 Jueves", "Alerta");
                    }
                    if (SHora2Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 2 Jueves", "Alerta");
                    }
                    if (SHora3Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 3 Jueves", "Alerta");
                    }
                    if (SHora4Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 4 Jueves", "Alerta");
                    }
                    if (SHora5Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 5 Jueves", "Alerta");
                    }
                    if (SHora6Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 6 Jueves", "Alerta");
                    }
                    if (SHora7Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 7 Jueves", "Alerta");
                    }
                    if (SHora8Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 8 Jueves", "Alerta");
                    }
                    if (SHora9Jueves == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 9 Jueves", "Alerta");
                    }

                    if (SHora1Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 1 Viernes", "Alerta");
                    }
                    if (SHora2Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 2 Viernes", "Alerta");
                    }
                    if (SHora3Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 3 Viernes", "Alerta");
                    }
                    if (SHora4Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 4 Viernes", "Alerta");
                    }
                    if (SHora5Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 5 Viernes", "Alerta");
                    }
                    if (SHora6Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 6 Viernes", "Alerta");
                    }
                    if (SHora7Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 7 Viernes", "Alerta");
                    }
                    if (SHora8Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 8 Viernes", "Alerta");
                    }
                    if (SHora9Viernes == null)
                    {
                        MessageBoxPersonalizado("Corrobora la hora para Hora 9 Viernes", "Alerta");
                    }

                }

            }
            else
            {
                MessageBoxPersonalizado("No se ingresaron todas las horas correctamente", "Alerta");
            }
        }

        private string ObtenerClaveHorarioProfesor()
        {
            string ultimaID = "";

            string query = "SELECT TOP 1 cve_horario_profesor FROM horario_profesor ORDER BY cve_horario_profesor DESC";

            try
            {
                using (var reader = conexion.ExecuteReader(query))
                {
                    if (reader.Read())
                    {
                        ultimaID = reader["cve_horario_profesor"].ToString().Trim();

                        int ultimoNumero;
                        if (int.TryParse(ultimaID, out ultimoNumero))
                        {
                            ultimoNumero++;
                            ultimaID = ultimoNumero.ToString().PadLeft(4, '0');
                        }
                    }
                }

                if (string.IsNullOrEmpty(ultimaID))
                {
                    ultimaID = "0001";
                }
            }
            catch (Exception ex)
            {
                MessageBoxPersonalizado("No se puede agregar el horario.", "Error");
            }

            return ultimaID;
        }

        private void añadirHorarioProfesor(string cve_horario_profesor, string ciclo_escolar, string cve_profesor, string turno)
        {
            confirmacion = false;

            string query = "INSERT INTO [dbo].[horario_profesor] ([cve_horario_profesor],[ciclo_escolar],[cve_profesor],[turno])" +
            "VALUES (@cve_horario_profesor, @ciclo_escolar,@cve_profesor,@turno)";

            SqlParameter[] parameters = {
        new SqlParameter("@cve_horario_profesor", cve_horario_profesor),
        new SqlParameter("@ciclo_escolar", ciclo_escolar),
        new SqlParameter("@cve_profesor", cve_profesor),
        new SqlParameter("@turno", turno),
    };

            try
            {                
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
                MessageBoxPersonalizado("Se creo el horario." + cve_horario_profesor + ".", "Confimación");
                confirmacion = true;
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar
                confirmacion = false;
                MessageBoxPersonalizado("Error al crear el horario en HORARIOS PROFESOR. " + ex , "Error");
            }


        }

        private void añadirHorarioProfesorClases(string cve_horario_profesor, string ciclo_escolar, string cve_profesor, string turno,string dia_clase, TimeSpan hora_clase,string hora_clase_formateada, string cve_clase)
        {
            string query = "INSERT INTO [dbo].[horario_profesor_clases_v3] ([cve_horario_profesor],[ciclo_escolar],[cve_profesor],[turno],[dia_clase],[hora_clase],[hora_clase_formateada],[cve_clase])" +
            "VALUES (@cve_horario_profesor, @ciclo_escolar,@cve_profesor,@turno,@dia_clase,@hora_clase,@hora_clase_formateada,@cve_clase)";

            SqlParameter[] parameters = {
        new SqlParameter("@cve_horario_profesor", cve_horario_profesor),
        new SqlParameter("@ciclo_escolar", ciclo_escolar),
        new SqlParameter("@cve_profesor", cve_profesor),
        new SqlParameter("@turno", turno),
        new SqlParameter("@dia_clase", dia_clase),
        new SqlParameter("@hora_clase", hora_clase),
        new SqlParameter("@hora_clase_formateada", hora_clase_formateada),
        new SqlParameter("@cve_clase", cve_clase),
    };

            try
            {
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
                
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar

                MessageBoxPersonalizado("Error al crear el horario en CLASES HORARIOS PROFESOR. " + ex, "Error");
            }


        }
        
        static bool ValidarCicloEscolar(string cicloEscolar)
        {
            string patron = @"^\d{4}-\d{4}$";
            Regex regex = new Regex(patron);

            return regex.IsMatch(cicloEscolar);
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

        private void llenadoHorarioSeleccionado(string cve_horario_profesor)
        {
            G_cve_horario_profesor = "";
            G_ciclo_escolar = "";
            G_cve_profesor  ="";
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

            query = "WITH CTE AS (" +
                "SELECT [cve_horario_profesor]" +
                ",[ciclo_escolar]" +
                ",[cve_profesor]" +
                ",[turno],[dia_clase]" +
                ",[hora_clase]" +
                ",[hora_clase_formateada]" +
                ",[cve_clase]," +
                "ROW_NUMBER() OVER (PARTITION BY [hora_clase] ORDER BY [cve_horario_profesor])" +
                " AS rn FROM [dbo].[horario_profesor_clases_v3]" +
                ") " +
                "SELECT [cve_horario_profesor]" +
                ",[ciclo_escolar]" +
                ",[cve_profesor]" +
                ",[turno],[dia_clase]" +
                ",[hora_clase]" +
                ",[hora_clase_formateada]" +
                ",[cve_clase] " +
                "FROM CTE " +
                "WHERE rn = 1 AND [cve_horario_profesor] = '" + cve_horario_profesor + "'";

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
            query = "select horario_profesor_clases_v3.cve_clase, meterias.nombre_materia from horario_profesor_clases_v3 inner join meterias on horario_profesor_clases_v3.cve_clase = meterias.cve_materia where cve_horario_profesor = '"+cve_horario_profesor+"' and dia_clase = 'Lunes' and hora_clase_formateada = '"+SHora1+"'";
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
            Hora1.Content =SHora1;
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

        private void llenarCMBPersonal()
        {
            cmbDocente.Items.Clear();
            string query = "select personal.cve_personal,personal.nombre,personal.apelldio_pateno,personal.apellido_materno,roles.descripcion from personal inner join roles on personal.rol_personal = roles.cve_rol where rol_personal = '3'";

            using (var reader = conexion.ExecuteReader(query))
            {
                string personal = "";
                while (reader.Read())
                {
                    personal = "";
                    personal = reader["cve_personal"].ToString() + " " + reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString();
                    cmbDocente.Items.Add(personal);
                }
            }
        }

        private void llenarCMBTurnos()
        {
            cmbTurnos.Items.Add("Matutino");
            cmbTurnos.Items.Add("Vespertino");
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
                    horaioProfesor = "Horario: " + reader["cve_horario_profesor"].ToString() + " Docente: " + reader["cve_profesor"] +"-"+ reader["nombre"].ToString() + " " + reader["apelldio_pateno"].ToString() + " " + reader["apellido_materno"].ToString() + " Ciclo escolar: [" + reader["ciclo_escolar"] + "] Turno: " + reader["turno"];
                    cmbHorarioProfesor.Items.Add(horaioProfesor);
                }
            }
        }

        private void llenarCMBPlantillas()
        {
            cmbPlantillaHorario.Items.Clear();
            string query = "select * from plantilla_horas";

            using (var reader = conexion.ExecuteReader(query))
            {                
                while (reader.Read())
                {
                    cmbPlantillaHorario.Items.Add(reader["hora"].ToString());
                }
            }
        }

        private void limpiarTablaHorarios()
        {
            Hora1.Content = "Ingresa hora";
            Hora2.Content = "Ingresa hora";
            Hora3.Content = "Ingresa hora";
            Hora4.Content = "Ingresa hora";
            Hora5.Content = "Ingresa hora";
            Hora6.Content = "Ingresa hora";
            Hora7.Content = "Ingresa hora";
            Hora8.Content = "Ingresa hora";
            Hora9.Content = "Ingresa hora";

            SHora1 = "0";
            SHora2 = "0";
            SHora3 = "0";
            SHora4 = "0";
            SHora5 = "0";
            SHora6 = "0";
            SHora7 = "0";
            SHora8 = "0";
            SHora9 = "0";

            Hora1Lunes.Content = "Asignatura";
            Hora2Lunes.Content = "Asignatura";
            Hora3Lunes.Content = "Asignatura";
            Hora4Lunes.Content = "Asignatura";
            Hora5Lunes.Content = "Asignatura";
            Hora6Lunes.Content = "Asignatura";
            Hora7Lunes.Content = "Asignatura";
            Hora8Lunes.Content = "Asignatura";
            Hora9Lunes.Content = "Asignatura";

            SHora1Lunes = "0";
            SHora2Lunes = "0";
            SHora3Lunes = "0";
            SHora4Lunes = "0";
            SHora5Lunes = "0";
            SHora6Lunes = "0";
            SHora7Lunes = "0";
            SHora8Lunes = "0";
            SHora9Lunes = "0";

            Hora1Martes.Content = "Asignatura";
            Hora2Martes.Content = "Asignatura";
            Hora3Martes.Content = "Asignatura";
            Hora4Martes.Content = "Asignatura";
            Hora5Martes.Content = "Asignatura";
            Hora6Martes.Content = "Asignatura";
            Hora7Martes.Content = "Asignatura";
            Hora8Martes.Content = "Asignatura";
            Hora9Martes.Content = "Asignatura";

            SHora1Martes = "0";
            SHora2Martes = "0";
            SHora3Martes = "0";
            SHora4Martes = "0";
            SHora5Martes = "0";
            SHora6Martes = "0";
            SHora7Martes = "0";
            SHora8Martes = "0";
            SHora9Martes = "0";


            Hora1Miercoles.Content = "Asignatura";
            Hora2Miercoles.Content = "Asignatura";
            Hora3Miercoles.Content = "Asignatura";
            Hora4Miercoles.Content = "Asignatura";
            Hora5Miercoles.Content = "Asignatura";
            Hora6Miercoles.Content = "Asignatura";
            Hora7Miercoles.Content = "Asignatura";
            Hora8Miercoles.Content = "Asignatura";
            Hora9Miercoles.Content = "Asignatura";

            SHora1Miercoles = "0";
            SHora2Miercoles = "0";
            SHora3Miercoles = "0";
            SHora4Miercoles = "0";
            SHora5Miercoles = "0";
            SHora6Miercoles = "0";
            SHora7Miercoles = "0";
            SHora8Miercoles = "0";
            SHora9Miercoles = "0";


            Hora1Jueves.Content = "Asignatura";
            Hora2Jueves.Content = "Asignatura";
            Hora3Jueves.Content = "Asignatura";
            Hora4Jueves.Content = "Asignatura";
            Hora5Jueves.Content = "Asignatura";
            Hora6Jueves.Content = "Asignatura";
            Hora7Jueves.Content = "Asignatura";
            Hora8Jueves.Content = "Asignatura";
            Hora9Jueves.Content = "Asignatura";

            SHora1Jueves = "0";
            SHora2Jueves = "0";
            SHora3Jueves = "0";
            SHora4Jueves = "0";
            SHora5Jueves = "0";
            SHora6Jueves = "0";
            SHora7Jueves = "0";
            SHora8Jueves = "0";
            SHora9Jueves = "0";


            Hora2Viernes.Content = "Asignatura";
            Hora3Viernes.Content = "Asignatura";
            Hora4Viernes.Content = "Asignatura";
            Hora5Viernes.Content = "Asignatura";
            Hora1Viernes.Content = "Asignatura";
            Hora6Viernes.Content = "Asignatura";
            Hora7Viernes.Content = "Asignatura";
            Hora8Viernes.Content = "Asignatura";
            Hora9Viernes.Content = "Asignatura";
            
            SHora1Viernes = "0";
            SHora2Viernes = "0";
            SHora3Viernes = "0";
            SHora4Viernes = "0";
            SHora5Viernes = "0";
            SHora6Viernes = "0";
            SHora7Viernes = "0";
            SHora8Viernes = "0";
            SHora9Viernes = "0";


        }

        private void Hora1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora1 = "";
            SHora1 = InputMessageBox.Show("Hora 1");
            if (SHora1 != null)
            {
                Hora1.Content = SHora1;
            }
            else
            {

            }

        }

        private void Hora2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora2 = "";
            SHora2 = InputMessageBox.Show("Hora 2");
            if (SHora2 != null)
            {
                Hora2.Content = SHora2;
            }
            else
            {

            }
        }

        private void Hora3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora3 = "";
            SHora3 = InputMessageBox.Show("Hora 3");
            if (SHora3 != null)
            {
                Hora3.Content = SHora3;
            }
            else
            {

            }
        }

        private void Hora4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora4 = "";
            SHora4 = InputMessageBox.Show("Hora 4");
            if (SHora4 != null)
            {
                Hora4.Content = SHora4;
            }
            else
            {

            }
        }

        private void Hora5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora5 = "";
            SHora5 = InputMessageBox.Show("Hora 5");
            if (SHora5 != null)
            {
                Hora5.Content = SHora5;
            }
            else
            {

            }
        }

        private void Hora6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora6 = "";
            SHora6 = InputMessageBox.Show("Hora 6");
            if (SHora6 != null)
            {
                Hora6.Content = SHora6;
            }
            else
            {

            }
        }

        private void Hora7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora7 = "";
            SHora7 = InputMessageBox.Show("Hora 7");
            if (SHora7 != null)
            {
                Hora7.Content = SHora7;
            }
            else
            {

            }
        }

        private void Hora8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora8 = "";
            SHora8 = InputMessageBox.Show("Hora 8");
            if (SHora8 != null)
            {
                Hora8.Content = SHora8;
            }
            else
            {

            }
        }

        private void Hora9_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora9 = "";
            SHora9 = InputMessageBox.Show("Hora 9");
            if (SHora9 != null)
            {
                Hora9.Content = SHora9;
            }
            else
            {

            }
        }

        private void Hora1Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora1Lunes = "";
            SHora1Lunes = AsignacionAsignatura.Show("Hora 1 de Lunes");

            if (SHora1Lunes != null)
            {
                Hora1Lunes.Content = SHora1Lunes;
            }
            else
            {

            }

        }

        private void Hora2Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora2Lunes = "";
            SHora2Lunes = AsignacionAsignatura.Show("Hora 2 de Lunes");

            if (SHora2Lunes != null)
            {
                Hora2Lunes.Content = SHora2Lunes;
            }
            else
            {

            }
        }

        private void Hora3Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora3Lunes = "";
            SHora3Lunes = AsignacionAsignatura.Show("Hora 3 de Lunes");

            if (SHora3Lunes != null)
            {
                Hora3Lunes.Content = SHora3Lunes;
            }
            else
            {

            }
        }

        private void Hora4Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora4Lunes = "";
            SHora4Lunes = AsignacionAsignatura.Show("Hora 4 de Lunes");

            if (SHora4Lunes != null)
            {
                Hora4Lunes.Content = SHora4Lunes;
            }
            else
            {

            }
        }

        private void Hora5Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora5Lunes = "";
            SHora5Lunes = AsignacionAsignatura.Show("Hora 5 de Lunes");

            if (SHora5Lunes != null)
            {
                Hora5Lunes.Content = SHora5Lunes;
            }
            else
            {

            }
        }

        private void Hora6Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora6Lunes = "";
            SHora6Lunes = AsignacionAsignatura.Show("Hora 6 de Lunes");

            if (SHora6Lunes != null)
            {
                Hora6Lunes.Content = SHora6Lunes;
            }
            else
            {

            }
        }

        private void Hora7Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora7Lunes = "";
            SHora7Lunes = AsignacionAsignatura.Show("Hora 7 de Lunes");

            if (SHora7Lunes != null)
            {
                Hora7Lunes.Content = SHora7Lunes;
            }
            else
            {

            }
        }

        private void Hora8Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora8Lunes = "";
            SHora8Lunes = AsignacionAsignatura.Show("Hora 8 de Lunes");

            if (SHora8Lunes != null)
            {
                Hora8Lunes.Content = SHora8Lunes;
            }
            else
            {

            }
        }

        private void Hora9Lunes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora9Lunes = "";
            SHora9Lunes = AsignacionAsignatura.Show("Hora 9 de Lunes");

            if (SHora9Lunes != null)
            {
                Hora9Lunes.Content = SHora9Lunes;
            }
            else
            {

            }
        }

        private void Hora1Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora1Martes = "";
            SHora1Martes = AsignacionAsignatura.Show("Hora 1 de Martes");

            if (SHora1Martes != null)
            {
                Hora1Martes.Content = SHora1Martes;
            }
            else
            {

            }
        }

        private void Hora2Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora2Martes = "";
            SHora2Martes = AsignacionAsignatura.Show("Hora 2 de Martes");

            if (SHora2Martes != null)
            {
                Hora2Martes.Content = SHora2Martes;
            }
            else
            {

            }
        }

        private void Hora3Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora3Martes = "";
            SHora3Martes = AsignacionAsignatura.Show("Hora 3 de Martes");

            if (SHora3Martes != null)
            {
                Hora3Martes.Content = SHora3Martes;
            }
            else
            {

            }
        }

        private void Hora4Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora4Martes = "";
            SHora4Martes = AsignacionAsignatura.Show("Hora 4 de Martes");

            if (SHora4Martes != null)
            {
                Hora4Martes.Content = SHora4Martes;
            }
            else
            {

            }
        }

        private void Hora5Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora5Martes = "";
            SHora5Martes = AsignacionAsignatura.Show("Hora 5 de Martes");

            if (SHora5Martes != null)
            {
                Hora5Martes.Content = SHora5Martes;
            }
            else
            {

            }
        }

        private void Hora6Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora6Martes = "";
            SHora6Martes = AsignacionAsignatura.Show("Hora 6 de Martes");

            if (SHora6Martes != null)
            {
                Hora6Martes.Content = SHora6Martes;
            }
            else
            {

            }
        }

        private void Hora7Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora7Martes = "";
            SHora7Martes = AsignacionAsignatura.Show("Hora 7 de Martes");

            if (SHora7Martes != null)
            {
                Hora7Martes.Content = SHora7Martes;
            }
            else
            {

            }
        }

        private void Hora8Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora8Martes = "";
            SHora8Martes = AsignacionAsignatura.Show("Hora 8 de Martes");

            if (SHora8Martes != null)
            {
                Hora8Martes.Content = SHora8Martes;
            }
            else
            {

            }
        }

        private void Hora9Martes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora9Martes = "";
            SHora9Martes = AsignacionAsignatura.Show("Hora 9 de Martes");

            if (SHora9Martes != null)
            {
                Hora9Martes.Content = SHora9Martes;
            }
            else
            {

            }
        }

        private void Hora1Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora1Miercoles = "";
            SHora1Miercoles = AsignacionAsignatura.Show("Hora 1 de Miercoles");

            if (SHora1Miercoles != null)
            {
                Hora1Miercoles.Content = SHora1Miercoles;
            }
            else
            {

            }
        }

        private void Hora2Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora2Miercoles = "";
            SHora2Miercoles = AsignacionAsignatura.Show("Hora 2 de Miercoles");

            if (SHora2Miercoles != null)
            {
                Hora2Miercoles.Content = SHora2Miercoles;
            }
            else
            {

            }
        }

        private void Hora3Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora3Miercoles = "";
            SHora3Miercoles = AsignacionAsignatura.Show("Hora 3 de Miercoles");

            if (SHora3Miercoles != null)
            {
                Hora3Miercoles.Content = SHora3Miercoles;
            }
            else
            {

            }
        }

        private void Hora4Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora4Miercoles = "";
            SHora4Miercoles = AsignacionAsignatura.Show("Hora 4 de Miercoles");

            if (SHora4Miercoles != null)
            {
                Hora4Miercoles.Content = SHora4Miercoles;
            }
            else
            {

            }
        }

        private void Hora5Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora5Miercoles = "";
            SHora5Miercoles = AsignacionAsignatura.Show("Hora 5 de Miercoles");

            if (SHora5Miercoles != null)
            {
                Hora5Miercoles.Content = SHora5Miercoles;
            }
            else
            {

            }
        }

        private void Hora6Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora6Miercoles = "";
            SHora6Miercoles = AsignacionAsignatura.Show("Hora 6 de Miercoles");

            if (SHora6Miercoles != null)
            {
                Hora6Miercoles.Content = SHora6Miercoles;
            }
            else
            {

            }
        }

        private void Hora7Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora7Miercoles = "";
            SHora7Miercoles = AsignacionAsignatura.Show("Hora 7 de Miercoles");

            if (SHora7Miercoles != null)
            {
                Hora7Miercoles.Content = SHora7Miercoles;
            }
            else
            {

            }
        }

        private void Hora8Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora8Miercoles = "";
            SHora8Miercoles = AsignacionAsignatura.Show("Hora 8 de Miercoles");

            if (SHora8Miercoles != null)
            {
                Hora8Miercoles.Content = SHora8Miercoles;
            }
            else
            {

            }
        }

        private void Hora9Miercoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora9Miercoles = "";
            SHora9Miercoles = AsignacionAsignatura.Show("Hora 9 de Miercoles");

            if (SHora9Miercoles != null)
            {
                Hora9Miercoles.Content = SHora9Miercoles;
            }
            else
            {

            }
        }

        private void Hora1Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora1Jueves = "";
            SHora1Jueves = AsignacionAsignatura.Show("Hora 1 de Jueves");

            if (SHora1Jueves != null)
            {
                Hora1Jueves.Content = SHora1Jueves;
            }
            else
            {

            }
        }

        private void Hora2Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora2Jueves = "";
            SHora2Jueves = AsignacionAsignatura.Show("Hora 2 de Jueves");

            if (SHora2Jueves != null)
            {
                Hora2Jueves.Content = SHora2Jueves;
            }
            else
            {

            }
        }

        private void Hora3Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora3Jueves = "";
            SHora3Jueves = AsignacionAsignatura.Show("Hora 3 de Jueves");

            if (SHora3Jueves != null)
            {
                Hora3Jueves.Content = SHora3Jueves;
            }
            else
            {

            }
        }

        private void Hora4Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora4Jueves = "";
            SHora4Jueves = AsignacionAsignatura.Show("Hora 4 de Jueves");

            if (SHora4Jueves != null)
            {
                Hora4Jueves.Content = SHora4Jueves;
            }
            else
            {

            }
        }

        private void Hora5Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora5Jueves = "";
            SHora5Jueves = AsignacionAsignatura.Show("Hora 5 de Jueves");

            if (SHora5Jueves != null)
            {
                Hora5Jueves.Content = SHora5Jueves;
            }
            else
            {

            }
        }

        private void Hora6Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora6Jueves = "";
            SHora6Jueves = AsignacionAsignatura.Show("Hora 6 de Jueves");

            if (SHora6Jueves != null)
            {
                Hora6Jueves.Content = SHora6Jueves;
            }
            else
            {

            }
        }

        private void Hora7Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora7Jueves = "";
            SHora7Jueves = AsignacionAsignatura.Show("Hora 7 de Jueves");

            if (SHora7Jueves != null)
            {
                Hora7Jueves.Content = SHora7Jueves;
            }
            else
            {

            }
        }

        private void Hora8Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora8Jueves = "";
            SHora8Jueves = AsignacionAsignatura.Show("Hora 8 de Jueves");

            if (SHora8Jueves != null)
            {
                Hora8Jueves.Content = SHora8Jueves;
            }
            else
            {

            }
        }

        private void Hora9Jueves_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora9Jueves = "";
            SHora9Jueves = AsignacionAsignatura.Show("Hora 9 de Jueves");

            if (SHora9Jueves != null)
            {
                Hora9Jueves.Content = SHora9Jueves;
            }
            else
            {

            }
        }

        private void Hora1Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora1Viernes = "";
            SHora1Viernes = AsignacionAsignatura.Show("Hora 1 de Viernes");

            if (SHora1Viernes != null)
            {
                Hora1Viernes.Content = SHora1Viernes;
            }
            else
            {

            }
        }

        private void Hora2Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora2Viernes = "";
            SHora2Viernes = AsignacionAsignatura.Show("Hora 2 de Viernes");

            if (SHora2Viernes != null)
            {
                Hora2Viernes.Content = SHora2Viernes;
            }
            else
            {

            }
        }

        private void Hora3Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora3Viernes = "";
            SHora3Viernes = AsignacionAsignatura.Show("Hora 3 de Viernes");

            if (SHora3Viernes != null)
            {
                Hora3Viernes.Content = SHora3Viernes;
            }
            else
            {

            }
        }

        private void Hora4Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora4Viernes = "";
            SHora4Viernes = AsignacionAsignatura.Show("Hora 4 de Viernes");

            if (SHora4Viernes != null)
            {
                Hora4Viernes.Content = SHora4Viernes;
            }
            else
            {

            }
        }

        private void Hora5Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora5Viernes = "";
            SHora5Viernes = AsignacionAsignatura.Show("Hora 5 de Viernes");

            if (SHora5Viernes != null)
            {
                Hora5Viernes.Content = SHora5Viernes;
            }
            else
            {

            }
        }

        private void Hora6Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora6Viernes = "";
            SHora6Viernes = AsignacionAsignatura.Show("Hora 6 de Viernes");

            if (SHora6Viernes != null)
            {
                Hora6Viernes.Content = SHora6Viernes;
            }
            else
            {

            }
        }

        private void Hora7Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora7Viernes = "";
            SHora7Viernes = AsignacionAsignatura.Show("Hora 7 de Viernes");

            if (SHora7Viernes != null)
            {
                Hora7Viernes.Content = SHora7Viernes;
            }
            else
            {

            }
        }

        private void Hora8Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora8Viernes = "";
            SHora8Viernes = AsignacionAsignatura.Show("Hora 8 de Viernes");

            if (SHora8Viernes != null)
            {
                Hora8Viernes.Content = SHora8Viernes;
            }
            else
            {

            }
        }

        private void Hora9Viernes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SHora9Viernes = "";
            SHora9Viernes = AsignacionAsignatura.Show("Hora 9 de Viernes");

            if (SHora9Viernes != null)
            {
                Hora9Viernes.Content = SHora9Viernes;
            }
            else
            {

            }
        }

        private void btnPlantillaHorario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (CPlantillaHorario.Visibility != Visibility.Visible)
            {
                CPlantillaHorario.Visibility = Visibility.Visible;
                llenarCMBPlantillas();
            }
            else
            {
                CPlantillaHorario.Visibility = Visibility.Hidden;
            }
            
        }

        private void btnContraer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CPlantillaHorario.Visibility  = Visibility.Hidden;
        }

        private void btnNuevaPlantilla_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string NuevaHora = "";
            NuevaHora = InputMessageBox.Show("Nueva hora");


            string query = "INSERT INTO [dbo].[plantilla_horas] ([hora])" + "VALUES (@nueva_hora)";

            SqlParameter[] parameters = {
        new SqlParameter("@nueva_hora", NuevaHora),};

            try
            {
                // Pasar el array de parámetros a ExecuteNonQuery
                conexion.ExecuteNonQuery(query, parameters);
                MessageBoxPersonalizado("Se añadio la hora a la plantilla. ","Confimación");
                llenarCMBPlantillas();
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar

                MessageBoxPersonalizado("La hora ya existe en la plantilla", "Error");
            }

        }

        private void btnAplicarPlantilla_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string plantillaHora = cmbPlantillaHorario.Text;

                if (string.IsNullOrEmpty(plantillaHora) || plantillaHora.Length < 5)
                {
                    MessageBoxPersonalizado("No se ha seleccionado una hora de plantilla o el formato es incorrecto.", "Alerta");
                    return;
                }

                // Validar y extraer la hora y minutos
                int hora = int.Parse(plantillaHora.Substring(0, 2));
                string minutos = plantillaHora.Substring(3, 2);

                if (hora < 0 || hora > 23 || int.Parse(minutos) < 0 || int.Parse(minutos) > 59)
                {
                    MessageBoxPersonalizado("La hora de plantilla es incorrecta.", "Alerta");
                    return;
                }

                string[] horas = new string[9];

                for (int i = 0; i < 9; i++)
                {
                    // Determinar el formato AM/PM para la hora actual
                    string formato = hora >= 12 ? "PM" : "AM";

                    // Convertir hora al formato de 12 horas para mostrar con AM/PM
                    int horaMostrar = hora % 12;
                    if (horaMostrar == 0) horaMostrar = 12;

                    horas[i] = $"{hora:D2}:{minutos} {formato}";

                    hora++;
                    if (hora >= 24) hora = 0; // Reiniciar la hora a 0 si alcanza 24
                }

                Hora1.Content = horas[0];
                Hora2.Content = horas[1];
                Hora3.Content = horas[2];
                Hora4.Content = horas[3];
                Hora5.Content = horas[4];
                Hora6.Content = horas[5];
                Hora7.Content = horas[6];
                Hora8.Content = horas[7];
                Hora9.Content = horas[8];

                SHora1 = horas[0];
                SHora2 = horas[1];
                SHora3 = horas[2];
                SHora4 = horas[3];
                SHora5 = horas[4];
                SHora6 = horas[5];
                SHora7 = horas[6];
                SHora8 = horas[7];
                SHora9 = horas[8];
            }
            catch (Exception ex)
            {
                MessageBoxPersonalizado($"Ocurrió un error: {ex.Message}", "Alerta");
            }

        }
    }
}
