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

            RuleFor(request => request.AvatarStyle!.Skin)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.HEAD_REQUIRED);

            RuleFor(request => request.AvatarStyle!.Top)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.TORSO_REQUIRED);

            RuleFor(request => request.AvatarStyle!.Backpack)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.LEG_REQUIRED);

            RuleFor(request => request.AvatarStyle!.Clothe)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.BAG_REQUIRED);

            RuleFor(request => request.AvatarStyle!.Acessory)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.ACESSORY_REQUIRED);
        }
    }
}
