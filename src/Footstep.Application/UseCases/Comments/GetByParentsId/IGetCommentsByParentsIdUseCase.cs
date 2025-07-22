using Footstep.Communication.Responses.Comments;

namespace Footstep.Application.UseCases.Comments.GetByParentsId
{
    public interface IGetCommentsByParentsIdUseCase
    {
        Task<List<ResponseCommentJson>> Execute(Guid id);
    }
}
