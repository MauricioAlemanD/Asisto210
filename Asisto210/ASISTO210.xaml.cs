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
using System.Windows.Shapes;
using System.Xml.Linq;


namespace Asisto210
{
    /// <summary>
    /// Lógica de interacción para ASISTO210.xaml
    /// </summary>
    /// 
  
    public partial class ASISTO210 : Window
    {
        Global gbl = new Global();
        bool estado = false;
        private int iMachineNumber = 1;


        List<ListaResgitro> lsRg = new List<ListaResgitro>();


        public ASISTO210()
        {
            estado = gbl.Biometrico.Connect_Net("192.168.0.45", 4370);
            InitializeComponent();
            lblSaludo.Content = lblSaludo.Content + " " + gbl.Usuario;
            estadoConexion();

           //GridView griw = new GridView();

            //GridViewColumn columnaId = new GridViewColumn();
            //columnaId.Header = "ID Usuario";
            //columnaId.DisplayMemberBinding = new System.Windows.Data.Binding("Id");
            //columnaId.Width = 50;
            //griw.Columns.Add(columnaId);

            //GridViewColumn columnaNombre = new GridViewColumn();
            //columnaNombre.Header = "Nombre";
            //columnaNombre.DisplayMemberBinding = new System.Windows.Data.Binding("Nombre");
            //columnaNombre.Width = 150;
            //griw.Columns.Add(columnaNombre);

            //GridViewColumn columnaHRegistro = new GridViewColumn();
            //columnaHRegistro.Header = "Hora de registro";
            //columnaHRegistro.DisplayMemberBinding = new System.Windows.Data.Binding("HoraRegistro");
            //columnaHRegistro.Width = 70;
            //griw.Columns.Add(columnaHRegistro);

            //GridViewColumn columnaFRegistro = new GridViewColumn();
            //columnaFRegistro.Header = "Fecha de registro";
            //columnaFRegistro.DisplayMemberBinding = new System.Windows.Data.Binding("FechaRegistro");
            //columnaFRegistro.Width = 70;
            //griw.Columns.Add(columnaFRegistro);

            //GridViewColumn columnaMRegistro = new GridViewColumn();
            //columnaMRegistro.Header = "Método de registro";
            //columnaMRegistro.DisplayMemberBinding = new System.Windows.Data.Binding("MetodoRegistro");
            //columnaMRegistro.Width = 50;
            //griw.Columns.Add(columnaMRegistro);

            //lsvEntradas.View = griw;



            //llenadoUtilma();
        }

        private void estadoConexion()
        {

            if (estado == true)
            {
                //lblEstado.Visibility = Visibility.Visible;
                iMachineNumber = 1;
            }
        }

        //private void llenadoUtilma()
        //{
        //    if (estado == true)
        //    {
        //        string sdwEnrollNumber = "";
        //        int idwVerifyMode = 0;
        //        int idwInOutMode = 0;
        //        int idwYear = 0;
        //        int idwMonth = 0;
        //        int idwDay = 0;
        //        int idwHour = 0;
        //        int idwMinute = 0;
        //        int idwSecond = 0;
        //        int idwWorkcode = 0;

        //        string sName = "";
        //        string sPassword = "";
        //        int iPrivilege = 0;
        //        bool bEnabled = true;

        //        int idwErrorCode = 0;
        //        int iGLCount = 0;
        //        int iIndex = 0;

        //        lsvEntradas.Items.Clear();

        //        gbl.Biometrico.EnableDevice(iMachineNumber, false);

        //        if (gbl.Biometrico.ReadGeneralLogData(iMachineNumber))
        //        {
        //            while (gbl.Biometrico.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
        //                   out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
        //            {
        //                iGLCount++;

        //                if (gbl.Biometrico.SSR_GetUserInfo(iMachineNumber, sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
        //                {
        //                    var fechaHora = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
        //                    String Metodo = "";

        //                    if (idwVerifyMode.ToString() == "3")
        //                    {
        //                        Metodo = "Pin";
        //                    } else if (idwVerifyMode.ToString() == "1")
        //                    {
        //                        Metodo = "Huella";
        //                    } else if (idwVerifyMode.ToString() == "15")
        //                    {
        //                        Metodo = "Facial";
        //                    }

        //                    lsRg.Add(new ListaResgitro(sdwEnrollNumber.ToString(), sName, fechaHora.TimeOfDay.ToString(), idwDay.ToString() + "-" + idwMonth.ToString() + "-" + idwYear.ToString(), Metodo));
        //                    lsvEntradas.ItemsSource = lsRg;
        //                }



        //                //lsvEntradas.Items.Add(iGLCount.ToString());
        //                //lsvEntradas.Items.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
        //                //lsvEntradas.Items.Add(idwVerifyMode.ToString());
        //                //lsvEntradas.Items.Add(idwInOutMode.ToString());
        //                //lsvEntradas.Items.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
        //                //lsvEntradas.Items.Add(idwWorkcode.ToString());
        //                iIndex++;

        //                //var FechaHora = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);


        //            }
        //        }
        //    }
        //}

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListViewItem_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {


        }

        private void ListViewItem_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListViewItem_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListViewItem_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
   
        }

        private void ListViewItem_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void lblClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void lblMin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
