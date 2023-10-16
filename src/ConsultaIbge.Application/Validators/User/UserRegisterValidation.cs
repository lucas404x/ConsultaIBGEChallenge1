using ConsultaIbge.Application.Dtos.User;
using FluentValidation;

namespace ConsultaIbge.Application.Validators.User;

public class UserRegisterValidation : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("É necessário informar o nome completo.");
        RuleFor(x => x.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("É necessário informar um email válido.");
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithMessage("A senha deve conter no mínimo 6 caracteres.");
    }
}
