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

    public async Task<PagedResult<Locality>> GetAsync(int pageSize, int pageIndex, string query = null, string flag = "default")
    {
        var localityQuery = DefineQueryByFlag(query, flag);

        var locality = await localityQuery.AsNoTrackingWithIdentityResolution()
                                  .OrderBy(x => x.State)
                                  .Skip(pageSize * (pageIndex - 1))
                                  .Take(pageSize).ToListAsync();

        var total = await localityQuery.AsNoTrackingWithIdentityResolution().CountAsync();

        return new PagedResult<Locality>(locality, total, pageIndex, pageSize, query);

    }

    public async Task<Locality> GetByIdAsync(string id) => await _context.Localities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

    public void Add(Locality entity) => _context.Localities.Add(entity);

    public void Update(Locality entity) => _context.Localities.Update(entity);

    public void Delete(Locality entity) => _context.Localities.Remove(entity);

    public void Dispose() => _context.Dispose();

    #region Private Methods
    private IQueryable<Locality> DefineQueryByFlag(string query = null, string flag = null)
    {
        return flag switch
        {
            "city" => _context.Localities.AsQueryable().Where(x => EF.Functions.Like(x.City, $"%{query}%")),
            "state" => _context.Localities.AsQueryable().Where(x => EF.Functions.Like(x.State, $"%{query}%")),
            "ibge" => _context.Localities.AsQueryable().Where(x => EF.Functions.Like(x.Id, $"%{query}%")),
            _ => _context.Localities.AsQueryable().Where(x => EF.Functions.Like(x.State, $"%{query}%")),
        };
    } 
    #endregion
}