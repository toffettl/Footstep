using Footstep.Communication.Requests.Traces;

namespace Footstep.Application.UseCases.Traces.Update
{
    public interface IUpdateTraceUseCase
    {
        Task Execute(Guid id, RequestTraceJson request);
    }
}
