namespace ConsultaIbge.Domain.Exceptions.Locality;

public sealed class LocalityNotFoundException : DomainException
{
    public LocalityNotFoundException(string message) : base(message)
    {
    }

    public LocalityNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
