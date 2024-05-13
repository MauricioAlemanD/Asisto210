using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisto210
{

     class ListaResgitro
    {

        private string id;
        private string nombre;
        private string horaRegistro;
        private string fechaRegistro;
        private string metodoRegistro;

        public string Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string HoraRegistro { get => horaRegistro; set => horaRegistro = value; }
        public string FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public string MetodoRegistro { get => metodoRegistro; set => metodoRegistro = value; }

        public ListaResgitro(string pId,string pNombre,string pHoraRegistro,string pFechaRegistro,string pMetodoRegistro)
        {
            id = pId;
            nombre = pNombre;
            horaRegistro = pHoraRegistro;
            fechaRegistro = pFechaRegistro;
            metodoRegistro = pMetodoRegistro;
        }
    }
}
