using System;
using System.Threading.Tasks;
using Application.Services.v1;
using Xunit;

namespace API.Tests.Application.Test
{
    public class ClienteAppServiceUnitTest
    {
        [Trait("Consulta cliente por id", "id informado é igual a zero. Retorna mensagem informando que deve ser maior que zero.")]
        [Fact]
        public async Task ConsultaClientePorId_Id_Informado_Fora_Do_Range_Permitido()
        {
            ClienteAppService cliente = new ClienteAppService();

            var result = await cliente.ConsultaClientePorId(Faker.RandomNumber.Next(4,999));

            Assert.Equal("O id precisa ser um número inteiro válido entre 1 e 3.", result.Erro.Mensagem);
            Assert.Null(result.Nome);
            
        }

        [Theory]
        [InlineData(1, "João")]
        [InlineData(2, "Maria")]
        [InlineData(3, "Ana")]
        [Trait("Consulta cliente por id", "Id informado é valido e o nome foi retornado")]
        public async Task ConsultaClientePorId_Id_Retorna_nome(int id, string nome)
        {
            ClienteAppService cliente = new ClienteAppService();

            var result = await cliente.ConsultaClientePorId(id);

            Assert.Equal(nome, result.Nome);
        }
    }
}
