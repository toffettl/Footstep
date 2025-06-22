using Footstep.Communication.Responses;

namespace Footstep.Application.UseCases.Traces.GetById
{
    public interface IGetByIdTraceUseCase
    {
        Task<ResponseTraceJson> Execute(Guid id);
    }
}
