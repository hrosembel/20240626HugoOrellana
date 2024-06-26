using Microsoft.AspNetCore.Mvc;
using Moq;
using PruebaCrecerAPI.DAL.Interfaces;
using PruebaCrecerAPI.Models;
using PruebaCrecerAPI.WebAPI.Controllers;

namespace PruebaCrecerAPI.Tests
{
    public class EmpresasTests
    {

        [Fact]
        public async void ObtenerEmpresaPorId_OK()
        {
            var empresaData = new Mock<IEmpresaData>();
            empresaData
                .Setup(x => x.ObtenerEmpresaPorNIT(It.IsAny<string>()))
                .ReturnsAsync(ObtenerEmpresaDePrueba().First());
            EmpresaController empresaController = new EmpresaController(empresaData.Object);
            var result = await empresaController.ObtenerEmpresaPorNIT(It.IsAny<string>());

            Assert.IsType<OkObjectResult>(result);
        }

        private List<Empresa> ObtenerEmpresaDePrueba()
        {
            return new List<Empresa> {
                new Empresa { Nombre = "TEST COMPANY 1", Bitacora = "test", RazonSocial = "fundacion", FechaRegistro = "2024-06-26"},
                new Empresa { Nombre = "TEST COMPANY 2", Bitacora = "test", RazonSocial = "fundacion", FechaRegistro = "2024-06-26"},
                new Empresa { Nombre = "TEST COMPANY 3", Bitacora = "test", RazonSocial = "fundacion", FechaRegistro = "2024-06-26"},
            };
        }
    }
}