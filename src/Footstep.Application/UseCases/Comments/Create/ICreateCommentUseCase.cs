
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.Create
{
    public interface ICreateCommentUseCase
    {
        Task<ResponseCommentJson> Execute(RequestCommentJson request);
    }
}
