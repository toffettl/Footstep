using Footstep.Communication.Requests.Traces;

namespace Footstep.Application.UseCases.Traces.Update
{
    public interface IUpdatePointOfInterestUseCase
    {
        Task Execute(Guid id, RequestPointOfInterestJson request);
    }
}
