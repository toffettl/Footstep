using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.Create
{
    public interface ICreatePointOfInterestUseCase
    {
        Task<ResponsePointOfIntereseJson> Execute(RequestPointOfInterestJson request);
    }
}
