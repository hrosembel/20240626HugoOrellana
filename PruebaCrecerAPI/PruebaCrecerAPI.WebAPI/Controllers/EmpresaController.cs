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


        [HttpGet]
        public async Task<IActionResult> ObtenerEmpresaPorNIT(string NIT) 
            => Ok(await _empresaData.ObtenerEmpresaPorNIT(NIT));
    }
}
