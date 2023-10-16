namespace ConsultaIbge.Application.Dtos.Commom;

public record PagedResultDto<T>(IEnumerable<T> List, int TotalResults, int PageIndex, int PageSize, string Query) where T : class;
