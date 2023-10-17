namespace ConsultaIbge.Domain.Entities;

public record PagedResult<T>(IEnumerable<T> List, int TotalResults, int PageIndex, int PageSize, string Query) where T : class;
