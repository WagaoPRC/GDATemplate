namespace GDATemplate.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task Rollback();
    }
}