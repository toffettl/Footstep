using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Addresses
{
    public interface IAddressReadOnlyRepository
    {
        Task<Address?> GetByLatitudeAndLongitude(double latitude, double longitude);
    }
}
