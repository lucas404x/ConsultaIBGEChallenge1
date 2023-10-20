using FluentValidation;

namespace ConsultaIbge.Application.Validators.Commom;

public class IdValidation : AbstractValidator<string>
{
    public IdValidation()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("O campo ID é requerido.")
            .Length(7)
            .WithMessage("O ID deve conter 7 caracteres.");
    }
}
