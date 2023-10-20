namespace ConsultaIbge.Domain.Exceptions;

public sealed class InvalidProvidedCredentialsException : DomainException
{
    public InvalidProvidedCredentialsException(string message) : base(message)
    {
    }

    public InvalidProvidedCredentialsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
