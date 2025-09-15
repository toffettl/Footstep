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

            RuleFor(request => request.UnlockedPointOfInterestStyle)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDPOINTOFINTERESTSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedHeadStyle)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDHEADSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedTorsoStyle)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDTORSOSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedLegStyle)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDLEGSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedBagStyle)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDBAGSTYLES_REQUIRED);
            
            RuleFor(request => request.UnlockedAcessoryStyle)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.UNLOCKEDACESSORYSTYLES_REQUIRED);
        }
    }
}
