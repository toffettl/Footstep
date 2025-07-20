using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetByRay;

public interface IGetNearbyPointsOfInterestUseCase
{
    Task<List<ResponsePointOfIntereseJson>> Execute(double latitude, double longitude, double radiusInMeters);
}