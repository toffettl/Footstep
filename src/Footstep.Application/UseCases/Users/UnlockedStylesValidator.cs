using FluentValidation;
using Footstep.Communication.Requests.Users;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Users
{
    public class UnlockedStylesValidator : AbstractValidator<RequestUpdateUnlockedStylesUserJson>
    {
        public UnlockedStylesValidator()
        {
            RuleFor(request => request.UnlockedMapStyles)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDMAPSTYLES_REQUIRED);

            RuleFor(request => request.UnlockedPointOfInterestStyles)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDPOINTOFINTERESTSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedHeadStyles)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDHEADSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedTorsoStyles)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDTORSOSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedLegStyles)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDLEGSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedBagStyles)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDBAGSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedAcessoryStyles)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDACESSORYSTYLES_REQUIRED);
        }
    }
}
