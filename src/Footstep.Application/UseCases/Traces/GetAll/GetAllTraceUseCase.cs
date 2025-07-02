using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Repositories.Traces;

namespace Footstep.Application.UseCases.Traces.GetAll
{
    public class GetAllTraceUseCase : IGetAllTraceUseCase
    {
        private readonly ITracesReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTraceUseCase(ITracesReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseTracesJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseTracesJson
            {
                Traces = _mapper.Map<List<ResponseTraceJson>>(result)
            };
        }
    }
}
