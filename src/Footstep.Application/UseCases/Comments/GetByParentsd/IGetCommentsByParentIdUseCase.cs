using Footstep.Communication.Enums;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByParentsId
{
    public interface IGetCommentsByParentIdUseCase
    {
        Task<PagedResult<ResponseCommentJson>> Execute(Guid parentId, ParentType type, int page, int pageSize);
    }
}
