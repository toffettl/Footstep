using AutoMapper;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.Create
{
    public class CreatePointOfInterestUseCase : ICreatePointOfInterestUseCase
    {
        private readonly IPointsOfInterestWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePointOfInterestUseCase(
            IPointsOfInterestWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePointOfIntereseJson> Execute(RequestPointOfInterestJson request)
        {
            Validade(request);

            var entity = _mapper.Map<PointOfInterest>(request);

            entity.CreatedAt = DateTime.UtcNow;

            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponsePointOfIntereseJson>(entity);
        }

        private void Validade(RequestPointOfInterestJson request)
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
