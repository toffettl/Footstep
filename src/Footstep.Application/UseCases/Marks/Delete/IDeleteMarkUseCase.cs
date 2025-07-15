namespace Footstep.Application.UseCases.Marks.Delete
{
    public interface IDeleteMarkUseCase
    {
        Task Execute(Guid id);
    }
}
