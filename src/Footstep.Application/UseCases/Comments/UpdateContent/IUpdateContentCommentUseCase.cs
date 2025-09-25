using Footstep.Communication.Requests.Comments;

namespace Footstep.Application.UseCases.Comments.UpdateContent
{
    public interface IUpdateContentCommentUseCase
    {
        Task Execute(Guid id, RequestUpdateContentComment request);
    }
}
