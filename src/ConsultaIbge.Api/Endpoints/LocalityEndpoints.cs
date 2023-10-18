using ConsultaIbge.Application.Dtos.Commom;
using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Filters;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Exceptions.Locality;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaIbge.Api.Endpoints;

public static class LocalityEndpoints
{
    public static WebApplication MapLocalityEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/v1/locality")
            .WithTags("locality")
            .WithOpenApi()
            .RequireAuthorization();

        root.MapGet("/", GetAll)
            .Produces<PagedResult<Locality>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all locations");

        root.MapGet("/{id}", GetById);

        root.MapGet("/get-city", GetByCity)
            .Produces<PagedResult<Locality>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get a location by City");

        root.MapGet("/get-state", GetByState)
           .Produces<PagedResult<Locality>>()
           .ProducesProblem(StatusCodes.Status404NotFound)
           .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
           .ProducesProblem(StatusCodes.Status500InternalServerError)
           .WithSummary("Get a location by State");

        root.MapGet("/get-ibge-code", GetByIbgeCode)
           .Produces<PagedResult<Locality>>()
           .ProducesProblem(StatusCodes.Status404NotFound)
           .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
           .ProducesProblem(StatusCodes.Status500InternalServerError)
           .WithSummary("Get a location by IBGE Code");

        root.MapPost("/", AddLocality)
            .AddEndpointFilter<ValidationFilter<LocalityAddDto>>()
            .Produces<ApiResponse<bool>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Try add a new location"); ;

        root.MapPut("/{id}", UpdateLocality)
            .AddEndpointFilter<ValidationFilter<LocalityUpdateDto>>()
            .Produces<ApiResponse<bool>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Update a location given an id");

        root.MapDelete("/{id}", RemoveLocality)
            .Produces<ApiResponse<bool>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Remove a location given an id");

        return app;
    }

    public static async Task<IResult> GetAll([FromServices] ILocalityService _service, [FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string? query = null)
    {
        var response = new ApiResponse<PagedResult<Locality>>();
        var result = await _service.GetAllAsync(ps, page, query);
        if (result.TotalResults == 0)
        {
            response.SetError("A consulta não encontrou nenhum resultado.");
            return Results.Json(response, statusCode: StatusCodes.Status404NotFound);
        }
        response.SetSuccess(result);
        return Results.Ok(response);
    }

    public static async Task<IResult> GetById([FromServices] ILocalityService _service, [FromRoute] string id)
    {
        var response = new ApiResponse<Locality>();
        var result = await _service.GetByIdAsync(id);
        if (result is null)
        {
            response.SetError($"Não foi possível encontrar o registro '{id}'.");
            return Results.UnprocessableEntity(response);
        }
        response.SetSuccess(result);
        return Results.Ok(response);
    }

    public static async Task<IResult> GetByCity([FromServices] ILocalityService _service, [FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string? query = null)
    {
        var response = new ApiResponse<PagedResult<Locality>>();
        var result = await _service.GetByCityAsync(ps, page, query);
        if (result.TotalResults == 0)
        {
            response.SetError("A consulta não encontrou nenhum resultado.");
            return Results.Json(response, statusCode: StatusCodes.Status404NotFound);
        }
        response.SetSuccess(result);
        return Results.Ok(response);
    }

    public static async Task<IResult> GetByState([FromServices] ILocalityService _service, [FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string? query = null)
    {
        var response = new ApiResponse<PagedResult<Locality>>();
        var result = await _service.GetByStateAsync(ps, page, query);
        if (result.TotalResults == 0)
        {
            response.SetError("A consulta não encontrou nenhum resultado.");
            return Results.Json(response, statusCode: StatusCodes.Status404NotFound);
        }
        response.SetSuccess(result);
        return Results.Ok(response);
    }

    public static async Task<IResult> GetByIbgeCode([FromServices] ILocalityService _service, [FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string? query = null)
    {
        var response = new ApiResponse<PagedResult<Locality>>();
        var result = await _service.GetByIbgeCodeAsync(ps, page, query);
        if (result.TotalResults == 0)
        {
            response.SetError("A consulta não encontrou nenhum resultado.");
            return Results.Json(response, statusCode: StatusCodes.Status404NotFound);
        }
        response.SetSuccess(result);
        return Results.Ok(response);
    }

    public static async Task<IResult> AddLocality([FromServices] ILocalityService _service, [FromBody] LocalityAddDto request)
    {
        var response = new ApiResponse<bool>();
        var result = await _service.Add(request);
        if (!result)
        {
            response.SetError("Não foi possível realizar a operação de gravação do registro.");
            return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
        }
        response.SetSuccess(true);
        return Results.Ok(response);
    }

    public static async Task<IResult> UpdateLocality([FromServices] ILocalityService _service, [FromBody] LocalityUpdateDto request, [FromRoute] string id)
    {
        var response = new ApiResponse<bool>();
        if (id != request.Id)
        {
            response.SetError("O id informado na entity e o id informado no header não coincidem.");
            return Results.BadRequest(response);
        }
        try
        {
            var result = await _service.Update(request);
            if (!result)
            {
                response.SetError($"Não foi possível atualizar o registro '{request.Id}'");
                return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
            }
            response.SetSuccess(true);
            return Results.Ok(response);
        }
        catch (LocalityNotFoundException ex)
        {
            response.SetError(ex.Message);
            return Results.UnprocessableEntity(response);
        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
            return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> RemoveLocality([FromServices] ILocalityService _service, [FromRoute] string id)
    {
        var response = new ApiResponse<bool>();
        try
        {
            var result = await _service.Delete(id);
            if (!result)
            {
                response.SetError($"Não foi possível deletar o registro '{id}'");
                return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        catch (LocalityNotFoundException ex)
        {
            response.SetError(ex.Message);
            return Results.UnprocessableEntity(response);
        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
            return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
        }
        return Results.Ok(response);
    }
}