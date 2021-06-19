using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class ListaErroResponseDto
    {
        public ListaErroResponseDto(List<string> erros)
        {
            Erros = erros;
        }

        public List<string> Erros { get; set; }

        
    }
}
