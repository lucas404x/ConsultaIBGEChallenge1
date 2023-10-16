using ConsultaIbge.Application.Validators.Locality;
using FluentValidation.Results;

namespace ConsultaIbge.Application.Dtos.Locality;

public class LocalityAddDto
{
    public string Id { get; set; }
    public string State { get; set; }
    public string City { get; set; }

    public bool IsValid()
    {
        ValidationResult validationResult = new LocalityAddValidation().Validate(this);
        return validationResult.IsValid;
    }
}
