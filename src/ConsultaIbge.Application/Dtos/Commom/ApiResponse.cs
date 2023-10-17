namespace ConsultaIbge.Application.Dtos.Commom;

public class ApiResponse<T>
{
    public T? Result { get; private set; }
    public List<string> Errors { get; private set; } = new();

    public void SetSuccess(T result)
    {
        Result = result;
        Errors = new();
    }

    public void SetError(string errorMessage)
    {
        Result = default;
        Errors.Add(errorMessage);
    }
    public void SetError(List<string> errors)
    {
        Result = default;
        Errors = errors;
    }
}