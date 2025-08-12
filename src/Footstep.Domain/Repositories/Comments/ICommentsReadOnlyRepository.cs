using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Comments
{
    public interface ICommentsReadOnlyRepository
    {
        Task<(List<Comment> Comments, int TotalCount)> GetByParentsId(Guid id, int page, int pageSize);
        Task<List<Comment>> GetByAuthorId(Guid id);
        Task<List<Comment>> GetByParentIdAndAuthorId(Guid parentId, Guid authorId);
    }
}
