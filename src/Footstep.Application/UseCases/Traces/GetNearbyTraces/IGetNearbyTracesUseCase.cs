using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetByRay;

public interface IGetNearbyTracesUseCase
{
    Task<List<ResponseTraceJson>> Execute(double latitude, double longitude, double radiusInMeters);
}