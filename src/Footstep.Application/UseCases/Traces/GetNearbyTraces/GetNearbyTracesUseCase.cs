using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.GetByRay;
public class GetNearbyTracesUseCase : IGetNearbyTracesUseCase
{
    private readonly ITracesReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetNearbyTracesUseCase(ITracesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ResponseTraceJson>> Execute(double latitude, double longitude, double radiusInMeters)
    {
        var allTraces = await _repository.GetAll();

        var nearbyTraces = allTraces
            .Where(t => CalculateDistanceInMeters(latitude, longitude, t.Latitude, t.Longitude) <= radiusInMeters)
            .ToList();

        return _mapper.Map<List<ResponseTraceJson>>(nearbyTraces);
    }

    private double CalculateDistanceInMeters(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371000;
        var latRad1 = DegreesToRadians(lat1);
        var latRad2 = DegreesToRadians(lat2);
        var deltaLat = DegreesToRadians(lat2 - lat1);
        var deltaLon = DegreesToRadians(lon2 - lon1);

        var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                Math.Cos(latRad1) * Math.Cos(latRad2) *
                Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c;
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
}
