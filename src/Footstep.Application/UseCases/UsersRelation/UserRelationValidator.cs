using FluentValidation;
using Footstep.Communication.Requests.UserRelation;
using Footstep.Exception;

namespace Footstep.Application.UseCases.RelationUser;
public class UserRelationValidator : AbstractValidator<RequestUserRelationJson>
{
    public UserRelationValidator()
    {
        RuleFor(user => user.FollowerId)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.FOLLOWER_ID_CANNOT_BE_NULL);

        RuleFor(user => user.FollowingId)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.FOLLOWED_USER_ID_CANNOT_BE_NULL);
    }
}
