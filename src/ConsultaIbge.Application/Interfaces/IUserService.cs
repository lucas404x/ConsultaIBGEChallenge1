using ConsultaIbge.Application.Dtos;

namespace ConsultaIbge.Application.Interfaces;

public interface IUserService
{
    public Task<string> Login(UserLoginDto userDto);
    public Task<string> Register(UserLoginDto userDto);
}
