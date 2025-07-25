using Footstep.Communication.Responses.UserRelation;

namespace Footstep.Application.UseCases.UsersRelation.GetFollowing
{
    public interface IGetFollowingUserRelationUseCase
    {
        Task<List<ResponseFollowingJson>> Execute(Guid followerId);
    }
}
