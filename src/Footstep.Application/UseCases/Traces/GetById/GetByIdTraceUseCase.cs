using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.GetById
{
    public class GetByIdTraceUseCase : IGetByIdTraceUseCase
    {
        private readonly ITracesReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdTraceUseCase(ITracesReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseTraceJson> Execute(Guid id)
        {
            var result = await _repository.GetById(id);

            if(result == null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRACE_NOT_FOUND);
            }

            return _mapper.Map<ResponseTraceJson>(result);
        }
    }
}
