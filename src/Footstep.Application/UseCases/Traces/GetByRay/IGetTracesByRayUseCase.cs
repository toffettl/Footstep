using Footstep.Communication.Responses;

namespace Footstep.Application.UseCases.Traces.GetByRay;

public interface IGetTracesByRayUseCase
{
    Task<List<ResponseTraceJson>> Execute(Guid id, double radiusInMeters);
}