using AutoMapper;
using Footstep.Communication.Responses.UserRelation;
using Footstep.Domain.Repositories.RelationUser;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.UsersRelation.GetFollowing
{
    public class GetFollowingUserRelationUseCase : IGetFollowingUserRelationUseCase
    {
        private readonly IUserRelationReadOnlyRepository _repostiory;
        private readonly IMapper _mapper;
        public GetFollowingUserRelationUseCase(
            IUserRelationReadOnlyRepository repository,
            IMapper mapper)
        {
            _repostiory = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseFollowingJson>> Execute(Guid followerId)
        {
            var result = await _repostiory.GetFollowing(followerId);

            if (result.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.THIS_USER_DOES_NOT_FOLLOW_ANYONE);
            }

            return _mapper.Map<List<ResponseFollowingJson>>(result);
        }
    }
}
