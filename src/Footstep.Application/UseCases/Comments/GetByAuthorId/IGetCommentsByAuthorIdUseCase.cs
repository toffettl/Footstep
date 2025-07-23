using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByAuthorId
{
    public interface IGetCommentsByAuthorIdUseCase
    {
        Task<List<ResponseCommentJson>> Execute(Guid id);
    }
}
