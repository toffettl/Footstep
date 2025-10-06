using AutoMapper;
using Footstep.Communication.Requests.Traces;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.Update
{
    public class UpdatePointOfInterestUseCase : IUpdatePointOfInterestUseCase
    {
        private readonly IPointOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
        private readonly IPointOfInterestUpdateOnlyRepository _pointOfInterestUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePointOfInterestUseCase(
            IPointOfInterestReadOnlyRepository pointsOfInterestReadOnlyRepository,
            IPointOfInterestUpdateOnlyRepository pointOfInterestUpdateOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _pointOfInterestReadOnlyRepository = pointsOfInterestReadOnlyRepository;
            _pointOfInterestUpdateOnlyRepository = pointOfInterestUpdateOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Execute(Guid id, RequestUpdatePointOfInterestJson request)
        {
            Validate(request);

            var pointOfInterest = await _pointOfInterestReadOnlyRepository.GetById(id);

            if(pointOfInterest == null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRACE_NOT_FOUND);
            }

            _mapper.Map(request, pointOfInterest);

            _pointOfInterestUpdateOnlyRepository.Update(pointOfInterest);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestUpdatePointOfInterestJson request)
        {
            var validator = new RequestUpdatePointOfInterestJsonValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorsMessages);
            }
        }            
    }
}
