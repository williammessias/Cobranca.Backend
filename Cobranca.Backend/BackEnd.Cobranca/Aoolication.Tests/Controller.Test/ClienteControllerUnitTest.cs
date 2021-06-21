using System;
using System.Threading.Tasks;
using Api.Controllers;
using Application.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class ClienteAppServiceTest : ControllerBase
    {
        readonly Mock<IClienteAppService> MockClienteAppService = new Mock<IClienteAppService>();

        [Trait("BuscaClientePorId", "Retorna 204. Id não existente")]
        [Fact(DisplayName = "Retorna status code 204")]
        public async Task BuscaCliente_Com_Id_Inexistente()
        {
            var cliente = new ClienteResponseDto()
            {
                Nome = ""
            }; ;

            MockClienteAppService.Setup(x => x.ConsultaClientePorId(It.IsAny<int>()))
                .ReturnsAsync(cliente);

            var controller = new ClientesController(MockClienteAppService.Object);

            var result = await controller.BuscaCliente(0);

            var actionResult = Assert.IsType<StatusCodeResult>(result);

            Assert.NotNull(actionResult);
            Assert.Equal(204, actionResult.StatusCode);
        }

        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [Theory(DisplayName = "Nome do Cliente encontrado")]
        [Trait("BuscaClientePorIdExistente", "Retorna 200. Id existente")]
        public async Task BuscaCliente_Com_Id_Existente(int id)
        {
            var cliente = new ClienteResponseDto()
            {
                Nome = Faker.Name.FullName()
            }; ;

            MockClienteAppService.Setup(x => x.ConsultaClientePorId(It.IsAny<int>()))
                .ReturnsAsync(cliente);

            var controller = new ClientesController(MockClienteAppService.Object);

            var result = await controller.BuscaCliente(id);

            var actionResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ObjectResult>(actionResult);

            Assert.NotNull(actionResult);
            Assert.NotNull(model.Value);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [InlineData(1)] 
        [Theory(DisplayName = "É encontrada alguma exceção")]
        [Trait("Exceção ao buscar cliente por id", "Retorna exceção")]
        public async Task BuscaCliente_Por_Id_Retorna_Excecao(int id)
        {

            MockClienteAppService
                .Setup(u => u.ConsultaClientePorId(It.IsAny<int>()))
                .Throws(new Exception());


            var controller = new ClientesController(MockClienteAppService.Object);

            var result =  await controller.BuscaCliente(id);

            var actionResult = Assert.IsType<ObjectResult>(result);

            Assert.NotNull(actionResult); 
            Assert.Equal(500, actionResult.StatusCode);
        }

    }
}
