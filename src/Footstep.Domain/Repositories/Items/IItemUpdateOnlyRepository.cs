using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Items
{
    public interface IItemUpdateOnlyRepository
    {
        void Update(Item item);
    }
}
