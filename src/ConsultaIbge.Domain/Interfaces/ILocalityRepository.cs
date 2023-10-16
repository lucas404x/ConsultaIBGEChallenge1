using ConsultaIbge.Core.Data;
using ConsultaIbge.Domain.Entities;

namespace ConsultaIbge.Domain.Interfaces;

public interface ILocalityRepository : IRepository<Locality>
{
    Task<IEnumerable<Locality>> GetAllAsync();
    Task<Locality> GetByIdAsync(string id);

    void Add(Locality entity);
    void Update(Locality entity);
    void Delete(Locality entity);
}
