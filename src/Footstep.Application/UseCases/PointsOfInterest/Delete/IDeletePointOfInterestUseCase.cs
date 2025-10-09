namespace Footstep.Application.UseCases.Traces.Delete
{
    public interface IDeletePointOfInterestUseCase
    {
        Task Execute(Guid id);
    }
}
