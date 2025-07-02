using AutoMapper;
using Footstep.Communication.Requests.Traces;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.Update
{
    public class UpdateTraceUseCase : IUpdateTraceUseCase
    {
        private readonly ITracesUpdateOnlyRepository _repostiory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTraceUseCase(ITracesUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            _repostiory = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Execute(Guid id, RequestTraceJson request)
        {
            Validate(request);

            var trace = await _repostiory.GetById(id);

            if(trace == null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRACE_NOT_FOUND);
            }

            _mapper.Map(request, trace);

            _repostiory.Update(trace);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestTraceJson request)
        {
            var validator = new TraceValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorsMessages);
            }
        }            
    }
}
