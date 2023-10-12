using ConsultaIbge.Core.Data;
using ConsultaIbge.Data.Context;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultaIbge.Data.Repositories;

public class IbgeRepository : IIbgeRepository
{
    private readonly ApplicationContext _context;

    public IbgeRepository(ApplicationContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<PagedResult<Ibge>> GetAllAsync(int pageSize, int pageIndex, string query = null)
    {
        var ibgeQuery = _context.Ibges.AsQueryable();

        var ibge = await ibgeQuery.AsNoTrackingWithIdentityResolution()
                                  .Where(x => EF.Functions.Like(x.State, $"%{query}%"))
                                  .OrderBy(x => x.State)
                                  .Skip(pageSize * (pageIndex - 1))
                                  .Take(pageSize).ToListAsync();

        var total = await ibgeQuery.AsNoTrackingWithIdentityResolution()
                                   .Where(x => EF.Functions.Like(x.State, $"%{query}%"))
                                   .CountAsync();

        return new PagedResult<Ibge>()
        {
            List = ibge,
            TotalResults = total,
            PageIndex = pageIndex,
            PageSize = pageSize,
            Query = query
        };

    }

    public async Task<Ibge> GetByIdAsync(int id) => await _context.Ibges.FindAsync(id);

    public void Add(Ibge entity) => _context.Ibges.Add(entity);

    public void Update(Ibge entity) => _context.Ibges.Update(entity);

    public void Delete(Ibge entity) => _context.Ibges.Remove(entity);    

    public void Dispose() => _context?.Dispose();
}
