using ConsultaIbge.Application.Dtos.Commom;
using ConsultaIbge.Application.Dtos.User;
using ConsultaIbge.Application.Filters;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaIbge.Api.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/v1/user")
            .WithTags("user")
            .WithOpenApi();

        root.MapPost("/login", Login)
            .AddEndpointFilter<ValidationFilter<UserLoginDto>>()
            .Produces<ApiResponse<UserResponseDto>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Make login and returns bearer token");

        root.MapPost("/register", Register)
            .AddEndpointFilter<ValidationFilter<UserRegisterDto>>()
            .Produces<ApiResponse<UserResponseDto>>()
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create User and returns bearer token"); ;

        return app;
    }

    public static async Task<IResult> Login([FromServices] IUserService _service, [FromBody] UserLoginDto request)
    {
        var response = new ApiResponse<UserResponseDto>();
        try
        {
            var result = await _service.Login(request);
            response.SetSuccess(result);
            return Results.Ok(response);
        }
        catch (InvalidProvidedCredentialsException e)
        {
            response.SetError(e.Message);
            return Results.UnprocessableEntity(response);
        }
        catch(Exception ex)
        {
            response.SetError(ex.Message);
            return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> Register([FromServices] IUserService _service, [FromBody] UserRegisterDto request)
    {
        var response = new ApiResponse<UserResponseDto>();
        try
        {
            var result = await _service.Register(request);
            response.SetSuccess(result);
            return Results.Ok(response);
        }
        catch (EmailAlreadyExistsException e)
        {
            response.SetError(e.Message);
            return Results.UnprocessableEntity(response);
        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
            return Results.Json(response, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}