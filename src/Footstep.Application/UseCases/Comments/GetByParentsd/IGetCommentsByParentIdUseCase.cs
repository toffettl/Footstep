using Footstep.Communication.Enums;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByParentsId
{
    public interface IGetCommentsByParentIdUseCase
    {
        Task<List<ResponseCommentJson>> Execute(Guid parentId, ParentType type);
    }
}
