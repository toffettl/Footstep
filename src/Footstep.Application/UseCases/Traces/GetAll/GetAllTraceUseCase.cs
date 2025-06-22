using AutoMapper;
using Footstep.Communication.Responses;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
