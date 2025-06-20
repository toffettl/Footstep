namespace Footstep.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
