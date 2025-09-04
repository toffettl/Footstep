using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.Update
{
    public interface IUpdateCommentLikeCommentUseCase
    {
        Task Execute(Guid id, Guid userId);
    }
}
