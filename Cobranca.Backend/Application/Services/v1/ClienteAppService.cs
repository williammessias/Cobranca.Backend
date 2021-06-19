using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Domain.DTO;

namespace Application.Services.v1
{
    public class ClienteAppService : IClienteAppService
    {

        public Task<ClienteResponseDto> ConsultaClientePorId(int id)
        {
            var cliente = new ClienteResponseDto() { Erro = new ErroResponseDto() { Mensagem =  "" } };

            if (id <= 0)
            {
                cliente.Erro.Mensagem = "O id precisa ser um número inteiro válido maior que zero.";
                return Task.FromResult(cliente);
            }
            var lista = new List<KeyValuePair<int, string>>();

            lista.Add(new KeyValuePair<int, string>(1, "João"));
            lista.Add(new KeyValuePair<int, string>(2, "Maria"));
            lista.Add(new KeyValuePair<int, string>(3, "Ana"));

            cliente.Nome = lista.Where(c => c.Key == id).FirstOrDefault().Value;

         
            return Task.FromResult(cliente);
        }
    }
     
}