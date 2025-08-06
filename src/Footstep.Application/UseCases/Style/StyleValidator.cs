using FluentValidation;
using Footstep.Communication.Requests.Styles;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Style
{
    public class StyleValidator : AbstractValidator<RequestStyleJson>
    {
        public StyleValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);

            RuleFor(request => request.Description)
                .NotEmpty().WithMessage(ResourceErrorMessages.DESCRIPTION_REQUIRED);


            RuleFor(request => request.ImageUrl)
                .NotEmpty().WithMessage(ResourceErrorMessages.IMAGEURL_REQUIRED)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute)).WithMessage(ResourceErrorMessages.IMAGEURL_INVALID);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.PRICE_INVALID);
        }
    }
}
