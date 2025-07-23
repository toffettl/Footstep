namespace Footstep.Application.UseCases.Comments.Delete
{
    public interface IDeleteCommentUseCase
    {
        Task Execute(Guid id);
    }
}
