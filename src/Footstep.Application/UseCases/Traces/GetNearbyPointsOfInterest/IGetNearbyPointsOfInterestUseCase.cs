using Footstep.Communication.Responses.Traces;

namespace Footstep.Application.UseCases.Traces.GetByRay;

public interface IGetNearbyPointsOfInterestUseCase
{
    Task<List<ResponsePaginationPointOfInterestJson>> Execute(double latitude, double longitude, double radiusInMeters);
}