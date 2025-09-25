using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Comments
{
    public interface ICommentsReadOnlyRepository
    {
        Task<Comment?> GetById(Guid id);
        Task<(List<Comment> Comments, int TotalCount)> GetByPointOfInterestId(Guid id, int page, int pageSize);
        Task<(List<Comment> Comments, int TotalCount)> GetByCommentId(Guid id, int page, int pageSize);
        Task<(List<Comment> Comments, int TotalCount)> GetByUserId(Guid id, int page, int pageSize);
        Task<List<Comment>> GetByPointOfInterestIdAndUserId(Guid pointOfInterestId, Guid userId);
        Task<List<Comment>> GetByCommentIdAndUserId(Guid commentId, Guid userId);
    }
}
