using ConsultaIbge.Api.Configuration;
using ConsultaIbge.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

var app = builder.Build();
app.UseSwaggerConfiguration();
app.UseApiConfiguration();
app.MapHomeEndpoints();
app.MapUserEndpoints();
app.MapLocalityEndpoints();

app.Run();