using FluentValidation;
using Footstep.Communication.Requests.Comments;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Comments
{
    public class CommentValidator : AbstractValidator<RequestCommentJson>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ResourceErrorMessages.COMMENT_CANNOT_BE_EMPTY);

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage(ResourceErrorMessages.AUTHOR_ID_CANNOT_BE_EMPTY);

            RuleFor(x => x.ParentId)
                .NotEmpty().WithMessage(ResourceErrorMessages.PARENT_ID_CANNOT_BE_EMPTY);

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ResourceErrorMessages.CONTENT_EMPTY);
        }
    }
}
