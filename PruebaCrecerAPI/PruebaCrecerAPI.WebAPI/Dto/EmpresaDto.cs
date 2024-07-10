using System.ComponentModel.DataAnnotations;

namespace PruebaCrecerAPI.WebAPI.Dto
{
    public class EmpresaDto
    {
        public int Id { get; set; }

        [Required]
        public string? NIT { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string Bitacora { get; set; }
    }
}
