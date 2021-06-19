using System;
using System.Threading.Tasks;
using Domain.DTO;

namespace Application.Interfaces.Services
{
    public interface IClienteAppService 
    {
        Task<ClienteResponseDto> ConsultaClientePorId(int request);
    }
}
