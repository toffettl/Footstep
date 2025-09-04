using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Repositories.Traces;

namespace Footstep.Application.UseCases.Traces.GetAll
{
    public class GetAllPointOfInterestUseCase : IGetAllPoitntOfInterestUseCase
    {
        private readonly IPointsOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetAllPointOfInterestUseCase(
            IPointsOfInterestReadOnlyRepository pointOfInterestReadOnlyRepository,
            IMapper mapper)
        {
            _pointOfInterestReadOnlyRepository = pointOfInterestReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<ResponsePointOfInterestJson>> Execute()
        {
            var pointsOfInterest = await _pointOfInterestReadOnlyRepository.GetAll();

            return _mapper.Map<List<ResponsePointOfInterestJson>>(pointsOfInterest);
        }
    }
}
