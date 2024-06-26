using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet]
        public async Task<IActionResult> ObtenerPorNITEmpresa(string NIT)
            => Ok(await _departamentoData.ObtenerPorNITEmpresa(NIT));
    }
}
