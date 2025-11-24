using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Images;
using Microsoft.EntityFrameworkCore;

namespace Footstep.Infrastructure.DataAccess.Repositories
{
    public class ImageRepository : IImageWriteOnlyRepository
    {
        private readonly FootstepDbContext _dbContext;
        public ImageRepository(FootstepDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Image image)
        {
            await _dbContext.Images.AddAsync(image);
        }

        public async Task<bool?> Delete(Guid id)
        {
            var result = await _dbContext.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (result == null)
            {
                return false;
            }

            _dbContext.Images.Remove(result);

            return true;
        }
    }
}
