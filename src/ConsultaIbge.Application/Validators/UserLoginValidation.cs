using ConsultaIbge.Application.Dtos;
using FluentValidation;

namespace ConsultaIbge.Application.Validators;

public class UserLoginValidation : AbstractValidator<UserLoginDto>
{
    public UserLoginValidation()
    {
        RuleFor(x => x.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("É necessário informar um email válido.");
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithMessage("A senha deve conter no mínimo 6 caracteres.");
    }
}
