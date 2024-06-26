using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaCrecerAPI.DAL.Implementations;
using PruebaCrecerAPI.DAL.Interfaces;

namespace PruebaCrecerAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoData _departamentoData;
        public DepartamentosController(IDepartamentoData departamentoData)
        {
            _departamentoData = departamentoData;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDepartamento([FromBody] Models.NuevoDepartamento nuevoDepartamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            //Verificar si existe

            var successful = _departamentoData.AgregarDepartamento(nuevoDepartamento);
            return Ok(successful);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorNITEmpresa(string NIT)
            => Ok(await _departamentoData.ObtenerPorNITEmpresa(NIT));
    }
}
