using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Images
{
    public interface IImageWriteOnlyRepository
    {
        Task Add(Image image);
        Task<bool?> Delete(Guid Id);
    }
}
