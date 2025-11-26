using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Traces;
using Footstep.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System;
namespace Footstep.Application.UseCases.Traces.GetByRay;
public class GetNearbyPointsOfInterestUseCase : IGetNearbyPointsOfInterestUseCase
{
    private readonly IPointOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
    private readonly IAmazonS3 _amazonS3;
    private readonly IOptions<S3Settings> _s3Settings;
    private readonly IMapper _mapper;

    public GetNearbyPointsOfInterestUseCase(
        IPointOfInterestReadOnlyRepository pointOfInterestReadOnlyRepository,
        IAmazonS3 amazonS3,
        IOptions<S3Settings> s3Settings,
        IMapper mapper)
    {
        _pointOfInterestReadOnlyRepository = pointOfInterestReadOnlyRepository;
        _amazonS3 = amazonS3;
        _s3Settings = s3Settings;
        _mapper = mapper;
    }

    public async Task<List<ResponsePaginationPointOfInterestJson>> Execute(double latitude, double longitude, double radiusInMeters)
    {
        var allPointsOfInterest = await _pointOfInterestReadOnlyRepository.GetAll();

        var nearbyTraces = allPointsOfInterest
            .Where(t => CalculateDistanceInMeters(latitude, longitude, t.Address.Latitude, t.Address.Longitude) <= radiusInMeters)
            .ToList();

        Console.WriteLine(CalculateDistanceInMeters(latitude, longitude, 0, 0));

        var responses = _mapper.Map<List<ResponsePaginationPointOfInterestJson>>(nearbyTraces);

        foreach (var pointOfInterest in nearbyTraces)
        {
            var response = _mapper.Map<ResponsePaginationPointOfInterestJson>(pointOfInterest);

            response.Media.Image = null;

            if (pointOfInterest.Images.Count > 0)
            {
                var image = pointOfInterest.Images.ElementAt(new Random().Next(pointOfInterest.Images.Count));
                response.Media.Image = GetResponseImage(image.Id);
            }

            responses.Add(response);
        }

        return responses;
    }

    private ResponsePaginationPointOfInterestImageJson GetResponseImage(Guid imageId)
    {
        var s3Request = new GetPreSignedUrlRequest
        {
            BucketName = _s3Settings.Value.BucketName,
            Key = imageId.ToString(),
            Expires = DateTime.UtcNow.AddDays(1)
        };

        return new ResponsePaginationPointOfInterestImageJson()
        {
            Id = imageId,
            Url = _amazonS3.GetPreSignedURL(s3Request)
        };
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
