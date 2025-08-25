using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Items
{
    public interface IItemWriteOnlyRepository
    {
        Task Add(Item item);
    }
}
