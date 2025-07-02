using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetById
{
    public interface IGetByIdTraceUseCase
    {
        Task<ResponseTraceJson> Execute(Guid id);
    }
}
