using Footstep.Communication.Requests.Traces;

namespace Footstep.Application.UseCases.Traces.UpdateStatus
{
    public interface IUpdateStatusPointOfInterestUseCase
    {
        Task Execute(Guid id, RequestUpdateStatusPointOfInterestJson request);
    }
}
