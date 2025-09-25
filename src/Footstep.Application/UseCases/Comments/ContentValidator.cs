using FluentValidation;
using Footstep.Communication.Requests.Comments;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Comments
{
    public class ContentValidator : AbstractValidator<RequestUpdateContentComment>
    {
        public ContentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ResourceErrorMessages.CONTENT_EMPTY);
        }
    }
}
