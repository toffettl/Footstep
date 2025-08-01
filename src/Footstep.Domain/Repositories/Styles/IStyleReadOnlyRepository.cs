namespace Footstep.Domain.Repositories.Styles
{
    public interface IStyleReadOnlyRepository
    {
        Task<bool> ExistActiveStyleWithName(string name);
    }
}
