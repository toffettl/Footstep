using AutoMapper;
using Footstep.Communication.Responses.Marks;
using Footstep.Domain.Repositories.Marks;

namespace Footstep.Application.UseCases.Marks.GetNearby
{
    public class GetNearbyMarkUseCase : IGetNearbyMarkUseCase
    {
        private readonly IMarkReadOnlyRepository _repository; 
        private readonly IMapper _mapper;

        public GetNearbyMarkUseCase(IMarkReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseMarksJson> Execute(double latitude, double longitude, double radiusInMeters)
        {
            var allMarks = await _repository.GetAll();

            var nearbyMarks = allMarks
                .Where(t => CalculeDistanceInMeters(latitude, longitude, t.Latitude, t.Longitude) < radiusInMeters)
                .ToList();

            return new ResponseMarksJson
            {
                Marks = _mapper.Map<List<ResponseMarkJson>>(nearbyMarks)
            };
        }

        private double CalculeDistanceInMeters(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371000;
            var latRad1 = DegreesToRadians(lat1);
            var latRad2 = DegreesToRadians(lat2);
            var deltaLat = DegreesToRadians(lat2 - lat1);
            var deltaLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                    Math.Cos(latRad1) * Math.Cos(latRad2) * 
                    Math.Pow(Math.Sin(deltaLon / 2), 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
