using ConsultaIbge.Application.Dtos;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Interfaces;

namespace ConsultaIbge.Application.Services;

public class IbgeService : IIbgeService
{
    private readonly IIbgeRepository _repository;

    public IbgeService(IIbgeRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<Ibge>> GetAllAsync(int pageSize, int pageIndex, string query = null) => await _repository.GetAllAsync(pageSize, pageIndex, query);

    public async Task<Ibge> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);

    public async Task<bool> Add(IbgeAddDto entity)
    {
        _repository.Add(new Ibge(entity.Id, entity.State, entity.City));

        return await _repository.UnitOfWork.CommitAsync();
    }

    public async Task<bool> Update(IbgeUpdateDto entity)
    {
        _repository.Update(new Ibge(entity.Id, entity.State, entity.City));

        return await _repository.UnitOfWork.CommitAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _repository.GetByIdAsync(id);
        _repository.Delete(result);

        return await _repository.UnitOfWork.CommitAsync();
    }
}
