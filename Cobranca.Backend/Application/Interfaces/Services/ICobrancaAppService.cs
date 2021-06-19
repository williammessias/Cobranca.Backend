using System;
using System.Threading.Tasks;
using Domain.DTO;

namespace Application.Interfaces.Services
{
    public interface ICobrancaAppService 
    {
        Task<CalculoResponseDto> CalcularValorTotal(CalculoValorTotalParcelasRequestDto request);
    }
}
