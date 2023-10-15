using ConsultaIbge.Application.Dtos;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Domain.Entities;
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
        // TODO: Handle errors properly
        if (user is null) return null;
        if (_passwordHasherService.Verify(userDto.Password, user.Password))
        {
            return new(user.Name, user.Email, _tokenService.Generate(user));
        }
        // TODO: Handle errors properly
        return null;
    }

    public async Task<UserResponseDto> Register(UserRegisterDto userDto)
    {
        if (await _userRepository.Exists(userDto.Email))
        {
            // TODO: Handle errors propertly
            return null;
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
