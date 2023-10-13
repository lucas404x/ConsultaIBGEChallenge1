using ConsultaIbge.Api.Configuration;
using ConsultaIbge.Application.Dtos;
using ConsultaIbge.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();


var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(app.Environment);


//app.MapGet("/", async (IIbgeService _service, [FromQuery]IbgeGetAllDto entity) => await _service.GetAllAsync(entity.PageSize, entity.PageIndex, entity.Query));

app.MapGet("/ibge/{id}", async (IIbgeService _service, string id) =>
{
    var result = await _service.GetByIdAsync(id);

    if (result is null) return Results.NotFound();

    return Results.Ok(result);
});

app.MapPost("/ibge/add", async (IIbgeService _service, IbgeAddDto entity) =>
{
    var result = await _service.Add(entity);

    if(!result) return Results.BadRequest();

    return Results.Ok();
});

app.MapPut("/ibge/update", async (IIbgeService _service, IbgeUpdateDto entity, string id) =>
{
    if(id != entity.Id) return Results.BadRequest();

    var ibge = await _service.GetByIdAsync(id);
    if(ibge is null) return Results.NotFound();

    var result = await _service.Update(entity);

    if (!result) return Results.BadRequest();

    return Results.Ok();
});

app.MapDelete("/ibge/remove/{id}", async (IIbgeService _service, string id) =>
{
    var entity = await _service.GetByIdAsync(id);
    if (entity is null) return Results.NotFound();

    var result = await _service.Delete(id);

    if (!result) return Results.BadRequest();

    return Results.Ok();

});

app.Run();