using Footstep.Communication.Requests.UserRelation;
using Footstep.Communication.Responses.UserRelation;

namespace Footstep.Application.UseCases.UsersRelation.GetFollowers
{
    public interface IGetFollowersUserRelationUseCase
    {
        Task<List<ResponseFollowersJson>> Execute(Guid followingId);
    }
}
