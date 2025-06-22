namespace Footstep.Application.UseCases.Traces.Delete
{
    public interface IDeleteTraceUseCase
    {
        Task Execute(Guid id);
    }
}
