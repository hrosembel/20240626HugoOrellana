using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.Models
{
    public class Departamento
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NivelDeOrganizacion { get; set; }
        public int NumeroEmpleados { get; set; }
    }
}
