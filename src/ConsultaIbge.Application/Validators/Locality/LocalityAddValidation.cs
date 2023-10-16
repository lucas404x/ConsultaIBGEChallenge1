using ConsultaIbge.Application.Dtos.Locality;
using FluentValidation;

namespace ConsultaIbge.Application.Validators.Locality;

public class LocalityAddValidation : AbstractValidator<LocalityAddDto>
{
    public LocalityAddValidation()
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
