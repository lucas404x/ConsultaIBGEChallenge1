namespace ConsultaIbge.Domain.Exceptions;

public sealed class EmailAlreadyExistsException : DomainException
{
    public EmailAlreadyExistsException(string message) : base(message)
    {
    }

    public EmailAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
