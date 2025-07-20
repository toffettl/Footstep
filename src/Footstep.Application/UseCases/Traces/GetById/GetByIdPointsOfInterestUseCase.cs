using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.GetById
{
    public class GetByIdPointsOfInterestUseCase : IGetByIdPointOfInterestUseCase
    {
        private readonly IPointsOfInterestReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdPointsOfInterestUseCase(IPointsOfInterestReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponsePointOfIntereseJson> Execute(Guid id)
        {
            var result = await _repository.GetById(id);

            if (result == null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRACE_NOT_FOUND);
            }

            return _mapper.Map<ResponsePointOfIntereseJson>(result);
        }
    }
}
