using System.Threading.Tasks;
using Application.Interfaces.Services;
using Domain.DTO;

namespace Application.Services.v1
{
    public class CobrancaAppService : ICobrancaAppService
    {

        public Task<CalculoResponseDto> CalcularValorTotal(CalculoValorTotalParcelasRequestDto request)
        {
            var totalParcela = (double)decimal.Multiply(request.Parcela, request.Valor);
            var totalParcelaComPercentual = (totalParcela) + (totalParcela * 0.05);

            return Task.FromResult(new CalculoResponseDto
            {
                ValorTotal = totalParcelaComPercentual
            });
           
        }
    }
     
}