using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Validators.Commom;
using FluentValidation;

namespace ConsultaIbge.Application.Validators.User;

public class UserLoginValidation : AbstractValidator<UserLoginDto>
{
    public UserLoginValidation()
    {
        RuleFor(x => x.Email)
            .SetValidator(x => new EmailValidation());

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithMessage("A senha deve conter no mínimo 6 caracteres.");
    }
}
