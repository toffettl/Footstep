using FluentValidation;
using Footstep.Communication.Requests.Comments;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Comments.UpdateStatus
{
    public class UpdateStatusValidator : AbstractValidator<RequestUpdateStatusCommentsJson>
    {
        public UpdateStatusValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage(ResourceErrorMessages.COMMENT_CANNOT_BE_EMPTY);        }
    }
}
