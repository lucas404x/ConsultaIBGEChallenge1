using ConsultaIbge.Api.Configuration;
using ConsultaIbge.Application.Dtos;
using ConsultaIbge.Application.Filters;
using ConsultaIbge.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration();


app.MapPost("/user/login", async (IUserService _service, UserLoginDto request) =>
{
    var result = await _service.Login(request);
    if (result is null) return Results.BadRequest();
    return Results.Ok(result);
}).AddEndpointFilter<ValidationFilter<UserLoginDto>>();

app.MapPost("/user/register", async (IUserService _service, UserRegisterDto request) =>
{
    var result = await _service.Register(request);
    if (result is null) return Results.BadRequest();
    return Results.Ok(result);
}).AddEndpointFilter<ValidationFilter<UserRegisterDto>>(); ;

app.MapGet("/ibge/{id}", async (IIbgeService _service, string id) =>
{
    var result = await _service.GetByIdAsync(id);

    if (result is null) return Results.NotFound();

    return Results.Ok(result);
}).RequireAuthorization();

app.MapPost("/ibge/add", async (IIbgeService _service, IbgeAddDto entity) =>
{
    var result = await _service.Add(entity);

    if(!result) return Results.BadRequest();

    return Results.Ok();
}).RequireAuthorization();

app.MapPut("/ibge/update", async (IIbgeService _service, IbgeUpdateDto entity, string id) =>
{
    if(id != entity.Id) return Results.BadRequest();

    var ibge = await _service.GetByIdAsync(id);
    if(ibge is null) return Results.NotFound();

    var result = await _service.Update(entity);

    if (!result) return Results.BadRequest();

    return Results.Ok();
}).RequireAuthorization();

app.MapDelete("/ibge/remove/{id}", async (IIbgeService _service, string id) =>
{
    var entity = await _service.GetByIdAsync(id);
    if (entity is null) return Results.NotFound();

    var result = await _service.Delete(id);

    if (!result) return Results.BadRequest();

    return Results.Ok();

}).RequireAuthorization();

app.Run();