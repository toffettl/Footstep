using Footstep.Communication.Requests.Comments;

namespace Footstep.Application.UseCases.Comments.Update
{
    public interface IUpdateStatusCommentsUseCase
    {
        Task Execute(Guid id, RequestUpdateStatusCommentsJson request);
    }
}
