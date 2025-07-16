using Footstep.Communication.Responses.Marks;

namespace Footstep.Application.UseCases.Marks.GetNearby
{
    public interface IGetNearbyMarkUseCase
    {
        Task<ResponseMarksJson> Execute(double latitude, double longitude, double radiusInMeters);
    }
}
