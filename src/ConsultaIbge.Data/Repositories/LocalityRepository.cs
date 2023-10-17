using ConsultaIbge.Core.Data;
using ConsultaIbge.Data.Context;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultaIbge.Data.Repositories;

public class LocalityRepository : ILocalityRepository
{
    private readonly ApplicationContext _context;

    public LocalityRepository(ApplicationContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<PagedResult<Locality>> GetAllAsync(int pageSize, int pageIndex, string query = null)
    {
        var localityQuery = _context.Localities.AsQueryable();

        var locality = await localityQuery.AsNoTrackingWithIdentityResolution()
                                  .Where(x => EF.Functions.Like(x.State, $"%{query}%"))
                                  .OrderBy(x => x.State)
                                  .Skip(pageSize * (pageIndex - 1))
                                  .Take(pageSize).ToListAsync();

        var total = await localityQuery.AsNoTrackingWithIdentityResolution()
                                   .Where(x => EF.Functions.Like(x.State, $"%{query}%"))
                                   .CountAsync();

        return new PagedResult<Locality>(locality, total, pageIndex, pageSize, query);

    }
    //public async Task<IEnumerable<Locality>> GetAllAsync() => await _context.Localities.ToListAsync();

    public async Task<Locality> GetByIdAsync(string id) => await _context.Localities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

    public void Add(Locality entity) => _context.Localities.Add(entity);

    public void Update(Locality entity) => _context.Localities.Update(entity);

    public void Delete(Locality entity) => _context.Localities.Remove(entity);

    public void Dispose() => _context.Dispose();
}