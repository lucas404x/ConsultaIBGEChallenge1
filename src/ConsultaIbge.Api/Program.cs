using ConsultaIbge.Api.Configuration;
using ConsultaIbge.Application.Dtos.Commom;
using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Filters;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

var app = builder.Build();
app.UseSwaggerConfiguration();

app.UseApiConfiguration();

#region Endpoints

#region User
app.MapPost("v1/user/login", async (IUserService _service, UserLoginDto request) =>
{
    var result = await _service.Login(request);
    if (result is null) return Results.BadRequest();
    return Results.Ok(result);
}).AddEndpointFilter<ValidationFilter<UserLoginDto>>();

app.MapPost("v1/user/register", async (IUserService _service, UserRegisterDto request) =>
{
    var result = await _service.Register(request);
    if (result is null) return Results.BadRequest();
    return Results.Ok(result);
}).AddEndpointFilter<ValidationFilter<UserRegisterDto>>();
#endregion

#region Locality
app.MapGet("v1/locality/{id}", async (ILocalityService _service, string id) =>
{
    var response = new ApiResponse<Locality>();
    var result = await _service.GetByIdAsync(id);
    if (result is null)
    {
        response.SetError($"A localidade '{id}' não foi encontrada.");
        Results.NotFound(response);
    }
    return Results.Ok(response);
}).RequireAuthorization();

app.MapPost("v1/locality/add", async (ILocalityService _service, LocalityAddDto request) =>
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
}).RequireAuthorization().AddEndpointFilter<ValidationFilter<LocalityAddDto>>();

app.MapPut("v1/locality/update", async (ILocalityService _service, LocalityUpdateDto request, string id) =>
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
}).RequireAuthorization().AddEndpointFilter<ValidationFilter<LocalityUpdateDto>>();

app.MapDelete("v1/locality/remove/{id}", async (ILocalityService _service, string id) =>
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
    return Results.Ok();

}).RequireAuthorization();
#endregion

#endregion

app.Run();