using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaCrecerAPI.BLL.Interfaces;
using PruebaCrecerAPI.DAL.Implementations;
using PruebaCrecerAPI.DAL.Interfaces;
using PruebaCrecerAPI.Models;
using PruebaCrecerAPI.WebAPI.Dto;

namespace PruebaCrecerAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;
        public EmpresaController(IEmpresaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerEmpresas()
        {
            try
            {
                var empresas = await _service.GetAll();
                List<EmpresaDto> empresasDto = new();
                foreach (var empresa in empresas)
                {
                    empresasDto.Add(new EmpresaDto
                    {
                        Id = empresa.Id,
                        NIT = empresa.NIT,
                        Nombre= empresa.Nombre,
                        Bitacora= empresa.Bitacora,
                        RazonSocial= empresa.RazonSocial
                    });
                }
                return Ok(empresasDto);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno en el servidor");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                NuevaEmpresa? empresa = await _service.GetById(id);
                if (empresa == null)
                {
                    return NotFound();
                }
                //Cambiar por mapper
                return Ok(new EmpresaDto
                {
                    Id = empresa.Id,
                    NIT = empresa.NIT,
                    Nombre = empresa.Nombre,
                    Bitacora= empresa.Bitacora,
                    RazonSocial= empresa.RazonSocial
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno en el servidor");
            }
        }

        [HttpGet]
        [Route("GetByNIT")]
        public async Task<IActionResult> ObtenerEmpresaPorNIT(string NIT)
        {
            try
            {
                NuevaEmpresa? empresa = await _service.GetByNIT(NIT);
                if (empresa == null)
                {
                    return NotFound();
                }
                //Cambiar por mapper
                return Ok(new EmpresaDto
                {
                    Id = empresa.Id,
                    NIT = empresa.NIT,
                    Nombre = empresa.Nombre,
                    Bitacora = empresa.Bitacora,
                    RazonSocial = empresa.RazonSocial
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno en el servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEmpresa([FromBody] EmpresaDto dto)
        {
            try
            {
                var empresa = await _service.GetByNIT(dto.NIT);

                if (empresa != default)
                {
                    return BadRequest("La empresa ya existe");
                }

                int id = await _service.Create(
                    new NuevaEmpresa
                    {
                        NIT = dto.NIT,
                        Nombre = dto.Nombre,
                        RazonSocial = dto.RazonSocial,
                        Bitacora = dto.Bitacora
                    });
                return CreatedAtAction("GET", new { id }, dto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno en el servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEmpresa([FromBody] EmpresaDto dto)
        {
            try
            {
                //Debe cambiarse por un mapper
                NuevaEmpresa empresa = new()
                {
                    NIT = dto.NIT,
                    Nombre = dto.Nombre,
                    RazonSocial = dto.RazonSocial,
                    Bitacora = dto.Bitacora
                };

                var updated = await _service.Update(empresa);
                if (updated)
                {
                    return Ok("Empresa actualizada exitosamente");
                }
                else
                {
                    return NotFound("No se pudieron actualizar los datos de la empresa");
                }
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno en el servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEmpresa(int id)
        {
            try
            {
                var deleted = await _service.Delete(id);
                if (deleted)
                {
                    return Ok("Empresa eliminada exitosamente");
                }
                else
                {
                    return NotFound("No se pudo eliminar la empresa");
                }
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno en el servidor");
            }
        }
    }
}
