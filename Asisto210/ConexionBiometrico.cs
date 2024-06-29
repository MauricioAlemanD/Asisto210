using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asisto210
{
    class ConexionBiometrico
    {

        public zkemkeeper.CZKEMClass Biometrico = new zkemkeeper.CZKEMClass();

        public bool estado = false;
        public int iMachineNumber = 1;

        public ConexionBiometrico()
        {
            estado = Biometrico.Connect_Net(Global.Global_IP, Convert.ToInt32(Global.Global_TCP));
        }

        public void estadoConexionBiometico()
        {

            if (estado == true)
            {
                iMachineNumber = 1;
            }
        }




        //No tocar
    }
}
