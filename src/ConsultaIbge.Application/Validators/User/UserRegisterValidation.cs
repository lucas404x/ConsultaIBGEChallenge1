using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Validators.Commom;
using FluentValidation;

namespace ConsultaIbge.Application.Validators.User;

public class UserRegisterValidation : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidation()
    {
        RuleFor(x => x.Id)
            .SetValidator(x => new IdValidation());
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("É necessário informar o nome completo.");
        RuleFor(x => x.Email)
            .SetValidator(x => new EmailValidation());
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithMessage("A senha deve conter no mínimo 6 caracteres.");
    }
}
