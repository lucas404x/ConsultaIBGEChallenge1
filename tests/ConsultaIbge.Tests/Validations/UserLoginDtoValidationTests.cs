using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Validators.User;
using FluentValidation.TestHelper;

namespace ConsultaIbge.Tests.Validations;

public class UserLoginDtoValidationTests
{
    private readonly UserLoginValidation _validator;

    public UserLoginDtoValidationTests()
    {
        _validator = new UserLoginValidation();
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalidemail")]
    [InlineData("missingatsigndomain.com")]
    public void UserLogin_InvalidEmail_ReturnsFail(string email)
    {
        var dto = new UserLoginDto(email, "123456");
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void UserLogin_ValidEmail_ReturnsSuccess()
    {
        var dto = new UserLoginDto("email@domain.com", "123456");
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1ds")]
    [InlineData("#####")]
    [InlineData("Str0N")]
    public void UserLogin_InvalidPassword_ReturnsFail(string password)
    {
        var dto = new UserLoginDto("email@domain.com", password);
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Theory]
    [InlineData("123456")]
    [InlineData("abcdef")]
    [InlineData("#######")]
    [InlineData("Str0Ng")]
    public void UserLogin_ValidPassword_ReturnsSuccess(string password)
    {
        var dto = new UserLoginDto("email@domain.com", password);
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}
