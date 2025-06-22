using FluentValidation;
using Footstep.Communication.Requests;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Traces
{
    public class TraceValidator : AbstractValidator<RequestTraceJson>
    {
        public TraceValidator()
        {
            RuleFor(trace => trace.Name)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.NAME_REQUIRED);
            
            RuleFor(trace => trace.ExpireAt)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage(ResourceErrorMessages.THE_EXPIRATION_DATE_CANNOT_BE_IN_THE_PAST);
            
            RuleFor(trace => trace.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage(ResourceErrorMessages.INVALID_LATITUDE);

            RuleFor(trace => trace.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage(ResourceErrorMessages.INVALID_LONGITUDE);

            RuleFor(trace => trace.Latitude)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.LATITUDE_REQUIRED);

            RuleFor(trace => trace.Longitude)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.LONGITUDE_REQUIRED);
        }
    }
}
