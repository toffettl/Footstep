using FluentValidation;
using Footstep.Communication.Requests.Users;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Users
{
    public class PreferencesValidator : AbstractValidator<RequestUpdatePreferencesUserJson>
    {
        public PreferencesValidator()
        {
            RuleFor(request => request.MapStyle)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.MAPSTYLE_REQUIRED);

            RuleFor(request => request.PointOfInterestStyle)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.POINTOFINTERESTSTYLE_REQUIRED);

            RuleFor(request => request.AvatarOverProfile)
                .NotNull()
                .WithMessage(ResourceErrorMessages.AVATAROVERPROFILE_REQUIRED);

            RuleFor(request => request.AvatarStyle!.Head)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.HEAD_REQUIRED);

            RuleFor(request => request.AvatarStyle!.Body)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.BODY_EMPTY);

            RuleFor(request => request.AvatarStyle!.Leg)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.LEG_REQUIRED);

            RuleFor(request => request.AvatarStyle!.Bag)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.BAG_REQUIRED);

            RuleFor(request => request.AvatarStyle!.Acessory)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.ACESSORY_REQUIRED);
        }
    }
}
