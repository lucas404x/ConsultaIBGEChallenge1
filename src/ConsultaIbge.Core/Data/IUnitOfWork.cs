namespace ConsultaIbge.Core.Data;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
