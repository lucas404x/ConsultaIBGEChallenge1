using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Validators.Commom;
using FluentValidation;

namespace ConsultaIbge.Application.Validators.Locality;

public class LocalityAddValidation : AbstractValidator<LocalityAddDto>
{
    public LocalityAddValidation()
    {
        RuleFor(x => x.Id)
            .SetValidator(x => new IdValidation());

        RuleFor(x => x.State)
            .NotEmpty()
            .WithMessage("É necessário informar o Estado");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("É necessário informar a Cidade");
    }
}
