using FluentValidation;
using FluentValidation.Results;

namespace ConsultaIbge.Application.Dtos;

public class IbgeAddDto
{ 
    public string Id { get; set; }
    public string State { get; set; }
    public string City { get; set; }

    public bool IsValid()
    {
        ValidationResult validationResult = new IbgeAddValidation().Validate(this);

        return validationResult.IsValid;
    }
}

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
