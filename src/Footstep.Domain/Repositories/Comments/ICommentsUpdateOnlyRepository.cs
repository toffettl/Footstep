using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Comments
{
    public interface ICommentsUpdateOnlyRepository
    {
        Task<Comment?> GetById(Guid id);
        void Update(Comment comment);
    }
}
