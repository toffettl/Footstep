using Amazon.S3;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Images;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using Footstep.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Footstep.Application.UseCases.PointsOfInterest.UpdateDeleteImage
{
    public class UpdateDeleteImagePointOfInterestUseCase : IUpdateDeleteImagePointOfInterestUseCase
    {
        private readonly IImageWriteOnlyRepository _imageWriteOnlyRepository;
        private readonly IAmazonS3 _amazonS3;
        private readonly IOptions<S3Settings> _s3Settings;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDeleteImagePointOfInterestUseCase(
            IImageWriteOnlyRepository imageWriteOnlyRepository,
            IAmazonS3 amazonS3,
            IOptions<S3Settings> s3Settings,
            IUnitOfWork unitOfWork)
        {
            _imageWriteOnlyRepository = imageWriteOnlyRepository;
            _amazonS3 = amazonS3;
            _s3Settings = s3Settings;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid imageId)
        {
            try
            {
                await _amazonS3.DeleteObjectAsync(_s3Settings.Value.BucketName, imageId.ToString());
            } catch
            {
                throw new System.Exception(ResourceErrorMessages.S3_CONNECTION_INVALID);
            }

            var result = await _imageWriteOnlyRepository.Delete(imageId);

            if (result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.IMAGE_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
