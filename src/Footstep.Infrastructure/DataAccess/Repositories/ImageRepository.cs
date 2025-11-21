using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Images;

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
    }
}
