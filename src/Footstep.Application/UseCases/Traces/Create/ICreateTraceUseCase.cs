using Footstep.Communication.Requests;
using Footstep.Communication.Responses;

namespace Footstep.Application.UseCases.Traces.Create
{
    public interface ICreateTraceUseCase
    {
        Task<ResponseCreateTraceJson> Execute(RequestTraceJson request);
    }
}
