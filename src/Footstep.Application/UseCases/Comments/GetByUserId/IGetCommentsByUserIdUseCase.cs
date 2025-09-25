using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByAuthorId
{
    public interface IGetCommentsByUserIdUseCase
    {
        Task<PagedResult<ResponseCommentJson>> Execute(Guid id, int page, int pageSize);
    }
}
