using ConsultaIbge.Api.Configuration;
using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Filters;
using ConsultaIbge.Application.Interfaces;

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
    var result = await _service.GetByIdAsync(id);

    if (result is null) return Results.NotFound();

    return Results.Ok(result);
}).RequireAuthorization();

app.MapPost("v1/locality/add", async (ILocalityService _service, LocalityAddDto entity) =>
{
    var result = await _service.Add(entity);

    if (!result) return Results.BadRequest();

    return Results.Ok();
}).RequireAuthorization().AddEndpointFilter<ValidationFilter<LocalityAddDto>>();

app.MapPut("v1/locality/update", async (ILocalityService _service, LocalityUpdateDto entity, string id) =>
{
    if (id != entity.Id) return Results.BadRequest();

    var ibge = await _service.GetByIdAsync(id);
    if (ibge is null) return Results.NotFound();

    var result = await _service.Update(entity);

    if (!result) return Results.BadRequest();

    return Results.Ok();
}).RequireAuthorization().AddEndpointFilter<ValidationFilter<LocalityUpdateDto>>();

app.MapDelete("v1/locality/remove/{id}", async (ILocalityService _service, string id) =>
{
    var entity = await _service.GetByIdAsync(id);
    if (entity is null) return Results.NotFound();

    var result = await _service.Delete(id);

    if (!result) return Results.BadRequest();

    return Results.Ok();

}).RequireAuthorization();
#endregion

#endregion

app.Run();