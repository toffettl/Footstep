using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Addresses
{
    public interface IAddressReadOnlyRepository
    {
        Task<Address?> GetById(Guid id);
        Task<Address?> GetByLatitudeAndLongitude(double latitude, double longitude);
    }
}
