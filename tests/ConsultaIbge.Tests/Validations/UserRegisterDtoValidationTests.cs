using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Validators.User;
using FluentValidation.TestHelper;

namespace ConsultaIbge.Tests.Validations;

public class UserRegisterDtoValidationTests
{
    private readonly UserRegisterValidation _validator;

    public UserRegisterDtoValidationTests()
    {
        _validator = new UserRegisterValidation();
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalidemail")]
    [InlineData("missingatsigndomain.com")]
    public void UserRegister_InvalidEmail_ReturnsFail(string email)
    {
        var dto = new UserRegisterDto("1234567", "name", email, "123456");
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void UserRegister_ValidEmail_ReturnsSuccess()
    {
        var dto = new UserRegisterDto("1234567", "name", "email@domain.com", "123456");
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1ds")]
    [InlineData("#####")]
    [InlineData("Str0N")]
    public void UserRegister_InvalidPassword_ReturnsFail(string password)
    {
        var dto = new UserRegisterDto("1234567", "name", "email@domain.com", password);
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Theory]
    [InlineData("123456")]
    [InlineData("abcdef")]
    [InlineData("#######")]
    [InlineData("Str0Ng")]
    public void UserRegister_ValidPassword_ReturnsSuccess(string password)
    {
        var dto = new UserRegisterDto("1234567", "name", "email@domain.com", password);
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }

    [Theory]
    [InlineData("")]
    [InlineData("211")]
    [InlineData("91901")]
    public void UserRegister_InvalidId_ReturnsFail(string id)
    {
        var dto = new UserRegisterDto(id, "name", "email@domain.com", "123456");
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void UserRegister_ValidId_ReturnsSuccess()
    {
        var dto = new UserRegisterDto("1234567", "name", "email@domain.com", "123456");
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void UserRegister_InvalidName_ReturnsFail()
    {
        var dto = new UserRegisterDto("1234567", "", "email@domain.com", "123456");
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void UserRegister_ValidName_ReturnsSuccess()
    {
        var dto = new UserRegisterDto("1234567", "name", "email@domain.com", "123456");
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}
