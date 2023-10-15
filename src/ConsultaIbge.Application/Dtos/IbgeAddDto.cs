using ConsultaIbge.Application.Validators;
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
