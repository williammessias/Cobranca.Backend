using System;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteAppService _service;
        public ClientesController(IClienteAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id usado para localizar um cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponseRemoveDefaults]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscaCliente(int id)
        {
            try
            {
                var cliente = await _service.ConsultaClientePorId(id);
              
                if(string.IsNullOrEmpty(cliente.Nome))
                    return StatusCode(StatusCodes.Status204NoContent);

                return StatusCode(StatusCodes.Status200OK, cliente);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ErroResponseDto { Mensagem = "Sistema indisponível no momento. Por favor, tente mais tarde." });
            }
        }

    }
}
