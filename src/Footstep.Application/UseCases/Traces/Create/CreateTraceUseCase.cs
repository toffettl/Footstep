using AutoMapper;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.Create
{
    public class CreateTraceUseCase : ICreateTraceUseCase
    {
        private readonly ITracesWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateTraceUseCase(
            ITracesWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseCreateTraceJson> Execute(RequestTraceJson request)
        {
            Validade(request);

            var entity = _mapper.Map<Trace>(request);

            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseCreateTraceJson>(entity);
        }

        private void Validade(RequestTraceJson request)
        {
            var validator = new TraceValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
