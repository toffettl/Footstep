using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Communication.Responses.Users;
using Footstep.Domain.Repositories.Users;
using Footstep.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Footstep.Application.UseCases.Users.GetByRanking
{
    public class GetUsersByRankingUseCase : IGetUsersByRankingUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IAmazonS3 _amazonS3;
        private readonly IOptions<S3Settings> _s3Settings;
        private readonly IMapper _mapper;

        public GetUsersByRankingUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IAmazonS3 amazonS3,
            IOptions<S3Settings> s3Settings,
            IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _amazonS3 = amazonS3;
            _mapper = mapper;
            _s3Settings = s3Settings;
        }

        public async Task<PagedResult<ResponsePaginationUserJson>> Execute(int page, int pageSize, DateTime dateTime)
        {
            var (users, totalCount) = await _userReadOnlyRepository.GetByRanking(page, pageSize, dateTime);

            List<ResponsePaginationUserJson> responses = new List<ResponsePaginationUserJson>();

            foreach (var user in users)
            {
                var response = _mapper.Map<ResponsePaginationUserJson>(user);

                response.ProfilePicture!.Uri = "";

                if (user.Preference.Image != null)
                {
                    response.ProfilePicture!.Uri = GetProfilePictureUrl(user.Preference.Image!.Id);
                }

                responses.Add(response);
            }

            return new PagedResult<ResponsePaginationUserJson>
            {
                Items = responses,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
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
