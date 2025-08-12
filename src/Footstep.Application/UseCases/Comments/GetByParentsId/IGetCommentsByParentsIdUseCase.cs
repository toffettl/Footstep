using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByParentsId
{
    public interface IGetCommentsByParentsIdUseCase
    {
        Task<PagedResult<ResponseCommentJson>> Execute(Guid id, int page, int pageSize);
    }
}
