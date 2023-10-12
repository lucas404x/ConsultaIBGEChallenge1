namespace ConsultaIbge.Data.Uow;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
