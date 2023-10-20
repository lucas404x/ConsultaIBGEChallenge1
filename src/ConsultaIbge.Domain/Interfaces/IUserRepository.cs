using ConsultaIbge.Core.Data;
using ConsultaIbge.Domain.Entities;

namespace ConsultaIbge.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task<bool> Exists(string email);
    void Add(User entity);
}
