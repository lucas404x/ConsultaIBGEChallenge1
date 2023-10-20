using ConsultaIbge.Application.Dtos.Commom;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ConsultaIbge.Application.Filters;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        T? argToValidate = (T?)context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T));
        IValidator<T>? validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync(argToValidate!);
            if (!validationResult.IsValid)
            {
                var response = new ApiResponse<T>();
                response.SetError(validationResult.Errors.ConvertAll(x => x.ErrorMessage));
                return Results.UnprocessableEntity(response);
            }
        }
        return await next.Invoke(context);
    }
}
