using Amazon.S3;
using Amazon.S3.Model;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Images;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using Footstep.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Footstep.Application.UseCases.Users.UpdateAddProfilePicture
{
    public class UpdateAddProfilePictureUserUseCase : IUpdateAddProfilePictureUserUseCase
    {
        private readonly IImageWriteOnlyRepository _imageWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IAmazonS3 _amazonS3;
        private readonly IOptions<S3Settings> _s3Settings;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddProfilePictureUserUseCase(
            IImageWriteOnlyRepository imageWriteOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IAmazonS3 amazonS3,
            IOptions<S3Settings> s3Settings,
            IUnitOfWork unitOfWork)
        {
            _imageWriteOnlyRepository = imageWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _amazonS3 = amazonS3;
            _s3Settings = s3Settings;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid Id, Stream stream, string fileName, string contentType)
        {
            var user = await _userReadOnlyRepository.GetById(Id);

            if (user == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            if (user.Preference.ImageId != null)
            {
                throw new System.Exception(ResourceErrorMessages.IMAGE_QUANTITY_INVALID);
            }

            Image image = new Image
            {
                PreferenceId = user.PreferenceId
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
            }
            catch
            {
                throw new System.Exception(ResourceErrorMessages.S3_CONNECTION_INVALID);
            }

            await _imageWriteOnlyRepository.Add(image);

            await _unitOfWork.Commit();
        }
    }
}
