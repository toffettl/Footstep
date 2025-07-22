using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Comments
{
    public interface ICommentsReadOnlyRepository
    {
        Task<List<Comment>> GetByParentsId(Guid id);
        Task<List<Comment>> GetByAuthorId(Guid id);
        Task<List<Comment>> GetByParentIdAndAuthorId(Guid parentId, Guid authorId);
    }
}
