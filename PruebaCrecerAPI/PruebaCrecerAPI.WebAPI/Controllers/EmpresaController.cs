using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaCrecerAPI.DAL.Interfaces;

namespace PruebaCrecerAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaData _empresaData;
        public EmpresaController(IEmpresaData empresaData)
        {
            _empresaData = empresaData;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEmpresa([FromBody] Models.NuevaEmpresa nuevaEmpresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            //Verificar si existe

            var successful = _empresaData.AgregarEmpresa(nuevaEmpresa);
            return Ok(successful);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerEmpresaPorNIT(string NIT) 
            => Ok(await _empresaData.ObtenerEmpresaPorNIT(NIT));
    }
}
