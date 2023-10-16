using ConsultaIbge.Application.Validators.Locality;
using FluentValidation.Results;

namespace ConsultaIbge.Application.Dtos.Locality;

public class LocalityUpdateDto
{
    public LocalityUpdateDto(string id, string state, string city)
    {
        Id = id;
        State = state;
        City = city;
    }

    public string Id { get; set; }
    public string State { get; set; }
    public string City { get; set; }

    public bool IsValid()
    {
        ValidationResult validationResult = new LocalityUpdateValidation().Validate(this);
        return validationResult.IsValid;
    }
}
