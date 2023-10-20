using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Application.Services;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Exceptions;
using ConsultaIbge.Domain.Interfaces;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace ConsultaIbge.Tests.Services;

public class UserServiceTests : IDisposable
{
    private const string EMAIL = "emailxpto@email.com";
    private const string PASSWORD = "passwordexample";
    private const string TOKEN = "mybearertoken";

    private readonly IUserRepository _userRepositoryMock;
    private readonly IPasswordHasherService _passwordHasherMock;
    private readonly ITokenService _tokenServiceMock;

    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _passwordHasherMock = Substitute.For<IPasswordHasherService>();
        _tokenServiceMock = Substitute.For<ITokenService>();

        _userService = new UserService(_userRepositoryMock, _passwordHasherMock, _tokenServiceMock);
    }

    public void Dispose()
    {
        _userRepositoryMock.Dispose();
    }

    [Fact]
    public async void Register_ExistingEmail_ReturnsFail()
    {
        _userRepositoryMock.Exists(Arg.Any<string>()).Returns(true);
        var dto = new UserRegisterDto("1234567", "NAME", EMAIL, PASSWORD);
        await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => _userService.Register(dto));
    }

    [Fact]
    public async void Register_ValidCredentials_ReturnsUser()
    {
        _userRepositoryMock.Exists(Arg.Any<string>()).Returns(false);
        _tokenServiceMock.Generate(Arg.Any<User>()).Returns(TOKEN);

        var dto = new UserRegisterDto("1234567", "NAME", EMAIL, PASSWORD);
        
        var result = await _userService.Register(dto);

        Assert.Multiple(() =>
        {
            Assert.Equal(EMAIL, result.Email);
            Assert.Equal(TOKEN, result.Token);
            _userRepositoryMock.Received(1).Add(Arg.Any<User>());
            _userRepositoryMock.Received(1).UnitOfWork.CommitAsync();
        });
    }

    [Fact]
    public async void Login_EmailNotFound_ReturnsFail()
    {
        var dto = new UserLoginDto(EMAIL, PASSWORD);
        _userRepositoryMock.GetByEmail(dto.Email).ReturnsNull();
        
        await Assert.ThrowsAsync<InvalidProvidedCredentialsException>(() => _userService.Login(dto));
        await _userRepositoryMock.Received(1).GetByEmail(EMAIL);
    }

    [Fact]
    public async void Login_PasswordsDoesNotMatch_ReturnsFail()
    {
        var dto = new UserLoginDto(EMAIL, PASSWORD);
        var userFake = new User { Email = EMAIL, Password = $"{PASSWORD}_1" };

        _userRepositoryMock.GetByEmail(dto.Email).Returns(userFake);
        _passwordHasherMock.Verify(dto.Password, userFake.Password).Returns(false);

        await Assert.ThrowsAsync<InvalidProvidedCredentialsException>(() => _userService.Login(dto));
        await _userRepositoryMock.Received(1).GetByEmail(EMAIL);
        _passwordHasherMock.Received(1).Verify(dto.Password, userFake.Password);
        _tokenServiceMock.DidNotReceive().Generate(Arg.Any<User>());
    }

    [Fact]
    public async void Login_CredentialsExists_ReturnsSuccess()
    {
        var dto = new UserLoginDto(EMAIL, PASSWORD);
        var userFake = new User { Email = EMAIL, Password = PASSWORD };

        _userRepositoryMock.GetByEmail(dto.Email).Returns(userFake);
        _passwordHasherMock.Verify(dto.Password, userFake.Password).Returns(true);
        _tokenServiceMock.Generate(userFake).Returns(TOKEN);

        var result = await _userService.Login(dto);

        Assert.Multiple(() =>
        {
            Assert.Equal(EMAIL, result.Email);
            Assert.Equal(TOKEN, result.Token);
            _userRepositoryMock.Received(1).GetByEmail(EMAIL);
            _passwordHasherMock.Received(1).Verify(dto.Password, userFake.Password);
        });
    }
}
