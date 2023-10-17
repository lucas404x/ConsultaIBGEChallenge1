using ConsultaIbge.Application.Dtos.Commom;
using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Filters;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Domain.Entities;
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
            .AddEndpointFilter<ValidationFilter<LocalityUpdateDto>>()
            .Produces<ApiResponse<bool>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Remove a location given an id");

        return app;
    }

    public static async Task<PagedResult<Locality>> GetAll([FromServices] ILocalityService _service, [FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string query = null) => 
        await _service.GetAllAsync(ps, page, query);

    public static async Task<PagedResult<Locality>> GetByCity([FromServices] ILocalityService _service, [FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string query = null) =>
            await _service.GetByCityAsync(ps, page, query);

    public static async Task<PagedResult<Locality>> GetByState([FromServices] ILocalityService _service, [FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string query = null) =>
        await _service.GetByStateAsync(ps, page, query);

    public static async Task<PagedResult<Locality>> GetByIbgeCode([FromServices] ILocalityService _service, [FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string query = null) =>
        await _service.GetByIbgeCodeAsync(ps, page, query);

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

    public static async Task<IResult> UpdateLocality([FromServices] ILocalityService _service, [FromBody] LocalityUpdateDto request, [FromQuery] string id)
    {
        var response = new ApiResponse<bool>();
        if (id != request.Id)
        {
            response.SetError("O id informado na entity e o id informado no header não coincidem.");
            return Results.BadRequest(response);
        }
        var locality = await _service.GetByIdAsync(id);
        if (locality is null)
        {
            response.SetError($"O id '{request.Id}' não foi encontrado.");
            return Results.NotFound(response);
        }
        var result = await _service.Update(request);
        if (!result)
        {
            response.SetError($"Não foi possível atualizar o registro '{request.Id}'");
            return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
        }
        response.SetSuccess(true);
        return Results.Ok(response);
    }

    public static async Task<IResult> RemoveLocality([FromServices] ILocalityService _service, [FromQuery] string id)
    {
        var response = new ApiResponse<bool>();
        var locality = await _service.GetByIdAsync(id);
        if (locality is null)
        {
            response.SetError($"O id '{id}' não foi encontrado.");
            return Results.NotFound(response);
        }
        var result = await _service.Delete(id);
        if (!result)
        {
            response.SetError($"Não foi possível deletar o registro '{id}'");
            return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
        }
        return Results.Ok(response);
    }
}