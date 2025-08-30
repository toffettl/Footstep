using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Comments
{
    public interface ICommentsUpdateOnlyRepository
    {
        void Update(Comment comment);
    }
}
