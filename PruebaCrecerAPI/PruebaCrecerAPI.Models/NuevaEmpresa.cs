using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCrecerAPI.Models
{
    [Table("Empresa")]
    public class NuevaEmpresa:BaseEntity
    {

        public string NIT { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string Bitacora { get; set; }
        public string FechaRegistro { get; set; }
    }
}
