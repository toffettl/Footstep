using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.Update
{
    public interface IUpdateCommentLikeUseCase
    {
        Task Execute(Guid id, Guid userId);
    }
}
