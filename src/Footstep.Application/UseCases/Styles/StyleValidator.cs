using FluentValidation;
using Footstep.Communication.Requests.Styles;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Styles
{
    public class StyleValidator : AbstractValidator<RequestStyleJson>
    {
        public StyleValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.NAME_EMPTY);

            RuleFor(request => request.Image)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.IMAGE_EMPTY);

            RuleFor(request => request.Price)
                .NotNull()
                .WithMessage(ResourceErrorMessages.PRICE_REQUIRED);

            RuleFor(request => request.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ResourceErrorMessages.PRICE_INVALID);

            RuleFor(request => request.Store)
                .NotNull()
                .WithMessage(ResourceErrorMessages.STORE_REQUIRED);

            RuleFor(request => request.StyleType)
                .IsInEnum()
                .WithMessage(ResourceErrorMessages.STYLETYPE_INVALID);

            RuleFor(request => request.StyleType)
                .NotNull()
                .WithMessage(ResourceErrorMessages.STYLETYPE_REQUIRED);
        }
    }
}
