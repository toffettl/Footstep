using FluentValidation;
using Footstep.Communication.Requests.Traces;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Traces
{
    public class TraceValidator : AbstractValidator<RequestPointOfInterestJson>
    {
        public TraceValidator()
        {   
            RuleFor(trace => trace.ExpireAt)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage(ResourceErrorMessages.THE_EXPIRATION_DATE_CANNOT_BE_IN_THE_PAST);
            
            RuleFor(trace => trace.Coordinates.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage(ResourceErrorMessages.INVALID_LATITUDE);

            RuleFor(trace => trace.Coordinates!.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage(ResourceErrorMessages.INVALID_LONGITUDE);

            RuleFor(trace => trace.Coordinates!.Latitude)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.LATITUDE_REQUIRED);

            RuleFor(trace => trace.Coordinates!.Longitude)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.LONGITUDE_REQUIRED);
        }
    }
}
