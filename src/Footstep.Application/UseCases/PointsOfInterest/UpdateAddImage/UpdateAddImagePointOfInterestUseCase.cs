using Amazon.S3;
using Amazon.S3.Model;
using Footstep.Application.UseCases.PointsOfInterest.UpdateImages;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Images;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using Footstep.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Footstep.Application.UseCases.PointsOfInterest.UpdateImage
{
    public class UpdateAddImagePointOfInterestUseCase : IUpdateAddImagePointOfInterestUseCase
    {
        private readonly IPointOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
        private readonly IImageWriteOnlyRepository _imageWriteOnlyRepository;
        private readonly IAmazonS3 _amazonS3;
        private readonly IOptions<S3Settings> _s3Settings;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddImagePointOfInterestUseCase(
            IPointOfInterestReadOnlyRepository pointOfInterestReadOnlyRepository,
            IImageWriteOnlyRepository imageWriteOnlyRepository,
            IAmazonS3 amazonS3,
            IOptions<S3Settings> s3Settings,
            IUnitOfWork unitOfWork)
        {
            _pointOfInterestReadOnlyRepository = pointOfInterestReadOnlyRepository;
            _imageWriteOnlyRepository = imageWriteOnlyRepository;
            _amazonS3 = amazonS3;
            _s3Settings = s3Settings;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid Id, Stream stream, string fileName, string contentType)
        {
            var pointOfInterest = await _pointOfInterestReadOnlyRepository.GetById(Id);

            if (pointOfInterest == null)
            {
                throw new NotFoundException(ResourceErrorMessages.POINT_OF_INTEREST_NOT_FOUND);
            }

            if (pointOfInterest.Images.Count() > 0)
            {
                throw new System.Exception(ResourceErrorMessages.IMAGE_QUANTITY_INVALID);
            }

            Image image = new Image
            {
                PointOfInterestId = pointOfInterest.Id,
            };

            var putRequest = new PutObjectRequest
            {
                BucketName = _s3Settings.Value.BucketName,
                Key = image.Id.ToString(),
                InputStream = stream,
                ContentType = contentType,
                Metadata =
                {
                    ["file-name"] = fileName
                }
            };

            try
            {
                await _amazonS3.PutObjectAsync(putRequest);
            } catch
            {
                throw new System.Exception(ResourceErrorMessages.S3_CONNECTION_INVALID);
            }

            await _imageWriteOnlyRepository.Add(image);

            await _unitOfWork.Commit();
        }
    }
}
