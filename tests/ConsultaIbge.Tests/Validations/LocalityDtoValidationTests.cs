using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Validators.Locality;
using FluentValidation.TestHelper;

namespace ConsultaIbge.Tests.Validations;

public class LocalityDtoValidationTests
{
    private readonly LocalityAddValidation _localityAddValidation;
    private readonly LocalityUpdateValidation _localityUpdateValidation;

    public LocalityDtoValidationTests()
    {
        _localityAddValidation = new LocalityAddValidation();
        _localityUpdateValidation = new LocalityUpdateValidation();
    }

    [Theory]
    [InlineData("", "DC State", "Gotham City")]
    [InlineData("123456", "DC State", "Gotham City")]
    public void Locality_InvalidId_ReturnsFail(string id, string state, string city)
    {
        // Assert
        var dto = new LocalityAddDto(id, state, city);

        // Act
        var result = _localityAddValidation.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Locality_InvalidState_ReturnsFail()
    {
        // Assert
        var dto = new LocalityAddDto("123456", "", "Gotham City");

        // Act
        var result = _localityAddValidation.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.State);
    }

    [Fact]
    public void Locality_InvalidCity_ReturnsFail()
    {
        // Assert
        var dto = new LocalityAddDto("1234567", "DC State", "");

        // Act
        var result = _localityAddValidation.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City);
    }


    [Fact]
    public void LocalityUpdate_InvalidState_ReturnsFail()
    {
        // Assert
        var dto = new LocalityUpdateDto("123456", "", "Gotham City");

        // Act
        var result = _localityUpdateValidation.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.State);
    }

    [Fact]
    public void LocalityUpdate_InvalidCity_ReturnsFail()
    {
        // Assert
        var dto = new LocalityUpdateDto("1234567", "DC State", "");

        // Act
        var result = _localityUpdateValidation.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City);
    }
}
