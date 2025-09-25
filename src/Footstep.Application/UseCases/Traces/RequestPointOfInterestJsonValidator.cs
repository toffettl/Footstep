using FluentValidation;
using Footstep.Communication.Requests.Traces;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Traces
{
    public class RequestPointOfInterestJsonValidator : AbstractValidator<RequestPointOfInterestJson>
    {
        public RequestPointOfInterestJsonValidator()
        {   
            RuleFor(trace => trace.Coordinates!.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage(ResourceErrorMessages.INVALID_LATITUDE);

            RuleFor(trace => trace.Coordinates!.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage(ResourceErrorMessages.INVALID_LONGITUDE);

            RuleFor(trace => trace.Coordinates!.Latitude)
                .NotNull()
                .WithMessage(ResourceErrorMessages.LATITUDE_REQUIRED);

            RuleFor(trace => trace.Coordinates!.Longitude)
                .NotNull()
                .WithMessage(ResourceErrorMessages.LONGITUDE_REQUIRED);
        }
    }
}
