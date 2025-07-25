using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.Update
{
    public interface IUpdateStatusCommentsUseCase
    {
        Task Execute(Guid id, RequestUpdateStatusCommentsJson request);
    }
}
