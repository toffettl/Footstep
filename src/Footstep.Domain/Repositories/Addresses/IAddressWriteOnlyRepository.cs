using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Addresses
{
    public interface IAddressWriteOnlyRepository
    {
        Task Add(Address address);
    }
}
