using ConsultaIbge.Core.Data;
using ConsultaIbge.Domain.Entities;

namespace ConsultaIbge.Domain.Interfaces;

public interface IIbgeRepository : IRepository<Ibge>
{
    Task<PagedResult<Ibge>> GetAllAsync(int pageSize, int pageIndex, string query = null);
    Task<Ibge> GetByIdAsync(string id);

    void Add(Ibge entity);
    void Update(Ibge entity);
    void Delete(Ibge entity);
}
