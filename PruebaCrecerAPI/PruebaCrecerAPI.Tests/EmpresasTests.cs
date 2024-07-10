using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using PruebaCrecerAPI.BLL.Implementations;
using PruebaCrecerAPI.BLL.Interfaces;
using PruebaCrecerAPI.DAL.Interfaces;
using PruebaCrecerAPI.Models;
using PruebaCrecerAPI.WebAPI.Controllers;
using PruebaCrecerAPI.WebAPI.Dto;
using System.Net;

namespace PruebaCrecerAPI.Tests
{
    public class EmpresasTests
    {
        private Mock<IGenericRepository<NuevaEmpresa>> _repository;
        private Mock<IEmpresaRepository> _empresaRepository;

        private IEmpresaService _empresaService;
        private List<NuevaEmpresa> empresasPrueba = new();
        public EmpresasTests()
        {
            _repository = new();
            _empresaRepository = new();
            _empresaService = new EmpresaService(_repository.Object, _empresaRepository.Object);
        }

        #region GetAll
        //Caso positivo
        [Fact]
        public async void ObtenerEmpresas()
        {
            // Arrange
            LlenarEmpresasDePrueba();
            _repository
                .Setup(x => x.GetAll())
                .ReturnsAsync(empresasPrueba);
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.ObtenerEmpresas();

            //Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<EmpresaDto> empresas = Assert.IsType<List<EmpresaDto>>(okResult.Value);
            Assert.NotEmpty(empresas);
            Assert.Equal(empresasPrueba.Count(), empresas.Count());
            foreach (var empresa in empresas)
            {
                //usando mapper se puede hacer una comparacion mas completa
                Assert.Contains<int>(empresa.Id, empresasPrueba.Select(x => x.Id));
            }
        }

        //Caso negativo
        [Fact]
        public async void ObtenerEmpresas_InternalServerError()
        {
            // Arrange
            EmpresaController empresaController = new EmpresaController(_empresaService);
            _repository
                .Setup(x => x.GetAll()).Throws(new Exception("Error interno"));

            //Act
            var result = await empresaController.ObtenerEmpresas();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.NotNull(statusCodeResult.Value);
            Assert.Equal("Error interno en el servidor", statusCodeResult.Value.ToString());
        }
        #endregion

        #region GetById
        //Caso positivo
        [Fact]
        public async void ObtenerEmpresaPorId_OK()
        {
            // Arrange
            int idEmpresa = 1;
            LlenarEmpresasDePrueba();
            _repository
                .Setup(x => x.GetById(idEmpresa))
                .ReturnsAsync(empresasPrueba.FirstOrDefault(x => x.Id == idEmpresa));
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.ObtenerPorId(idEmpresa);

            //Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            EmpresaDto empresa = Assert.IsType<EmpresaDto>(okResult.Value);
            Assert.Equal(idEmpresa, empresa?.Id);
        }

        //Caso negativo
        [Fact]
        public async void ObtenerEmpresaPorId_ReturnsNotFound()
        {
            // Arrange
            int idEmpresa = 1000000;
            LlenarEmpresasDePrueba();
            _repository
                .Setup(x => x.GetById(idEmpresa))
                .ReturnsAsync(empresasPrueba.FirstOrDefault(x => x.Id == idEmpresa));
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.ObtenerPorId(idEmpresa);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //Caso negativo
        [Fact]
        public async void ObtenerEmpresaPorId_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int idEmpresa = -1;
            LlenarEmpresasDePrueba();
            _repository
                .Setup(x => x.GetById(idEmpresa))
                .ReturnsAsync(empresasPrueba.FirstOrDefault(x => x.Id == idEmpresa));
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.ObtenerPorId(idEmpresa);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID no válido", badRequestResult.Value);
        }
        #endregion

        #region GetNIT
        //Caso positivo
        [Fact]
        public async void ObtenerEmpresaPorNIT_OK()
        {
            // Arrange
            string nitEmpresa = "00000000000001";
            LlenarEmpresasDePrueba();
            _empresaRepository
                .Setup(x => x.ObtenerEmpresaPorNIT(nitEmpresa))
                .ReturnsAsync(empresasPrueba.FirstOrDefault(x => x.NIT == nitEmpresa));
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.ObtenerEmpresaPorNIT(nitEmpresa);

            //Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            EmpresaDto empresa = Assert.IsType<EmpresaDto>(okResult.Value);
            Assert.Equal(nitEmpresa, empresa?.NIT);
        }

