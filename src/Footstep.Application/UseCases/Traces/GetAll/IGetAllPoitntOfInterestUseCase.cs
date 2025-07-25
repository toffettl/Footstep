using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetAll
{
    public interface IGetAllPoitntOfInterestUseCase
    {
        Task<ResponsePointOfInterestJson> Execute();
    }
}
