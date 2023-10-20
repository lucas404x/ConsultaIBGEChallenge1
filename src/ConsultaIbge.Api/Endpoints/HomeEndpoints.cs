namespace ConsultaIbge.Api.Endpoints;

public static class HomeEndpoints
{
    public static WebApplication MapHomeEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => Results.Ok(":-)"));
        return app;
    }
}
