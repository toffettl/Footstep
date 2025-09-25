using FluentValidation;
using Footstep.Communication.Enums;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Comments
{
    public class ParentTypeValidator : AbstractValidator<ParentType>
    {
        public ParentTypeValidator()
        {
            RuleFor(x => x)
                .IsInEnum();
        }
    }
}
