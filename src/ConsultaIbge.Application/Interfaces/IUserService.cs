using ConsultaIbge.Application.Dtos.User;

namespace ConsultaIbge.Application.Interfaces;

public interface IUserService
{
    public Task<UserResponseDto> Login(UserLoginDto userDto);
    public Task<UserResponseDto> Register(UserRegisterDto userDto);
}