        //Caso negativo
        [Fact]
        public async void ObtenerEmpresaPorNIT_ReturnsNotFound()
        {
            // Arrange
            string nitEmpresa = "00000000000000";
            _empresaRepository
                .Setup(x => x.ObtenerEmpresaPorNIT(nitEmpresa))
                .ReturnsAsync(empresasPrueba.FirstOrDefault(x => x.NIT == nitEmpresa));
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.ObtenerEmpresaPorNIT(nitEmpresa);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Create
        //Caso positivo
        [Fact]
        public async void AgregarEmpresa_OK()
        {
            // Arrange
            EmpresaDto nuevaEmpresa = new() { NIT="00000000000001"};
            _repository
                .Setup(x => x.Create(It.IsAny<NuevaEmpresa>()))
                .ReturnsAsync(1);
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.AgregarEmpresa(nuevaEmpresa);

            //Assert
            CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            EmpresaDto resultValue = Assert.IsType<EmpresaDto>(createdAtActionResult.Value);
            RouteValueDictionary routeValues = Assert.IsType<RouteValueDictionary>(createdAtActionResult.RouteValues);
            int id = Assert.IsType<int>(routeValues[key: "id"]);
            string actionName = Assert.IsType<string>(createdAtActionResult.ActionName);

            Assert.Equal(nuevaEmpresa, resultValue);
            Assert.True(id > 0);
            Assert.Equal("GET", actionName);
        }

        //Caso negativo
        [Fact]
        public async void AgregarEmpresa_EntityAlreadyExists()
        {
            // Arrange
            NuevaEmpresa nuevaEmpresa = new() {Id = 1, NIT = "00000000000001" };
            _empresaRepository
                .Setup(x => x.ObtenerEmpresaPorNIT(nuevaEmpresa.NIT))
                .ReturnsAsync(nuevaEmpresa);
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.AgregarEmpresa(new EmpresaDto() { NIT = "00000000000001" });

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("La empresa ya existe", badRequestResult.Value);
        }
        #endregion

        #region Update
        //Caso positivo
        [Fact]
        public async void ActualizarEmpresa_OK()
        {
            // Arrange
            EmpresaDto empresa = new() { Id=1, NIT = "00000000000001" };
            _repository
                .Setup(x => x.Update(It.IsAny<NuevaEmpresa>()))
                .ReturnsAsync(true);
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.ActualizarEmpresa(empresa);

            //Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal("Empresa actualizada exitosamente", okResult.Value);
        }

        //Caso negativo
        [Fact]
        public async void ActualizarEmpresa_Failed()
        {
            // Arrange
            EmpresaDto empresa = new() { Id = 1, NIT = "00000000000001" };
            _repository
                .Setup(x => x.Update(It.IsAny<NuevaEmpresa>()))
                .ReturnsAsync(false);
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.ActualizarEmpresa(empresa);

            //Assert
            NotFoundObjectResult notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundObjectResult.Value);
            Assert.Equal("No se pudieron actualizar los datos de la empresa", notFoundObjectResult.Value);
        }
        #endregion

        #region Delete
        //Caso positivo
        [Fact]
        public async void EliminarEmpresa_OK()
        {
            // Arrange
            int idEmpresa = 1;
            _repository
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.EliminarEmpresa(idEmpresa);

            //Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal("Empresa eliminada exitosamente", okResult.Value);
        }

        //Caso negativo
        [Fact]
        public async void EliminarEmpresa_Failed()
        {
            // Arrange
            int idEmpresa = 1;
            _repository
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(false);
            EmpresaController empresaController = new EmpresaController(_empresaService);

            //Act
            var result = await empresaController.EliminarEmpresa(idEmpresa);

            //Assert
            NotFoundObjectResult notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundObjectResult.Value);
            Assert.Equal("No se pudo eliminar la empresa", notFoundObjectResult.Value);
        }
        #endregion

        private void LlenarEmpresasDePrueba()
        {
            empresasPrueba.AddRange(new List<NuevaEmpresa>{ 
                new NuevaEmpresa { Id = 1, NIT="00000000000001", Nombre = "TEST COMPANY 1", Bitacora = "test", RazonSocial = "fundacion", FechaRegistro = "2024-06-26" },
                new NuevaEmpresa { Id = 2, NIT="00000000000002", Nombre = "TEST COMPANY 2", Bitacora = "test", RazonSocial = "fundacion", FechaRegistro = "2024-06-26" },
                new NuevaEmpresa { Id = 3, NIT="00000000000003", Nombre = "TEST COMPANY 3", Bitacora = "test", RazonSocial = "fundacion", FechaRegistro = "2024-06-26" }
            });
        }
    }
}