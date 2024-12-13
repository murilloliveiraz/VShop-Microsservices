namespace Products.API.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
