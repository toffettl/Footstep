using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Addresses;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class AddressRepository : 
        IAddressWriteOnlyRepository, 
        IAddressReadOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;

        public AddressRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Address address)
        {
            await _dbContext.Addresses
                .AddAsync(address);
        }

        public async Task<Address?> GetById(Guid id)
        {
            return await _dbContext.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Address?> GetByLatitudeAndLongitude(double latitude, double longitude)
        {
            return await _dbContext.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Latitude == latitude && a.Longitude == longitude);
        }
    }
}
