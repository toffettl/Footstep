using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Styles
{
    public interface IStyleWriteOnlyRepository
    {
        Task Add(Style user);
    }
}
