namespace ConsultaIbge.Application.Dtos;

public record ApiResponseDto<T>(T? Result, string? ErrorMessage);
