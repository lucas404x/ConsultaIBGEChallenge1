using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Domain.Entities;

namespace ConsultaIbge.Application.Interfaces;

public interface ILocalityService
{
    Task<IEnumerable<Locality>> GetAllAsync();
    Task<Locality?> GetByIdAsync(string id);

    Task<bool> Add(LocalityAddDto entity);
    Task<bool> Update(LocalityUpdateDto entity);
    Task<bool> Delete(string id);
}
