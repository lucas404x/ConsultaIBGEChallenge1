using ConsultaIbge.Application.Dtos;

namespace ConsultaIbge.Application.Interfaces;

public interface IUserService
{
    public Task<UserResponseDto> Login(UserLoginDto userDto);
    public Task<UserResponseDto> Register(UserRegisterDto userDto);
}
