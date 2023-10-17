﻿using ConsultaIbge.Core.Data;
using ConsultaIbge.Domain.Entities;

namespace ConsultaIbge.Domain.Interfaces;

public interface ILocalityRepository : IRepository<Locality>
{
    Task<PagedResult<Locality>> GetAsync(int pageSize, int pageIndex, string query, string flag);
    Task<Locality> GetByIdAsync(string id);

    void Add(Locality entity);
    void Update(Locality entity);
    void Delete(Locality entity);
}
