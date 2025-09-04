using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Comments
{
    public interface ICommentsReadOnlyRepository
    {
        Task<Comment?> GetById(Guid id);
        Task<List<Comment>> GetByPointOfInterestId(Guid id);
        Task<List<Comment>> GetByCommentId(Guid id);
        Task<List<Comment>> GetByUserId(Guid id);
        Task<List<Comment>> GetByPointOfInterestIdAndAuthorId(Guid pointOfInterestId, Guid authorId);
        Task<List<Comment>> GetByCommentIdAndAuthorId(Guid commentId, Guid authorId);
    }
}
