using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaCrecerAPI.BLL.Interfaces;

namespace PruebaCrecerAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoService _service;
        public DepartamentosController(IDepartamentoService service)
        {
            _service = service;
        }

        //[HttpGet]
        //public async Task<IActionResult> ObtenerDepartamentos()
        //    => Ok(await _service.GetAll());

        //[HttpGet]
        //public async Task<IActionResult> ObtenerPorId(int id)
        //    => Ok(await _service.GetById(id));

        //[HttpPost]
        //public async Task<IActionResult> AgregarDepartamento([FromBody] Models.NuevoDepartamento nuevoDepartamento)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Not a valid model");
        //    }

        //    //Verificar si existe

        //    var successful = await _service.Create(nuevoDepartamento);
        //    return Ok(successful);
        //}

        //[HttpGet]
        //public async Task<IActionResult> ObtenerPorNITEmpresa(string NIT)
        //    => Ok(await _departamentoData.ObtenerPorNITEmpresa(NIT));
    }
}
