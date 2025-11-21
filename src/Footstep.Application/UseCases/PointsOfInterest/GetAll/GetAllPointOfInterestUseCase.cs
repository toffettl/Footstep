using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories.Traces;
using Footstep.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Footstep.Application.UseCases.Traces.GetAll
{
    public class GetAllPointOfInterestUseCase : IGetAllPoitntOfInterestUseCase
    {
        private readonly IPointOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
        private readonly IAmazonS3 _amazonS3;
        private readonly IOptions<S3Settings> _s3Settings;
        private readonly IMapper _mapper;

        public GetAllPointOfInterestUseCase(
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

        public async Task<List<ResponsePointOfInterestJson>> Execute()
        {
            var pointsOfInterest = await _pointOfInterestReadOnlyRepository.GetAll();
            var responses = new List<ResponsePointOfInterestJson>();

            foreach(var pointOfInterest in pointsOfInterest)
            {
                var response = _mapper.Map<ResponsePointOfInterestJson>(pointOfInterest);

                foreach (var image in pointOfInterest.Images)
                {
                    response.Media.Images.Add(GetResponseImage(image));
                }

                responses.Add(response);
            }

            return responses;
        }

        private ResponsePointOfInterestImageJson GetResponseImage(Image image)
        {
            var s3Request = new GetPreSignedUrlRequest
            {
                BucketName = _s3Settings.Value.BucketName,
                Key = image.Id.ToString(),
                Expires = DateTime.UtcNow.AddDays(1)
            };

            return new ResponsePointOfInterestImageJson()
            {
                Id = image.Id,
                Url = _amazonS3.GetPreSignedURL(s3Request)
            };
        }
    }
}
