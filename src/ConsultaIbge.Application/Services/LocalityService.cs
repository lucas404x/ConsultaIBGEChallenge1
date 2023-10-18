using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Core.Enums;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Exceptions.Locality;
using ConsultaIbge.Domain.Interfaces;

namespace ConsultaIbge.Application.Services;

public class LocalityService : ILocalityService
{
    private readonly ILocalityRepository _repository;

    public LocalityService(ILocalityRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<Locality>> GetAllAsync(int pageSize, int pageIndex, string? query) => await _repository.GetAsync(pageSize, pageIndex, query, FlagQueryEnum.Default);

    public async Task<PagedResult<Locality>> GetByCityAsync(int pageSize, int pageIndex, string? query) => await _repository.GetAsync(pageSize, pageIndex, query, FlagQueryEnum.City);

    public async Task<PagedResult<Locality>> GetByStateAsync(int pageSize, int pageIndex, string? query) => await _repository.GetAsync(pageSize, pageIndex, query, FlagQueryEnum.State);

    public async Task<PagedResult<Locality>> GetByIbgeCodeAsync(int pageSize, int pageIndex, string? query) => await _repository.GetAsync(pageSize, pageIndex, query, FlagQueryEnum.Ibge);

    public async Task<Locality?> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);

    public async Task<bool> Add(LocalityAddDto entity)
    {
        _repository.Add(new Locality(entity.Id, entity.State, entity.City));
        return await _repository.UnitOfWork.CommitAsync();
    }

    public async Task<bool> Update(LocalityUpdateDto entity)
    {
        if (!await _repository.ExistsAsync(entity.Id)) throw new LocalityNotFoundException($"A localidade {entity.Id} não foi encontrada.");
        _repository.Update(new Locality(entity.Id, entity.State, entity.City));
        return await _repository.UnitOfWork.CommitAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result is null) throw new LocalityNotFoundException($"A localidade {id} não foi encontrada.");
        _repository.Delete(result);
        return await _repository.UnitOfWork.CommitAsync();
    }
}
