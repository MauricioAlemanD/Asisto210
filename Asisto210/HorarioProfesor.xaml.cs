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

namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para HorarioProfesor.xaml
    /// </summary>
    public partial class HorarioProfesor : Page
    {

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
        }

        private void btnAñadirAsignatura_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            revisionHorario();
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

                                    //Clase Hora 1 Lunes
                                    añadirHorarioProfesorClases(cve_horario_profesor, ciclo_escolar_ed, cve_docente, turno,"Lunes",TimeSpan.ParseExact(SHora1.Substring(0, 5), @"hh\:mm",null),SHora1,SHora1Lunes.Substring(0,4));

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
            }
            catch (Exception ex)
            {
                // Manejo de excepción, mostrar el error o registrar

                MessageBoxPersonalizado("Error al crear el horario en HORARIOS PROFESOR. " + ex , "Error");
            }


        }

        private void añadirHorarioProfesorClases(string cve_horario_profesor, string ciclo_escolar, string cve_profesor, string turno,string dia_clase, TimeSpan hora_clase,string hora_clase_formateada, string cve_clase)
        {
            string query = "INSERT INTO [dbo].[horario_profesor_clases_v2] ([cve_horario_profesor],[ciclo_escolar],[cve_profesor],[turno],[dia_clase],[hora_clase],[hora_clase_formateada],[cve_clase])" +
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
                MessageBoxPersonalizado("Se añadieron las clases al horario." + cve_horario_profesor + ".", "Confimación");
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
            MensajePersonalizado messageBox = new MensajePersonalizado(mensajealerta, titulo);
            bool? result = messageBox.ShowDialog();

            if (result == true)
            {

            }
            else
            {

            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SHora1 = "2";
            SHora2 = "2";
            SHora3 = "2";
            SHora4 = "2";
            SHora5 = "2";
            SHora6 = "2";
            SHora7 = "2";
            SHora8 = "2";
            SHora9 = "2";

            SHora1Lunes = "2";
            SHora2Lunes = "2";
            SHora3Lunes = "2";
            SHora4Lunes = "2";
            SHora5Lunes = "2";
            SHora6Lunes = "2";
            SHora7Lunes = "2";
            SHora8Lunes = "2";
            SHora9Lunes = "2";

            SHora1Martes = "2";
            SHora2Martes = "2";
            SHora3Martes = "2";
            SHora4Martes = "2";
            SHora5Martes = "2";
            SHora6Martes = "2";
            SHora7Martes = "2";
            SHora8Martes = "2";
            SHora9Martes = "2";

            SHora1Miercoles = "2";
            SHora2Miercoles = "2";
            SHora3Miercoles = "2";
            SHora4Miercoles = "2";
            SHora5Miercoles = "2";
            SHora6Miercoles = "2";
            SHora7Miercoles = "2";
            SHora8Miercoles = "2";
            SHora9Miercoles = "2";

            SHora1Jueves = "2";
            SHora2Jueves = "2";
            SHora3Jueves = "2";
            SHora4Jueves = "2";
            SHora5Jueves = "2";
            SHora6Jueves = "2";
            SHora7Jueves = "2";
            SHora8Jueves = "2";
            SHora9Jueves = "2";

            SHora1Viernes = "2";
            SHora2Viernes = "2";
            SHora3Viernes = "2";
            SHora4Viernes = "2";
            SHora5Viernes = "2";
            SHora6Viernes = "2";
            SHora7Viernes = "2";
            SHora8Viernes = "2";
            SHora9Viernes = "2";

        }
    }
}
