using FluentValidation;
using Footstep.Communication.Requests;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Traces
{
    public class TraceValidator : AbstractValidator<RequestTraceJson>
    {
        public TraceValidator()
        {
            RuleFor(trace => trace.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
            RuleFor(trace => trace.ExpireAt).GreaterThan(DateTime.UtcNow).WithMessage(ResourceErrorMessages.THE_EXPIRATION_DATE_CANNOT_BE_IN_THE_PAST);
        }
    }
}
