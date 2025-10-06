using Footstep.Communication.Requests.Traces;

namespace Footstep.Application.UseCases.Traces.UpdateStatus
{
    public interface IUpdateStatusPointOfInterestUseCase
    {
        Task Execute(Guid id, Guid userId, bool like);
    }
}
