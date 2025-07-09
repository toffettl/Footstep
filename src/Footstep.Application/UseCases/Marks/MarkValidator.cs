using System.Data;
using FluentValidation;
using Footstep.Communication.Requests.Marks;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Marks
{
    public class MarkValidator : AbstractValidator<RequestMarkJson>
    {
        public MarkValidator()
        {
            RuleFor(mark => mark.Name)
                .MinimumLength(3)
                .WithMessage(ResourceErrorMessages.NAME_TRACE_SIZE);

            RuleFor(mark => mark.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage(ResourceErrorMessages.INVALID_LATITUDE);

            RuleFor(mark => mark.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage(ResourceErrorMessages.INVALID_LONGITUDE);
        }
    }
}
