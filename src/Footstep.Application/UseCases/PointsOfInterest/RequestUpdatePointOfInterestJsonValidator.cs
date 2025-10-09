using FluentValidation;
using Footstep.Communication.Requests.Traces;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Traces
{
    public class RequestUpdatePointOfInterestJsonValidator : AbstractValidator<RequestUpdatePointOfInterestJson>
    {
        public RequestUpdatePointOfInterestJsonValidator()
        {
            RuleFor(trace => trace.ExpireAt)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage(ResourceErrorMessages.THE_EXPIRATION_DATE_CANNOT_BE_IN_THE_PAST);
        }
    }
}
