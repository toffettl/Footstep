using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetById
{
    public interface IGetByIdPointOfInterestUseCase
    {
        Task<ResponsePointOfIntereseJson> Execute(Guid id);
    }
}
