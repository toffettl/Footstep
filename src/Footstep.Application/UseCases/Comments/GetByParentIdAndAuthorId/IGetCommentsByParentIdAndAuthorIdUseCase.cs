using Footstep.Communication.Enums;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByParentIdAndAuthorId
{
    public interface IGetCommentsByParentIdAndAuthorIdUseCase
    {
        Task<List<ResponseCommentJson>> Execute(Guid parenId, Guid authorId, ParentType parentType);
    }
}
