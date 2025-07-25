using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Repositories.Traces;

namespace Footstep.Application.UseCases.Traces.GetAll
{
    public class GetAllPointOfInterestUseCase : IGetAllPoitntOfInterestUseCase
    {
        private readonly IPointsOfInterestReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPointOfInterestUseCase(IPointsOfInterestReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponsePointOfInterestJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponsePointOfInterestJson
            {
                Traces = _mapper.Map<List<ResponsePointOfIntereseJson>>(result)
            };
        }
    }
}
