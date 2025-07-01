using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetAll
{
    public interface IGetAllTraceUseCase
    {
        Task<ResponseTracesJson> Execute();
    }
}
