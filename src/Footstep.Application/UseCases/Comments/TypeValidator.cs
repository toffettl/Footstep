using FluentValidation;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Comments
{
    public class TypeValidator : AbstractValidator<int>
    {
        public TypeValidator()
        {
            RuleFor(x => x)
                .InclusiveBetween(0, 1).WithMessage(ResourceErrorMessages.TYPE_INVALID);
        }
    }
}
