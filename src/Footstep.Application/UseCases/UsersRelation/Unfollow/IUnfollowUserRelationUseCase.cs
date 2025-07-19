namespace Footstep.Application.UseCases.UserRelation.Unfollow
{
    public interface IUnfollowUserRelationUseCase
    {
        Task Execute(Guid followerId, Guid followingId);
    }
}
