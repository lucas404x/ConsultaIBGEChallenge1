using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Exceptions;
using ConsultaIbge.Domain.Interfaces;

namespace ConsultaIbge.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, IPasswordHasherService passwordHasherService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
        _tokenService = tokenService;
    }

    public async Task<UserResponseDto> Login(UserLoginDto userDto)
    {
        var user = await _userRepository.GetByEmail(userDto.Email);
        if (user is null) throw new InvalidProvidedCredentialsException("As credenciais não batem no sistema.");
        if (_passwordHasherService.Verify(userDto.Password, user.Password))
        {
            return new(user.Name, user.Email, _tokenService.Generate(user));
        }
        throw new InvalidProvidedCredentialsException("As credenciais não batem no sistema.");
    }

    public async Task<UserResponseDto> Register(UserRegisterDto userDto)
    {
        if (await _userRepository.Exists(userDto.Email))
        {
            throw new EmailAlreadyExistsException("O email informado já está cadastrado no sistema.");
        }
        var user = new User { 
            Id = userDto.Id,
            Name = userDto.Name,
            Email = userDto.Email, 
            Password = _passwordHasherService.Hash(userDto.Password) 
        };
        _userRepository.Add(user);
        await _userRepository.UnitOfWork.CommitAsync();
        return new(user.Name, user.Email, _tokenService.Generate(user));
    }
}
