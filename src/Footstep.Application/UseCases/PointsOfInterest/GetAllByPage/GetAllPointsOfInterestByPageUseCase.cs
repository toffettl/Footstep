using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Traces;
using Footstep.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Footstep.Application.UseCases.Traces.GetAllByPage
{
    public class GetAllPointsOfInterestByPageUseCase : IGetAllPointsOfInterestByPageUseCase
    {
        private readonly IPointOfInterestReadOnlyRepository _pointsOfInterestReadOnlyRepository;
        private readonly IAmazonS3 _amazonS3;
        private readonly IOptions<S3Settings> _s3Settings;
        private readonly IMapper _mapper;
        private readonly Random _random = new Random();

        public GetAllPointsOfInterestByPageUseCase(
            IPointOfInterestReadOnlyRepository pointsOfInterestReadOnlyRepository,
            IAmazonS3 amazonS3,
            IOptions<S3Settings> s3Settings,
            IMapper mapper)
        {
            _pointsOfInterestReadOnlyRepository = pointsOfInterestReadOnlyRepository;
            _amazonS3 = amazonS3;
            _s3Settings = s3Settings;
            _mapper = mapper;
        }

        public async Task<PagedResult<ResponsePaginationPointOfInterestJson>> Execute(int page, int pageSize)
        {
            var (pointsOfInterest, totalCount) = await _pointsOfInterestReadOnlyRepository.GetAllByPage(page, pageSize);

            var responses = new List<ResponsePaginationPointOfInterestJson>();

            foreach (var pointOfInterest in pointsOfInterest)
            {
                var response = _mapper.Map<ResponsePaginationPointOfInterestJson>(pointOfInterest);
                
                var image = pointOfInterest.Images.ElementAt(_random.Next(pointOfInterest.Images.Count));

                response.Media.Image = GetResponseImage(image.Id);

                responses.Add(response);
            }

            return new PagedResult<ResponsePaginationPointOfInterestJson>
            {
                Items = responses,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
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
    }
}
