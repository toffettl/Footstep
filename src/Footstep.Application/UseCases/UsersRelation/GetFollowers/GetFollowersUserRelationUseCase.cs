using AutoMapper;
using Footstep.Communication.Responses.UserRelation;
using Footstep.Domain.Repositories.RelationUser;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.UsersRelation.GetFollowers
{
    public class GetFollowersUserRelationUseCase : IGetFollowersUserRelationUseCase
    {
        private readonly IUserRelationReadOnlyRepository _repostiory;
        private readonly IMapper _mapper;
        public GetFollowersUserRelationUseCase(
            IUserRelationReadOnlyRepository repository,
            IMapper mapper)
        {
            _repostiory = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseFollowersJson>> Execute(Guid followingId)
        {
            var result = await _repostiory.GetFollowers(followingId);

            if (result.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.THIS_USER_HAS_NO_FOLLOWERS);
            }

            return _mapper.Map<List<ResponseFollowersJson>>(result);
        }
    }
}
