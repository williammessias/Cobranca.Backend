using System;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaAppService _service;
        public CobrancaController(ICobrancaAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("simulavalortotal")]
        [SwaggerResponseRemoveDefaults]
        [ProducesResponseType(typeof(CalculoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Calcular([FromBody] CalculoValorTotalParcelasRequestDto request )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity);
                }

                var valorTotal = await _service.CalcularValorTotal(request);

                return StatusCode(StatusCodes.Status200OK, valorTotal);
            } 
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new ErroResponseDto { Mensagem = "Sistema indisponível no momento. Por favor, tente mais tarde." });
            }
        }

    }
}
