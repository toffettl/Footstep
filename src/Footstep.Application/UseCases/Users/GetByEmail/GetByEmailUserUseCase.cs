using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Enums;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using Footstep.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Footstep.Application.UseCases.Users.GetByEmail
{
    public class GetByEmailUserUseCase : IGetByEmailUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IAmazonS3 _amazonS3;
        private readonly IOptions<S3Settings> _s3Settings;
        private readonly IMapper _mapper;

        public GetByEmailUserUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IAmazonS3 amazonS3,
            IOptions<S3Settings> s3Settings,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _amazonS3 = amazonS3;
            _s3Settings = s3Settings;
            _mapper = mapper;
        }

        public async Task<ResponseUserJson> Execute(string email)
        {
            var user = await _userReadOnlyRepository.GetByEmail(email);

            if (user == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            var response = _mapper.Map<ResponseUserJson>(user);

            response.ProfilePicture!.Uri = "";

            if (user.Preference.Image != null)
            {
                response.ProfilePicture!.Uri = GetProfilePictureUrl(user.Preference.Image!.Id);
            }

            return response;
        }

        private string GetProfilePictureUrl(Guid imageId)
        {
            var s3Request = new GetPreSignedUrlRequest
            {
                BucketName = _s3Settings.Value.BucketName,
                Key = imageId.ToString(),
                Expires = DateTime.UtcNow.AddDays(1)
            };

            return _amazonS3.GetPreSignedURL(s3Request);
        }
    }
}
