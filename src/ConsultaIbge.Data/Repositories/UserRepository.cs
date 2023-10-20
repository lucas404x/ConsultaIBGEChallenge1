using ConsultaIbge.Core.Data;
using ConsultaIbge.Data.Context;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultaIbge.Data.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public Task<User?> GetByEmail(string email) 
        => _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email);

    public Task<bool> Exists(string email) => 
        _context.Users.AsNoTracking().AnyAsync(x => x.Email == email);

    public void Add(User entity) => _context.Add(entity);

    public void Dispose() => _context.Dispose();
}
