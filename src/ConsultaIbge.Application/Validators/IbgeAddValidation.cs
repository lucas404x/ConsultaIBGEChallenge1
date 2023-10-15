using ConsultaIbge.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaIbge.Application.Validators;

public class IbgeAddValidation : AbstractValidator<IbgeAddDto>
{
    public IbgeAddValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("É necessário informar o Id")
            .Length(7, 7)
            .WithMessage("O Id deve ter 7 caracteres");

        RuleFor(x => x.State)
            .NotEmpty()
            .WithMessage("É necessário informar o Estado");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("É necessário informar a Cidade");
    }
}
