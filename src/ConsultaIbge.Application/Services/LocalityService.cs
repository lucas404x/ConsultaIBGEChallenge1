using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Interfaces;

namespace ConsultaIbge.Application.Services;

public class LocalityService : ILocalityService
{
    private readonly ILocalityRepository _repository;

    public LocalityService(ILocalityRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<Locality>>GetAllAsync(int pageSize, int pageIndex, string query) => await _repository.GetAllAsync(pageSize, pageIndex, query);

    public async Task<Locality?> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);

    public async Task<bool> Add(LocalityAddDto entity)
    {
        _repository.Add(new Locality(entity.Id, entity.State, entity.City));

        return await _repository.UnitOfWork.CommitAsync();
    }

    public async Task<bool> Update(LocalityUpdateDto entity)
    {
        _repository.Update(new Locality(entity.Id, entity.State, entity.City));

        return await _repository.UnitOfWork.CommitAsync();
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _repository.GetByIdAsync(id);
        _repository.Delete(result);

        return await _repository.UnitOfWork.CommitAsync();
    }
}
