using Footstep.Communication.Responses;

namespace Footstep.Application.UseCases.Traces.GetAll
{
    public interface IGetAllTraceUseCase
    {
        Task<ResponseTracesJson> Execute();
    }
}
