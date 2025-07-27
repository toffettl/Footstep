using Footstep.Communication.Requests.Traces;

namespace Footstep.Application.UseCases.Traces.UpdateStatus
{
    public interface IUpdateStatusPointOfInterestUseCase
    {
        Task Execute(Guid id, RequestUpdateStatusPointOfInterestJson request);
        Task Execute(Guid id, RequestUpdateStatusPointOfInterestJson.RequestUpdateLikesPointOfInterestJson requestView);

        Task Execute(Guid id, RequestUpdateStatusPointOfInterestJson.RequestUpdateViewsPointOfInterestJson requestLike);
    }
}
