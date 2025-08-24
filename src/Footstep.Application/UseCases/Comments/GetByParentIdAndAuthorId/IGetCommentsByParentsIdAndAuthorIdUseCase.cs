using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByParentIdAndAuthorId
{
    public interface IGetCommentsByParentsIdAndAuthorIdUseCase
    {
        Task<List<ResponseCommentJson>> Execute(Guid parenId, Guid authorId, int type);
    }
}
