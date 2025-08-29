using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByAuthorId
{
    public interface IGetCommentsByUserIdUseCase
    {
        Task<List<ResponseCommentJson>> Execute(Guid id);
    }
}
