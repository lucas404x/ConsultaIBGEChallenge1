using ConsultaIbge.Application.Dtos;
using ConsultaIbge.Domain.Entities;

namespace ConsultaIbge.Application.Interfaces;

public interface IIbgeService
{
    Task<PagedResult<Ibge>> GetAllAsync(int pageSize, int pageIndex, string query = null);
    Task<Ibge> GetByIdAsync(string id);

    Task<bool> Add(IbgeAddDto entity);
    Task<bool> Update(IbgeUpdateDto entity);
    Task<bool> Delete(string id);
}
