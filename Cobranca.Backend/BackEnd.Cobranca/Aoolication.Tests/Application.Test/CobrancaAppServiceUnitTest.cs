using System;
using System.Threading.Tasks;
using Application.Services.v1;
using Domain.DTO;
using Xunit;

namespace API.Tests.Application.Test
{
    public class CobrancaAppServiceUnitTest
    {
        [Trait("CalcularValorTotal", "São informados os valores cálculo")]
        [Fact]
        public async Task CalcularValorTotal_Retorna_Total()
        {
            CobrancaAppService cliente = new CobrancaAppService();

            var parametroCalculo = new CalculoValorTotalParcelasRequestDto()
            {
                Parcela = Faker.RandomNumber.Next(1, 999),
                Valor = Faker.RandomNumber.Next(1, 999999)
            };

            var result = await cliente.CalcularValorTotal(parametroCalculo);
            var totalCalculado = (double)decimal.Multiply(parametroCalculo.Parcela, parametroCalculo.Valor);

            Assert.IsType<CalculoResponseDto>(result);
            Assert.Equal(totalCalculado + (totalCalculado * 0.05), result.ValorTotal);
        }

    }
}
