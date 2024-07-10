using PruebaCrecerAPI.Models;
using System.Security.Cryptography.Xml;

namespace PruebaCrecerAPI.WebAPI.Dto
{
    public class EmpresaDepartamentoDto
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public int NumeroEmpleados { get; set; }
    }
}
