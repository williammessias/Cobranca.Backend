using System;
using System.Threading.Tasks;
using Api.Controllers;
using Application.Interfaces.Services;
using Application.Validators;
using Domain.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Application.Tests
{

    public class CobrancaControllerUnitTest : ControllerBase
    {
        readonly Mock<ICobrancaAppService> MockCobrancaAppService = new Mock<ICobrancaAppService>();
        private CalcularValorTotalValidator validador;

        [InlineData(0, -5)]
        [InlineData(-5, 0)]
        [InlineData(0, 0)]
        [Theory(DisplayName = "Numero de Parcela e valor de parcela invalida")]
        [Trait("Calcula valor total", "Retorna 422. Parcela e valor invalida")] 
        public async Task Calcula_Total_Com_Valor_E_Parcela_Invalidos(int parcela, decimal valor )
        {
            validador = new CalcularValorTotalValidator();

           var dadosCalculo = new CalculoValorTotalParcelasRequestDto()
            {
                Parcela = parcela,
                Valor = valor
            };

            var result = validador.Validate(dadosCalculo);

            Assert.False(result.IsValid);
            Assert.NotNull(result.Errors);
        }

        [Trait("Calcula valor total", "Retorna 200. Parcela e valor valido")]
        public async Task Calcula_Total_Com_Valor_Valido()
        {
            validador = new CalcularValorTotalValidator();

            var dadosCalculo = new CalculoValorTotalParcelasRequestDto()
            {
                Parcela = Faker.RandomNumber.Next(1, 999),
                Valor = Faker.RandomNumber.Next(1, 999999)
            };

            var result = validador.Validate(dadosCalculo);

            Assert.True(result.IsValid);

            var controller = new CobrancaController(MockCobrancaAppService.Object);

            var calculo = await controller.Calcular(dadosCalculo);

            var actionResult = Assert.IsType<ObjectResult>(calculo);

            Assert.NotNull(actionResult);
            Assert.Equal(200, actionResult.StatusCode);
        }


        [InlineData(1)]
        [Theory(DisplayName = "É encontrada alguma exceção no calculo")]
        [Trait("Exceção ao buscar calcular valor total", "Retorna exceção")]
        public async Task Calcula_Valor_Retorna_Excecao(int id)
        {
            var dadosCalculo = new CalculoValorTotalParcelasRequestDto()
            {
                Parcela = Faker.RandomNumber.Next(1, 999),
                Valor = Faker.RandomNumber.Next(1, 999999)
            };

            MockCobrancaAppService
                .Setup(u => u.CalcularValorTotal(It.IsAny<CalculoValorTotalParcelasRequestDto>()))
                .Throws(new Exception());


            var controller = new CobrancaController(MockCobrancaAppService.Object);

            var result = await controller.Calcular(dadosCalculo);

            var actionResult = Assert.IsType<ObjectResult>(result);

            Assert.NotNull(actionResult);
            Assert.Equal(500, actionResult.StatusCode);
        }

    }
}

