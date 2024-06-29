using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisto210
{    
    public class Global
    {
        public static string Global_CVE = "0000";
        public static string Global_ROL = "Docente";

        public static string Global_IP = "0";
        public static string Global_TCP = "0";


        public string Usuario {get;set; } = "0001";
        public string Rol {get;set; } = "Administrador";
        public zkemkeeper.CZKEMClass Biometrico = new zkemkeeper.CZKEMClass();

    }
}
