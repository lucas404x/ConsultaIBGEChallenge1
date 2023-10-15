using ConsultaIbge.Domain.Entities;

namespace ConsultaIbge.Application.Interfaces;

public interface ITokenService
{
    public string Generate(User user);
}
