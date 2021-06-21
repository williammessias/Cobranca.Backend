using System;
using Domain.DTO;
using FluentValidation;

namespace Application.Validators
{

    public class CalcularValorTotalValidator : AbstractValidator<CalculoValorTotalParcelasRequestDto>
    {
        public CalcularValorTotalValidator()
        {
            RuleFor(model => model.Parcela)
                .NotNull().WithMessage("O número da parcela precisa ser informado")
                .NotEmpty().WithMessage("O número da parcela precisa ser informado")
                .InclusiveBetween(1, 3).WithMessage("O número da parcela precisa estar entre 1 e 3");


            RuleFor(model => model.Valor)
             .NotNull().WithMessage("O valor da parcela precisa ser informado")
             .GreaterThanOrEqualTo(0).WithMessage("O valor da parcela precisa ser maior ou igual a 0");
        }

    }
} 