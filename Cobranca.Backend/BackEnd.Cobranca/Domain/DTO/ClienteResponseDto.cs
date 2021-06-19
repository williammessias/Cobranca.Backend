using System;
namespace Domain.DTO
{
    public class ClienteResponseDto
    {
        public ErroResponseDto? Erro { get; set; }
        public string Nome { get; set; }
    }
}
