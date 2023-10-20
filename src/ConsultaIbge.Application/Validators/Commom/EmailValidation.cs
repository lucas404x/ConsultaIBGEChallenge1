using FluentValidation;

namespace ConsultaIbge.Application.Validators.Commom;

public class EmailValidation : AbstractValidator<string>
{
    public EmailValidation()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("O campo e-mail é requerido.")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("o e-mail informado é inválido.");
    }
}
