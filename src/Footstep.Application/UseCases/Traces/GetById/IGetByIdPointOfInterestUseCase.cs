using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetById
{
    public interface IGetByIdPointOfInterestUseCase
    {
        Task<ResponsePointOfInterestJson> Execute(Guid id);
    }
}
